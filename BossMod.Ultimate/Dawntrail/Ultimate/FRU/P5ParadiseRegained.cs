namespace BossMod.Dawntrail.Ultimate.FRU;

class P5ParadiseRegainedTowers(BossModule module) : Components.GenericTowers(module, AID.WingsDarkAndLightExplosion)
{
    private readonly FRUConfig _config = Service.Config.Get<FRUConfig>();
    private WDir _southDir;

    public WDir RelSouth => _southDir;

    public override void AddAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        if (actor.Role == Role.Tank || Towers.Count == 0 || _southDir == default)
            return;

        var group = _config.P5ParadiseRegainedAssignments[assignment];
        var assigned = Towers.FindIndex(t => !t.ForbiddenSoakers[slot]);
        // left/right towers sit in the first cleave; soak those only after south resolves
        var canSoak = assigned >= 0 && (group <= 0 || NumCasts > 0);
        if (canSoak)
        {
            ref var t = ref Towers.Ref(assigned);
            hints.AddForbiddenZone(ShapeDistance.InvertedCircle(t.Position, 2), t.Activation);
        }
        else
        {
            foreach (var t in Towers)
                hints.AddForbiddenZone(ShapeDistance.Circle(t.Position, t.Radius), t.Activation);
            if (group is 1 or 2)
            {
                var south = _southDir.Normalized();
                var side = group == 1 ? south.OrthoR() : south.OrthoL();
                // dark (closest tether): stay max melee so OT can sit in the hitbox
                // light (farthest tether): go in so OT can be farthest
                var baits = Module.FindComponent<P5ParadiseRegainedBaits>();
                var dpsIn = baits is { Active: true, TetherClosest: false };
                // dark: left/right of the south tower at max melee; light: step in so OT can be farthest
                var dest = Module.Center + (dpsIn ? 3 : 7) * south + (dpsIn ? 2 : 4) * side;
                hints.AddForbiddenZone(ShapeDistance.InvertedCircle(dest, 1), WorldState.CurrentTime);
            }
        }
    }

    public override void OnMapEffect(byte index, uint state)
    {
        if (index is >= 51 and <= 53 && state == 0x00020001)
        {
            var dir = index switch
            {
                51 => -120.Degrees(),
                52 => 120.Degrees(),
                _ => 0.Degrees()
            };
            var pos = Module.Center + 7 * dir.ToDirection();
            var group = 0;
            if (Towers.Count == 0)
            {
                _southDir = pos - Module.Center;
            }
            else
            {
                var delta = (Angle.FromDirection(pos - Module.Center) - Angle.FromDirection(_southDir)).Normalized();
                group = delta.Rad < 0 ? 1 : 2; // left / right looking from south toward the boss
            }
            var tanks = Raid.WithSlot(true).WhereActor(p => p.Role == Role.Tank).Mask();
            var soakers = _config.P5ParadiseRegainedAssignments.BuildGroupMask(group, Raid) & ~tanks;
            var forbidden = soakers.Any() ? new BitMask(0xFF) & ~soakers : tanks;
            Towers.Add(new(pos, 3, 2, 2, forbidden, WorldState.FutureTime(9.5f)));
        }
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if (spell.Action == WatchedAction)
        {
            ++NumCasts;
            if (Towers.Count == 0)
            {
                ReportError($"Unexpected expolosion @ {caster.Position}");
            }
            else
            {
                if (!Towers[0].Position.AlmostEqual(caster.Position, 1))
                    ReportError($"Unexpected position: {caster.Position} instead of {Towers[0].Position}");
                Towers.RemoveAt(0);
            }
        }
    }
}

class P5ParadiseRegainedBaits(BossModule module) : Components.GenericBaitAway(module)
{
    private readonly WDir _relSouth = module.FindComponent<P5ParadiseRegainedTowers>()?.RelSouth ?? default;
    private Actor? _source;
    private Actor? _firstTarget;
    private AOEShapeCone? _curCleave;
    private DateTime _activation;
    private bool _tetherClosest;

    public bool Active => _source != null;
    public bool TetherClosest => _tetherClosest;

    private static readonly AOEShapeCone _shapeCleaveL = new(19, 120.Degrees(), 60.Degrees()); // note: looks wrong with correct range...
    private static readonly AOEShapeCone _shapeCleaveD = new(19, 120.Degrees(), -60.Degrees());
    private static readonly AOEShapeCircle _shapeTether = new(4);

    public override void Update()
    {
        CurrentBaits.Clear();
        if (_source != null && _curCleave != null)
        {
            var cleaveTarget = NumCasts == 0 ? _firstTarget : WorldState.Actors.Find(_source.TargetID);
            if (cleaveTarget != null)
                CurrentBaits.Add(new(_source, cleaveTarget, _curCleave, _activation));
            var tetherTarget = _tetherClosest ? Raid.WithoutSlot().Closest(_source.Position) : Raid.WithoutSlot().Farthest(_source.Position);
            if (tetherTarget != null)
                CurrentBaits.Add(new(tetherTarget, tetherTarget, _shapeTether, _activation)); // +0.7s?
        }
    }

    public override void AddHints(int slot, Actor actor, TextHints hints)
    {
        if (!ForbiddenPlayers[slot] && CurrentBaits.Count == 2 && _source != null && _firstTarget != null)
        {
            var isFirstTank = actor == _firstTarget;
            if (_source.TargetID == _firstTarget.InstanceID)
                hints.Add(isFirstTank ? "Pass aggro!" : "Taunt!"); // TODO: can a tank bait both cleaves with invuln?

            var firstTankShouldBaitTether = NumCasts > 0; // TODO: can a tank bait cleave+tether with invuln?
            var shouldBaitTether = isFirstTank == firstTankShouldBaitTether;
            if (shouldBaitTether && CurrentBaits[1].Target != actor)
                hints.Add(_tetherClosest ? "Go closer!" : "Go farther!");
        }

        base.AddHints(slot, actor, hints);
    }

    public override void AddAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        if (!ForbiddenPlayers[slot])
        {
            // just go to the next safespot
            hints.AddForbiddenZone(ShapeDistance.InvertedCircle(Module.Center + SafeOffset(slot, actor), 1));
        }
        else
        {
            foreach (var b in ActiveBaitsNotOn(actor))
            {
                // after the first hit, left/right soakers have to stand in towers on the cleave edge
                if (NumCasts > 0 && b.Shape is AOEShapeCone)
                    continue;
                hints.AddForbiddenZone(b.Shape, BaitOrigin(b), b.Rotation, b.Activation);
            }
        }
    }

    public override void DrawArenaForeground(int pcSlot, Actor pc)
    {
        base.DrawArenaForeground(pcSlot, pc);

        var safeOffset = SafeOffset(pcSlot, pc);
        if (safeOffset != default)
            Arena.AddCircle(Module.Center + safeOffset, 1, ArenaColor.Safe);
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        (var shape, var closest) = (AID)spell.Action.ID switch
        {
            AID.WingsDarkAndLightDL => (_shapeCleaveD, true),
            AID.WingsDarkAndLightLD => (_shapeCleaveL, false),
            _ => (null, false)
        };
        if (shape != null)
        {
            ForbiddenPlayers = Raid.WithSlot(true).WhereActor(p => p.Role != Role.Tank).Mask();
            _source = caster;
            _firstTarget = WorldState.Actors.Find(caster.TargetID);
            _curCleave = shape;
            _activation = Module.CastFinishAt(spell, 0.3f);
            _tetherClosest = closest;
        }
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        var nextShape = (AID)spell.Action.ID switch
        {
            AID.WingsDarkAndLightCleaveLight => _shapeCleaveD,
            AID.WingsDarkAndLightCleaveDark => _shapeCleaveL,
            _ => null
        };
        if (nextShape != null)
        {
            ++NumCasts;
            _curCleave = nextShape;
            _activation = WorldState.FutureTime(3.7f);
            _tetherClosest = !_tetherClosest;
        }
    }

    private WDir SafeOffset(int slot, Actor player)
    {
        if (_relSouth == default || _curCleave == null)
            return default; // not initialized properly

        var southDir = Angle.FromDirection(_relSouth);
        if (ForbiddenPlayers[slot])
        {
            return default; // TODO: implement hints for non-tanks
        }
        else if (player == _firstTarget)
        {
            if (NumCasts == 0)
            {
                // bait cleave, so that south is safe
                return 7 * (southDir + 2 * _curCleave.DirectionOffset).ToDirection();
            }
            else
            {
                // bait tether across south
                return (_tetherClosest ? 2 : 10) * (southDir + 180.Degrees()).ToDirection();
            }
        }
        else
        {
            if (NumCasts > 0)
            {
                // bait cleave, so that north is safe
                return 7 * (southDir - _curCleave.DirectionOffset).ToDirection();
            }
            else if (_tetherClosest)
            {
                // bait tether at south
                return 2 * southDir.ToDirection();
            }
            else
            {
                // bait tether at max melee at 45 degrees
                return 10 * (southDir + 0.75f * _curCleave.DirectionOffset).ToDirection();
            }
        }
    }
}
