using BossMod.AI;
using BossMod.Autorotation;
using BossMod.Pathfinding;

namespace BossMod.Dawntrail.Ultimate.FRU;

// note: this assumes the positions my static was using
sealed class FRUAI(RotationModuleManager manager, Actor player) : AIRotationModule(manager, player)
{
    public enum Track { Movement }
    public enum MovementStrategy { None, Pathfind, PathfindMeleeGreed, Explicit, ExplicitMelee, Prepull, DragToCenter }

    public static RotationModuleDefinition Definition()
    {
        var res = new RotationModuleDefinition("AI Experiment", "Experimental encounter-specific rotation", "Encounter AI", "veyn", RotationModuleQuality.WIP, new(~1ul), 100, 1, RotationModuleOrder.Movement, typeof(FRU));
        res.Define(Track.Movement).As<MovementStrategy>("Movement", "Movement")
            .AddOption(MovementStrategy.None, "No automatic movement")
            .AddOption(MovementStrategy.Pathfind, "Use standard pathfinding to move")
            .AddOption(MovementStrategy.PathfindMeleeGreed, "Stay at max effective range if that cell is still as safe as current, otherwise go to safespot")
            .AddOption(MovementStrategy.Explicit, "Move to specific point", supportedTargets: ActionTargets.Area)
            .AddOption(MovementStrategy.ExplicitMelee, "Move to the point in maxmelee that is closest to specific point", supportedTargets: ActionTargets.Area)
            .AddOption(MovementStrategy.Prepull, "Pre-pull position: as close to the clock-spot as possible")
            .AddOption(MovementStrategy.DragToCenter, "Drag boss to the arena center");
        return res;
    }

    private readonly FRUConfig _config = Service.Config.Get<FRUConfig>();

    public override void Execute(StrategyValues strategy, ref Actor? primaryTarget, float estimatedAnimLockDelay, bool isMoving)
    {
        if (Player.PendingKnockbacks.Count > 0)
        {
            Hints.ForcedMovement = default;
            return;
        }

        // same as Automatic movement: on thin ice a 1y nudge becomes a 32y slide, so only "stay" or "full slide" are legal
        var thinIce = Player.FindStatus(SID.ThinIce);
        if (thinIce != null)
        {
            var distance = thinIce.Value.Extra * 0.1f;
            Hints.AddForbiddenZone(ShapeDistance.Donut(Player.Position, 1, distance - 1), World.FutureTime(2));
        }

        if (Bossmods.ActiveModule is FRU module && module.Raid.TryFindSlot(Player.InstanceID, out var playerSlot))
        {
            SetForcedMovement(CalculateDestination(module, primaryTarget, strategy.Option(Track.Movement), Service.Config.Get<PartyRolesConfig>()[module.Raid.Members[playerSlot].ContentId]), thinIce != null ? 1.5f : 0.1f);
        }
    }

    private WPos? CalculateDestination(FRU module, Actor? primaryTarget, StrategyValues.OptionRef strategy, PartyRolesConfig.Assignment assignment)
    {
        var strat = strategy.As<MovementStrategy>();
        // plan default is PathfindMeleeGreed; Prepull is only a short window near 0. don't walk onto the boss during countdown
        if (!Player.InCombat && strat is MovementStrategy.Pathfind or MovementStrategy.PathfindMeleeGreed)
            strat = MovementStrategy.Prepull;

        return strat switch
        {
            MovementStrategy.Pathfind => PathfindPosition(null, assignment, primaryTarget),
            MovementStrategy.PathfindMeleeGreed => PathfindPosition(SuppressMeleeGreed(module, assignment) ? null : ResolveTarget(strategy.Value) ?? primaryTarget, assignment, primaryTarget),
            MovementStrategy.Explicit => ResolveTargetLocation(strategy.Value),
            MovementStrategy.ExplicitMelee => ExplicitMeleePosition(ResolveTargetLocation(strategy.Value), ResolveTarget(strategy.Value) ?? primaryTarget),
            MovementStrategy.Prepull => PrepullPosition(module, assignment),
            MovementStrategy.DragToCenter => DragToCenterPosition(module),
            _ => null
        };
    }

    // same idea as ThinIce: when the plan wants a spot off maxmelee, don't snap back to the boss
    private bool SuppressMeleeGreed(FRU module, PartyRolesConfig.Assignment assignment)
    {
        if (Player.FindStatus(SID.ThinIce) != null)
            return true;
        if (!module.Raid.TryFindSlot(Player.InstanceID, out var slot))
            return false;

        if (module.FindComponent<P1Explosion>() is { } explosion && explosion.RequiresStrictPosition(slot, assignment))
            return true;
        if (module.FindComponent<P2MirrorMirrorReflectedScytheKickBlue>() is { } blue && blue.RequiresStrictPosition(slot))
            return true;
        if (module.FindComponent<P2MirrorMirrorBanish>() is { } banish && banish.RequiresStrictPosition(slot, assignment))
            return true;
        if (module.FindComponent<P2Intermission>() is { } intermission && intermission.RequiresStrictPosition(assignment))
            return true;
        if (module.FindComponent<P3UltimateRelativity>() is { } relativity && relativity.RequiresStrictPosition(slot, Player))
            return true;
        return false;
    }

    // TODO: account for leeway for casters
    private WPos PathfindPosition(Actor? maxRangeTarget, PartyRolesConfig.Assignment assignment, Actor? primaryTarget)
    {
        var navi = NavigationDecision.Build(NavigationContext, World.CurrentTime, Hints, Player.Position, Speed());
        var dest = navi.Destination ?? Player.Position;
        var rangeRef = maxRangeTarget ?? (FRU.StandsRanged(assignment, Player) ? primaryTarget : null);
        if (rangeRef == null)
            return dest;

        const float greedTolerance = 0.15f;
        var isRanged = FRU.StandsRanged(assignment, Player);
        var maxRange = Player.HitboxRadius + rangeRef.HitboxRadius + (isRanged ? 25f : 3f) - greedTolerance;
        var minRange = isRanged ? Player.HitboxRadius + rangeRef.HitboxRadius + 8f : 0f;
        var toDest = dest - rangeRef.Position;
        var range = toDest.Length();
        var dir = range > 0.1f ? toDest / range : (Player.Position - rangeRef.Position);
        if (dir.LengthSq() < 0.01f)
            dir = new WDir(0, -1);
        else
            dir = dir.Normalized();

        WPos? snap = null;
        if (maxRangeTarget != null && range > maxRange)
            snap = rangeRef.Position + maxRange * dir;
        else if (isRanged && range < minRange && (dest - Player.Position).LengthSq() < 4)
            snap = rangeRef.Position + minRange * dir; // idle in melee: step out so we don't sit on tanks
        if (snap == null)
            return dest;
        if (navi.LeewaySeconds <= 0)
            return dest;

        var map = NavigationContext.Map;
        var snapGrid = map.WorldToGrid(snap.Value);
        var snapG = map.InBounds(snapGrid.x, snapGrid.y) ? map.PixelMaxG.BoundSafeAt(map.GridToIndex(snapGrid)) : 0;
        var curG = map.PixelMaxG.BoundSafeAt(NavigationContext.ThetaStar.StartNodeIndex);
        if (snapG >= curG)
            return snap.Value;
        return Player.DistanceToHitbox(rangeRef) <= maxRange ? Player.Position : dest;
    }

    private WPos ExplicitMeleePosition(WPos ideal, Actor? target) => target != null ? ClosestInMelee(ideal, target) : ideal;

    // assumption: pull range is 12; hitbox is 5, so maxmelee is 8, meaning we have approx 4m to move during pull - with sprint, speed is 7.8, accel is 30 => over 0.26s accel period we move 1.014m, then need another 0.38s to reach boss (but it also moves)
    private WPos PrepullPosition(FRU module, PartyRolesConfig.Assignment assignment)
    {
        var safeRange = 12.5f;
        var desiredRange = assignment is PartyRolesConfig.Assignment.MT or PartyRolesConfig.Assignment.OT or PartyRolesConfig.Assignment.M1 or PartyRolesConfig.Assignment.M2 ? 5 : 10;
        var dir = _config.P1CyclonicBreakSpots[assignment];
        if (dir < 0)
            dir = 0;
        var desiredPos = module.Center + desiredRange * (180 - 45 * dir).Degrees().ToDirection();
        var off = desiredPos - module.PrimaryActor.Position;
        var distSq = off.LengthSq();
        if (distSq >= safeRange * safeRange)
            return desiredPos;
        off /= MathF.Sqrt(distSq);
        return module.PrimaryActor.Position + off * safeRange;
    }

    // notes on boss movement: boss top speed is ~8.5m, it moves up to distance 7.5m (both hitboxes + 2m)
    // empyrically, if i stand still, i can start moving when boss is ~11m away and it will still be dragged to intended spot
    private WPos DragToCenterPosition(FRU module)
    {
        if (module.PrimaryActor.Position.Z >= module.Center.Z - 1)
            return module.Center - new WDir(0, 6); // boss is positioned, go to N clockspot
        var dragDistance = module.PrimaryActor.HitboxRadius + Player.HitboxRadius + 2.25f; // we need to stay approx here, it's fine to overshoot a little bit - then when boss teleports, it won't turn
        var meleeDistance = module.PrimaryActor.HitboxRadius + Player.HitboxRadius + 2.75f; // -0.25 is a small extra leeway
        var dragDir = (module.Center - module.PrimaryActor.Position).Normalized();
        var dragSpot = module.Center + dragDistance * dragDir;
        var timeToMelee = ((dragSpot - module.PrimaryActor.Position).Length() - meleeDistance) / (Speed() + 8.5f); // assume 8.5 boss speed...
        return GCD >= timeToMelee + 0.1f ? dragSpot : module.PrimaryActor.Position + meleeDistance * dragDir;
    }
}
