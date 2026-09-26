namespace BossMod.Dawntrail.Ultimate.FRU;

class P2AxeKick(BossModule module) : Components.StandardAOEs(module, AID.AxeKick, new AOEShapeCircle(16));
class P2ScytheKick(BossModule module) : Components.StandardAOEs(module, AID.ScytheKick, new AOEShapeDonut(4, 20));

class P2IcicleImpact(BossModule module) : Components.GenericAOEs(module, AID.IcicleImpact)
{
    public readonly List<AOEInstance> AOEs = []; // note: we don't remove finished aoes, since we use them in other components to detect safespots

    private static readonly AOEShapeCircle _shape = new(10);

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor) => AOEs.Skip(NumCasts);

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if (spell.Action == WatchedAction)
        {
            // initially all aoes start as non-risky
            AOEs.Add(new(_shape, caster.Position, default, Module.CastFinishAt(spell), 0, false));
        }
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        switch ((AID)spell.Action.ID)
        {
            case AID.IcicleImpact:
                ++NumCasts;
                break;
            case AID.HouseOfLight:
                // after proteans are baited, first two aoes become risky; remaining are still not - stones are supposed to be baited into them
                MarkAsRisky(0, Math.Min(2, AOEs.Count));
                break;
            case AID.FrigidStone:
                // after stones are baited, all aoes should be marked as risky
                MarkAsRisky(2, AOEs.Count);
                break;
        }
    }

    private void MarkAsRisky(int start, int end)
    {
        for (var i = start; i < end; ++i)
            AOEs.Ref(i).Risky = true;
    }
}

class P2FrigidNeedleCircle(BossModule module) : Components.StandardAOEs(module, AID.FrigidNeedleCircle, new AOEShapeCircle(5));
class P2FrigidNeedleCross(BossModule module) : Components.StandardAOEs(module, AID.FrigidNeedleCross, new AOEShapeCross(40, 2.5f));

class P2FrigidStone : Components.BaitAwayIcon
{
    public P2FrigidStone(BossModule module) : base(module, new AOEShapeCircle(5), (uint)IconID.FrigidStone, AID.FrigidStone, 8.1f, true)
    {
        EnableHints = false;
        IgnoreOtherBaits = true;
    }

    public override void AddAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints) { }
}

class P2DiamondDustHouseOfLight(BossModule module) : Components.GenericBaitAway(module, AID.HouseOfLight)
{
    private Actor? _source;
    private DateTime _activation;

    private static readonly AOEShapeCone _shape = new(60, 15.Degrees());

    public override void Update()
    {
        CurrentBaits.Clear();
        if (_source != null && ForbiddenPlayers.Any())
            foreach (var p in Raid.WithoutSlot().SortedByRange(_source.Position).Take(4))
                CurrentBaits.Add(new(_source, p, _shape, _activation));
    }

    public override void AddHints(int slot, Actor actor, TextHints hints)
    {
        if (CurrentBaits.Count == 0)
            return;

        var baitIndex = CurrentBaits.FindIndex(b => b.Target == actor);
        if (ForbiddenPlayers[slot])
        {
            if (baitIndex >= 0)
                hints.Add("Stay farther away!");
        }
        else
        {
            if (baitIndex < 0)
                hints.Add("Stay closer to bait!");
            else if (PlayersClippedBy(CurrentBaits[baitIndex]).Any())
                hints.Add("Bait cone away from raid!");
        }

        if (ActiveBaitsNotOn(actor).Any(b => IsClippedBy(actor, b)))
            hints.Add("GTFO from baited cone!");
    }

    public override void AddAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints) { }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID is AID.AxeKick or AID.ScytheKick)
        {
            _source = caster;
            _activation = Module.CastFinishAt(spell, 0.8f);
        }
    }

    public override void OnEventIcon(Actor actor, uint iconID, ulong targetID)
    {
        if (iconID == (uint)IconID.FrigidStone)
            ForbiddenPlayers.Set(Raid.FindSlot(actor.InstanceID));
    }
}

class P2DiamondDustSafespots(BossModule module) : BossComponent(module)
{
    private readonly FRUConfig _config = Service.Config.Get<FRUConfig>();
    private bool? _out;
    private bool? _supportsBaitCones;
    private bool? _conesAtCardinals;
    private readonly WDir[] _safeOffs = new WDir[PartyState.MaxPartySize];

    public override void AddAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        if (_safeOffs[slot] != default)
        {
            hints.PathfindMapBounds = FRU.PathfindHugBorderBounds;
            hints.AddForbiddenZone(ShapeDistance.PrecisePosition(Module.Center + _safeOffs[slot], new WDir(0, 1), Module.Bounds.MapResolution, actor.Position, 0.1f));
        }
    }

    public override void DrawArenaForeground(int pcSlot, Actor pc)
    {
        if (_safeOffs[pcSlot] != default)
            Arena.AddCircle(Module.Center + _safeOffs[pcSlot], 1, ArenaColor.Safe);
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        switch ((AID)spell.Action.ID)
        {
            case AID.IcicleImpact:
                if (_conesAtCardinals == null)
                {
                    _conesAtCardinals = IsCardinal(caster.Position - Module.Center);
                    InitIfReady();
                }
                break;
            case AID.AxeKick:
                _out = true;
                InitIfReady();
                break;
            case AID.ScytheKick:
                _out = false;
                InitIfReady();
                break;
        }
    }

    public override void OnCastFinished(Actor caster, ActorCastInfo spell)
    {
        switch ((AID)spell.Action.ID)
        {
            case AID.AxeKick:
                // out done => cone baiters go in, ice baiters stay
                for (var i = 0; i < _safeOffs.Length; ++i)
                    if (_safeOffs[i] != default && Raid[i]?.Class.IsSupport() == _supportsBaitCones)
                        _safeOffs[i] = 4 * _safeOffs[i].Normalized();
                break;
            case AID.ScytheKick:
                // in done => cone baiters stay, ice baiters go out
                for (var i = 0; i < _safeOffs.Length; ++i)
                    if (_safeOffs[i] != default && Raid[i]?.Class.IsSupport() != _supportsBaitCones)
                        _safeOffs[i] = 8 * _safeOffs[i].Normalized();
                break;
        }
    }

    public override void OnEventIcon(Actor actor, uint iconID, ulong targetID)
    {
        if (iconID == (uint)IconID.FrigidStone && _supportsBaitCones == null)
        {
            _supportsBaitCones = actor.Class.IsDD();
            InitIfReady();
        }
    }

    private void InitIfReady()
    {
        if (_out == null || _supportsBaitCones == null || _conesAtCardinals == null)
            return;
        var supportsAtCardinals = _supportsBaitCones == _conesAtCardinals;
        var offsetTH = supportsAtCardinals ? 0.Degrees() : _config.P2DiamondDustSupportsCCW ? 45.Degrees() : -45.Degrees();
        var offsetDD = !supportsAtCardinals ? 0.Degrees() : _config.P2DiamondDustDDCCW ? 45.Degrees() : -45.Degrees();
        foreach (var (slot, group) in _config.P2DiamondDustCardinals.Resolve(Raid))
        {
            var support = group < 4;
            var baitCone = _supportsBaitCones == support;
            var dir = 180.Degrees() - (group & 3) * 90.Degrees();
            dir += support ? offsetTH : offsetDD;
            var radius = (_out.Value ? 16 : 0) + (baitCone ? 1 : 3);
            _safeOffs[slot] = radius * dir.ToDirection();
        }
    }

    private bool IsCardinal(WDir off) => Math.Abs(off.X) < 1 || Math.Abs(off.Z) < 1;
}

class P2HeavenlyStrike(BossModule module) : Components.Knockback(module, AID.HeavenlyStrike)
{
    private readonly WDir[] _safeDirs = BuildSafeDirs(module);
    private readonly DateTime _activation = module.WorldState.FutureTime(3.9f);

    public override IEnumerable<Source> Sources(int slot, Actor actor)
    {
        yield return new(Module.Center, 12, _activation);
    }

    public override bool DestinationUnsafe(int slot, Actor actor, WPos pos)
    {
        if (base.DestinationUnsafe(slot, actor, pos))
            return true;
        var icicle = Module.FindComponent<P2IcicleImpact>();
        if (icicle != null)
            foreach (var aoe in icicle.ActiveAOEs(slot, actor))
                if (aoe.Check(pos))
                    return true;
        return false;
    }

    public override void AddAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        if (_safeDirs[slot] == default)
            return;
        hints.AddForbiddenZone(ShapeDistance.PrecisePosition(Module.Center + 6 * _safeDirs[slot], new(1, 0), Module.Bounds.MapResolution, actor.Position, 0.25f));
    }

    public override void DrawArenaForeground(int pcSlot, Actor pc)
    {
        base.DrawArenaForeground(pcSlot, pc);
        if (_safeDirs[pcSlot] != default)
            Arena.AddCircle(Module.Center + 18 * _safeDirs[pcSlot], 1, ArenaColor.Safe);
    }

    private static WDir[] BuildSafeDirs(BossModule module)
    {
        var res = new WDir[PartyState.MaxPartySize];
        var icicle = module.FindComponent<P2IcicleImpact>();
        if (icicle?.AOEs.Count > 0)
        {
            var safeDir = (icicle.AOEs[0].Origin - module.Center).Normalized();
            if (safeDir.X > 0.5f || safeDir.Z > 0.8f)
                safeDir = -safeDir; // G1
            foreach (var (slot, group) in Service.Config.Get<FRUConfig>().P2DiamondDustKnockbacks.Resolve(module.Raid))
                res[slot] = group == 1 ? -safeDir : safeDir;
        }
        return res;
    }
}

class P2SinboundHoly(BossModule module) : Components.UniformStackSpread(module, 6, 0, 4, 4)
{
    public int NumCasts;
    private DateTime _nextExplosion;
    private readonly WDir _destinationDir = CalculateDestination(module);
    private readonly List<WPos> _puddles = [];
    private readonly Actor?[] _follow = new Actor?[PartyState.MaxPartySize];

    private static WDir CalculateDestination(BossModule module)
    {
        // if oracle jumps directly to one of the initial safespots, both groups run opposite in one (arbitrary, CW) direction, and the one that ends up behind boss slides across - in that case we return zero destination
        // note: we assume that when this is called oracle is already at position
        var icicles = module.FindComponent<P2IcicleImpact>();
        var oracle = module.Enemies(OID.OraclesReflection).FirstOrDefault();
        if (icicles == null || icicles.AOEs.Count == 0 || oracle == null)
            return default;

        var idealDir = (module.Center - oracle.Position).Normalized(); // ideally we wanna stay as close as possible to across the oracle
        var destDir = (icicles.AOEs[0].Origin - module.Center).Normalized().OrthoL(); // actual destination is one of the last icicles
        return destDir.Dot(idealDir) switch
        {
            > 0.5f => destDir,
            < -0.5f => -destDir,
            _ => default, // fast movement mode
        };
    }

    public override void AddAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        // before the first puddle the stack component keeps the light parties together
        if (_puddles.Count == 0)
        {
            base.AddAIHints(slot, actor, assignment, hints);
            return;
        }

        // party stays on the healer it started with; dodging on its own is what walked people off the stack
        if (actor.Role != Role.Healer)
        {
            var master = _follow[slot];
            if (master == null || master.IsDeadOrDestroyed)
            {
                master = Raid.WithoutSlot().Where(p => p.Role == Role.Healer).MinBy(h => (h.Position - actor.Position).LengthSq());
                _follow[slot] = master;
            }
            if (master != null)
                hints.AddForbiddenZone(ShapeDistance.InvertedCircle(master.Position, 3));
            return;
        }

        // healer: one step past the puddle, then wait. the next puddle drops on them
        var radial = actor.Position - Module.Center;
        var radialDir = radial.LengthSq() > 1 ? radial.Normalized() : new WDir(0, -1);
        var dir = _destinationDir != default ? _destinationDir : radialDir.OrthoR();

        var hintTime = WorldState.FutureTime(50);
        hints.AddForbiddenZone(ShapeDistance.Circle(Module.Center, 16), hintTime);
        foreach (var p in _puddles)
            hints.AddForbiddenZone(ShapeDistance.Circle(p, 6));

        var along = dir.Dot(radial);
        var anchor = float.MinValue;
        var covered = false;
        foreach (var p in _puddles)
        {
            if ((actor.Position - p).LengthSq() >= 36)
                continue;
            covered = true;
            anchor = MathF.Max(anchor, dir.Dot(p - Module.Center));
        }
        var plane = covered ? anchor + 7 : along - 1;
        hints.AddForbiddenZone(ShapeDistance.HalfPlane(Module.Center + plane * dir, dir), covered ? default : hintTime);
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID == AID.SinboundHoly)
        {
            AddStacks(Raid.WithoutSlot().Where(p => p.Role == Role.Healer), Module.CastFinishAt(spell, 0.9f));
        }
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if ((AID)spell.Action.ID == AID.SinboundHolyAOE)
        {
            _puddles.Add(spell.TargetPos.ToWPos());
            if (WorldState.CurrentTime > _nextExplosion)
            {
                ++NumCasts;
                _nextExplosion = WorldState.FutureTime(0.5f);
            }
        }
    }
}

class P2SinboundHolyVoidzone(BossModule module) : Components.Voidzone(module, 6, OID.SinboundHolyVoidzone)
{
    public bool AIHintsEnabled = true;

    public override void AddAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        if (AIHintsEnabled)
            base.AddAIHints(slot, actor, assignment, hints);
    }
}

class P2ShiningArmor(BossModule module) : Components.GenericGaze(module, AID.ShiningArmor)
{
    private Actor? _source;
    private DateTime _activation;

    public override IEnumerable<Eye> ActiveEyes(int slot, Actor actor)
    {
        if (_source != null)
            yield return new(_source.Position, _activation);
    }

    public override void OnActorPlayActionTimelineEvent(Actor actor, ushort id)
    {
        if ((OID)actor.OID == OID.BossP2 && id == 0x1E43)
        {
            _source = actor;
            _activation = WorldState.FutureTime(7.2f);
        }
    }
}

class P2TwinStillnessSilence(BossModule module) : Components.GenericAOEs(module)
{
    public readonly List<AOEInstance> AOEs = [];
    private readonly Actor? _source = module.Enemies(OID.OraclesReflection).FirstOrDefault();
    private BitMask _thinIce;
    private readonly WPos[] _slideBackPos = new WPos[PartyState.MaxPartySize]; // used for hints only
    private P2SinboundHolyVoidzone? _voidzones; // used for hints only

    private const float SlideDistance = 32;
    private readonly AOEShapeCone _shapeFront = new(30, 135.Degrees());
    private readonly AOEShapeCone _shapeBack = new(30, 45.Degrees());

    public void EnableAIHints()
    {
        _voidzones = Module.FindComponent<P2SinboundHolyVoidzone>();
    }

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor) => AOEs.Take(1);

    public override void Update()
    {
        if (AOEs.Count != 2)
            return;
        foreach (var (i, p) in Raid.WithSlot().IncludedInMask(_thinIce))
            if (_slideBackPos[i] == default && p.LastFrameMovement != default)
                _slideBackPos[i] = p.PrevPosition;
    }

    // full 32y slide, or null to stand. a shorter correction is still a slide into the wall
    public WPos? SlideTarget(int slot, Actor actor)
    {
        if (_voidzones == null || _source == null || !_thinIce[slot] || actor.LastFrameMovement != default)
            return null;

        var sourceOffset = _source.Position - Module.Center;
        var needToMove = AOEs.Count > 0 ? AOEs[0].Check(actor.Position) : NumCasts == 0 && sourceOffset.Dot(actor.Position - Module.Center) > 0;
        if (!needToMove)
            return null;

        var zoneList = new ArcList(actor.Position, SlideDistance);
        zoneList.ForbidInverseCircle(Module.Center, Module.Bounds.Radius);

        foreach (var z in _voidzones.Sources)
        {
            var offset = z.Position - actor.Position;
            var dist = offset.Length();
            if (dist >= SlideDistance)
                zoneList.ForbidCircle(z.Position, _voidzones.Shape.Radius);
            else if (dist >= _voidzones.Shape.Radius)
                zoneList.ForbidArcByLength(Angle.FromDirection(offset), Angle.Asin(_voidzones.Shape.Radius / dist));
        }

        if (AOEs.Count == 0)
        {
            var farthestDir = Angle.FromDirection(-sourceOffset);
            var bestRange = zoneList.Allowed(5.Degrees()).MinBy(r => farthestDir.DistanceToRange(r.min, r.max).Abs().Rad);
            var dir = farthestDir.ClosestInRange(bestRange.min, bestRange.max);
            return actor.Position + SlideDistance * dir.ToDirection();
        }

        ref var nextAOE = ref AOEs.Ref(0);
        zoneList.ForbidInfiniteCone(nextAOE.Origin, nextAOE.Rotation, ((AOEShapeCone)nextAOE.Shape).HalfAngle);

        if (AOEs.Count == 1 && _slideBackPos[slot] != default && !zoneList.Forbidden.Contains(Angle.FromDirection(_slideBackPos[slot] - actor.Position).Rad))
            return _slideBackPos[slot];

        if (zoneList.Allowed(1.Degrees()).MaxBy(r => (r.max - r.min).Rad) is var best && best.max.Rad > best.min.Rad)
        {
            var dir = 0.5f * (best.min + best.max);
            return actor.Position + SlideDistance * dir.ToDirection();
        }
        return null;
    }

    public override void AddAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        if (_voidzones == null || _source == null)
            return;

        if (!_thinIce[slot])
        {
            // preposition
            // this is a bit hacky - we need to stay either far away from boss, or close (and slide over at the beginning of the ice)
            // the actual shape is quite complicated ('primary' shape is a set of points at distance X from a cone behind boss, 'secondary' is a set of points at distance X from primary), so we use a rough approximation

            // first, find a set of allowed angles along the border
            var zoneList = new ArcList(Module.Center, 17);
            foreach (var z in _voidzones.Sources)
                zoneList.ForbidCircle(z.Position, _voidzones.Shape.Radius);

            // now find closest allowed zone
            var actorDir = Angle.FromDirection(actor.Position - Module.Center);
            var closest = zoneList.Allowed(5.Degrees()).MinBy(z => actorDir.DistanceToRange(z.min, z.max).Abs().Rad);
            if (closest != default)
            {
                // already in a safe gap: do not walk to the middle of it. that step is what the ice turns into a slide
                if (actorDir.DistanceToRange(closest.min, closest.max).Abs().Rad < 0.05f)
                    return;

                var desiredDir = (closest.min + closest.max) * 0.5f;
                var halfWidth = (closest.max - closest.min) * 0.5f;
                hints.AddForbiddenZone(ShapeDistance.InvertedCone(Module.Center, 100, desiredDir, halfWidth), DateTime.MaxValue);
            }
        }
        else if (SlideTarget(slot, actor) is var slide && slide != null)
            hints.AddForbiddenZone(ShapeDistance.InvertedCircle(slide.Value, 1), DateTime.MaxValue);
    }

    public override void DrawArenaForeground(int pcSlot, Actor pc)
    {
        Arena.Actor(_source, ArenaColor.Object, true);
        if (_thinIce[pcSlot])
        {
            Arena.AddCircle(pc.Position, SlideDistance, ArenaColor.Vulnerable);
            Arena.AddLine(pc.Position, pc.Position - SlideDistance * WorldState.Client.CameraAzimuth.ToDirection(), ArenaColor.Vulnerable);
        }
    }

    public override void OnStatusGain(Actor actor, in ActorStatus status)
    {
        if ((SID)status.ID == SID.ThinIce)
            _thinIce.Set(Raid.FindSlot(actor.InstanceID));
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        var (shape1, off1, shape2, off2) = (AID)spell.Action.ID switch
        {
            AID.TwinStillnessFirst => (_shapeFront, 0.Degrees(), _shapeBack, 180.Degrees()),
            AID.TwinSilenceFirst => (_shapeBack, 0.Degrees(), _shapeFront, 180.Degrees()),
            _ => (null, default, null, default)
        };
        if (shape1 != null && shape2 != null)
        {
            AOEs.Add(new(shape1, caster.Position, spell.Rotation + off1, Module.CastFinishAt(spell)));
            AOEs.Add(new(shape2, caster.Position, spell.Rotation + off2, Module.CastFinishAt(spell, 2.1f)));
        }
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if ((AID)spell.Action.ID is AID.TwinStillnessFirst or AID.TwinStillnessSecond or AID.TwinSilenceFirst or AID.TwinSilenceSecond)
        {
            ++NumCasts;
            if (AOEs.Count > 0)
                AOEs.RemoveAt(0);
        }
    }
}
