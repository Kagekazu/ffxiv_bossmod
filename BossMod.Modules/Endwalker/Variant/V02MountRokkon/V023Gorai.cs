namespace BossMod.Endwalker.Variant.V02MountRokkon.V023Gorai;

public enum OID : uint
{
    Boss = 0x3F5C, // R3.450, x1
    ShishuWhiteBaboon = 0x3F5D, // R1.020, x0 (spawn during fight)
    BallOfLevin = 0x3F60, // R0.69-2.3, x0 (spawn during fight)
    FlameAndSulphurFlame = 0x1EB893, // R0.500, EventObj type
    FlameAndSulphurRock = 0x1EB894, // R0.500, EventObj type
    Helper = 0x233C, // R0.500, Helper type
}

public enum AID : uint
{
    AutoAttack = 34004, // Boss->player, no cast, single-target
    Teleport = 34003, // Boss->location, no cast, single-target
    FlickeringFlame = 34005, // Boss->self, 3.0s cast, single-target, summons flames
    SulphuricStone = 34006, // Boss->self, 3.0s cast, single-target, summons stones
    FlameAndSulphur = 34007, // Boss->self, 3.0s cast, single-target, summons both
    BrazenBalladExpanding = 34008, // Boss->self, 5.0s cast, single-target, flame lines widen, stones become circles
    BrazenBalladSplitting = 34009, // Boss->self, 5.0s cast, single-target, flame lines split, stones become donuts
    FireSpreadExpand = 34010, // Helper->self, no cast, range 46 width 10 rect
    FireSpreadSplit = 34011, // Helper->self, no cast, range 46 width 5 rect
    FallingRockExpand = 34013, // Helper->self, no cast, range 11 circle
    FallingRockSplit = 34014, // Helper->self, no cast, range 5-16 donut
    Unenlightenment = 34048, // Boss->self, 5.0s cast, single-target
    UnenlightenmentAOE = 34049, // Helper->self, no cast, range 60 circle, raidwide
    ImpurePurgation = 34022, // Boss->self, 3.6s cast, single-target
    ImpurePurgationFirst = 34023, // Helper->self, 4.0s cast, range 60 45-degree cone
    ImpurePurgationSecond = 34024, // Helper->self, 6.0s cast, range 60 45-degree cone
    MalformedPrayer = 34017, // Boss->self, 4.0s cast, single-target, three sequential towers
    Burst = 34018, // Helper->self, no cast, range 4 circle, tower
    DramaticBurst = 34019, // Helper->self, no cast, range 60 circle, tower fail
    RousingReincarnation1 = 34015, // Boss->self, 5.0s cast, single-target
    RousingReincarnation2 = 34016, // Helper->player, no cast, single-target
    SpikeOfFlameVisual = 34020, // Boss->self, 2.6s cast, single-target
    SpikeOfFlameAOE = 34021, // Helper->location, 3.0s cast, range 5 circle, baited on each player
    StringSnapVisual = 34025, // Boss->self, 2.6s cast, single-target
    StringSnap1 = 34026, // Helper->self, 3.0s cast, range 10 circle
    StringSnap2 = 34027, // Helper->self, 5.0s cast, range 10-20 donut
    StringSnap3 = 34028, // Helper->self, 7.0s cast, range 20-30 donut
    TorchingTormentVisual = 34046, // Boss->player, 5.0s cast, single-target
    TorchingTorment = 34047, // Helper->player, no cast, range 6 circle, tankbuster
    PureShockVisual = 34029, // Boss->self, 2.5s cast, single-target, route 5
    PureShock = 34030, // Helper->self, 3.0s cast, range 40 circle, raidwide
    Pull = 34031, // Helper->player, no cast, single-target, pulls players into octagon
    WilyWall = 34032, // ShishuWhiteBaboon->self, 7.0s cast, single-target
    SelfDestruct = 34033, // ShishuWhiteBaboon->self, 30.0s cast, range 10 circle
    HumbleHammerVisual = 34038, // Boss->self, 5.0s cast, single-target, route 6
    HumbleHammer = 34039, // Helper->location, 3.0s cast, range 3 circle
    Thundercall = 34034, // Boss->self, 3.0s cast, single-target
    ShockLargeVisual = 34012, // BallOfLevin->self, 6.0s cast, range 18 circle
    ShockSmallVisual = 34035, // BallOfLevin->self, 17.0s cast, range 8 circle
    ShockSmall = 34036, // BallOfLevin->self, no cast, range 8 circle
    ShockLarge = 34037, // BallOfLevin->self, no cast, range 8 circle
    WorldlyPursuitFirstCCW = 34042, // Boss->self, 5.0s cast, range 60 width 20 cross, route 7
    WorldlyPursuitFirstCW = 34043, // Boss->self, 5.0s cast, range 60 width 20 cross
    WorldlyPursuitRest = 34044, // Boss->self, 1.5s cast, range 60 width 20 cross
    FightingSpiritsVisual = 34040, // Boss->self, 5.0s cast, single-target
    FightingSpirits = 34041, // Helper->self, 6.2s cast, range 30 circle, raidwide
    BiwaBreakerFirst = 34045, // Boss->self, 4.0s cast, range 30 circle, raidwide x5
    BiwaBreakerRest = 34513, // Boss->self, no cast, range 30 circle
}

public enum SID : uint
{
    SmallOrb = 2970, // Helper->BallOfLevin, extra=0x261
}

public enum IconID : uint
{
    Tankbuster = 344, // player
}

class ArenaShrink(BossModule module) : OuterDeathwall(module, AID.Unenlightenment, 0.5f, new AOEShapeRect(20, 20, 20), true, new ArenaBoundsSquare(20), 0x00020001, 0x16);

class OctagonGaps(BossModule module) : Components.GenericAOEs(module)
{
    private const byte _firstIndex = 0x3D;
    private const float _quadrantOffset = 10;
    private static readonly AOEShapeDonut _gap = new(7.3f, 9.7f);
    private BitMask _raised;

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor)
    {
        foreach (var q in _raised.SetBits())
            yield return new(_gap, QuadrantCenter(q));
    }

    public override void OnMapEffect(byte index, uint state)
    {
        var quadrant = index - _firstIndex;
        if (quadrant is < 0 or > 3 || state == 0x00020001)
            return;
        _raised[quadrant] = state == 0x00200010;
    }

    // NW, NE, SW, SE
    private WPos QuadrantCenter(int quadrant)
        => Module.Center + new WDir((quadrant & 1) == 0 ? -_quadrantOffset : _quadrantOffset, quadrant < 2 ? -_quadrantOffset : _quadrantOffset);
}

class FlameAndSulphur(BossModule module) : Components.GenericAOEs(module)
{
    private static readonly AOEShapeRect _flameExpand = new(46, 5);
    private static readonly AOEShapeRect _flameSplit = new(46, 2.5f);
    private static readonly AOEShapeCircle _rockExpand = new(11);
    private static readonly AOEShapeDonut _rockSplit = new(5, 16);
    private readonly List<AOEInstance> _aoes = [];

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor) => _aoes;

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        var activation = Module.CastFinishAt(spell, 3.1f);
        switch ((AID)spell.Action.ID)
        {
            case AID.BrazenBalladExpanding:
                foreach (var f in Module.Enemies(OID.FlameAndSulphurFlame))
                    _aoes.Add(new(_flameExpand, f.Position, f.Rotation, activation));
                foreach (var r in Module.Enemies(OID.FlameAndSulphurRock))
                    _aoes.Add(new(_rockExpand, r.Position, r.Rotation, activation));
                break;
            case AID.BrazenBalladSplitting:
                foreach (var f in Module.Enemies(OID.FlameAndSulphurFlame))
                {
                    var offset = 7.5f * f.Rotation.ToDirection().OrthoL();
                    _aoes.Add(new(_flameSplit, f.Position + offset, f.Rotation, activation));
                    _aoes.Add(new(_flameSplit, f.Position - offset, f.Rotation, activation));
                }
                foreach (var r in Module.Enemies(OID.FlameAndSulphurRock))
                    _aoes.Add(new(_rockSplit, r.Position, r.Rotation, activation));
                break;
        }
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if ((AID)spell.Action.ID is AID.FireSpreadExpand or AID.FireSpreadSplit or AID.FallingRockExpand or AID.FallingRockSplit)
            _aoes.Clear();
    }
}

class ImpurePurgation(BossModule module) : Components.GroupedAOEs(module, [AID.ImpurePurgationFirst, AID.ImpurePurgationSecond], new AOEShapeCone(60, 22.5f.Degrees()), 4);

class MalformedPrayer(BossModule module) : Components.GenericTowers(module)
{
    private const byte _firstIndex = 0x1F;
    private const float _ringRadius = 11;
    private ImpurePurgation? _purgation;

    public override void OnMapEffect(byte index, uint state)
    {
        var spot = index - _firstIndex;
        if (state != 0x00020001 || spot is < 0 or > 7)
            return;
        var offset = (180 - 45 * spot).Degrees().ToDirection() * _ringRadius;
        Towers.Add(new(Module.Center + offset, 4, activation: WorldState.FutureTime(9)));
        RefreshSoakers();
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if ((AID)spell.Action.ID != AID.Burst || Towers.Count == 0)
            return;
        ++NumCasts;
        Towers.RemoveAt(0);
        RefreshSoakers();
    }

    public override void AddAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        _purgation ??= Module.FindComponent<ImpurePurgation>();
        if (_purgation is { Casters.Count: > 0 } && Towers.Count > 0 && !Towers[0].IsInside(actor))
            hints.AddForbiddenZone(ShapeDistance.InvertedCircle(Module.Center, 3));
        else
            base.AddAIHints(slot, actor, assignment, hints);
    }

    private void RefreshSoakers()
    {
        for (var i = 0; i < Towers.Count; ++i)
        {
            var tower = Towers[i];
            (tower.MinSoakers, tower.MaxSoakers) = i == 0 ? (1, 4) : (0, 0);
            Towers[i] = tower;
        }
    }
}

class Thundercall(BossModule module) : Components.GenericAOEs(module)
{
    private static readonly AOEShapeCircle _small = new(8);
    private static readonly AOEShapeCircle _large = new(18);
    private readonly List<(WPos Position, bool Shrunk, DateTime Activation)> _pending = [];
    private int _orbsSeen;

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor)
    {
        foreach (var p in _pending)
            yield return new(p.Shrunk ? _small : _large, p.Position, default, p.Activation);
    }

    public override void OnActorCreated(Actor actor)
    {
        if ((OID)actor.OID == OID.BallOfLevin && _orbsSeen++ < 2)
            _pending.Add((actor.Position, false, WorldState.FutureTime(10.8f)));
    }

    public override void OnStatusGain(Actor actor, in ActorStatus status)
    {
        if ((SID)status.ID != SID.SmallOrb || (OID)actor.OID != OID.BallOfLevin)
            return;
        var activation = WorldState.FutureTime(10);
        foreach (var orb in Module.Enemies(OID.BallOfLevin))
            _pending.Add((orb.Position, orb == actor, activation));
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if ((AID)spell.Action.ID is AID.ShockSmall or AID.ShockLarge)
        {
            ++NumCasts;
            _pending.Clear();
        }
    }
}

class WorldlyPursuit(BossModule module) : Components.GenericRotatingAOE(module)
{
    private static readonly AOEShapeCross _cross = new(60, 10);
    private const float _turnDeg = 22.5f;
    private const float _interval = 3.7f;
    private const int _hits = 5;

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        var sign = (AID)spell.Action.ID switch
        {
            AID.WorldlyPursuitFirstCCW => 1,
            AID.WorldlyPursuitFirstCW => -1,
            _ => 0
        };
        if (sign != 0)
            Sequences.Add(new(_cross, spell.LocXZ, spell.Rotation, (_turnDeg * sign).Degrees(), Module.CastFinishAt(spell), _interval, _hits, 1));
    }

    public override void OnCastFinished(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID is not AID.WorldlyPursuitFirstCCW and not AID.WorldlyPursuitFirstCW and not AID.WorldlyPursuitRest || Sequences.Count == 0)
            return;
        if (!AdvanceSequence(Sequences[0].Origin, spell.Rotation, WorldState.CurrentTime))
            AdvanceSequence(0, WorldState.CurrentTime);
    }
}

class StringSnap(BossModule module) : ExpandingRings(module, AID.StringSnap1, new AOEShapeCircle(10), new AOEShapeDonut(10, 20), new AOEShapeDonut(20, 30));

class Unenlightenment(BossModule module) : Components.RaidwideCastDelay(module, AID.Unenlightenment, AID.UnenlightenmentAOE, 0.5f);
class SpikeOfFlame(BossModule module) : Components.StandardAOEs(module, AID.SpikeOfFlameAOE, new AOEShapeCircle(5));
class TorchingTorment(BossModule module) : Components.BaitAwayIcon(module, new AOEShapeCircle(6), (uint)IconID.Tankbuster, AID.TorchingTorment, 5.9f, true, AIHints.PredictedDamageType.Tankbuster);
class PureShock(BossModule module) : Components.RaidwideCast(module, AID.PureShock);
class SelfDestruct(BossModule module) : Components.StandardAOEs(module, AID.SelfDestruct, new AOEShapeCircle(10));
class HumbleHammer(BossModule module) : Components.StandardAOEs(module, AID.HumbleHammer, new AOEShapeCircle(3));
class FightingSpirits(BossModule module) : Components.RaidwideCast(module, AID.FightingSpirits);
class BiwaBreaker(BossModule module) : Components.RaidwideCast(module, AID.BiwaBreakerFirst, "Raidwide x5");

class V023GoraiStates : StateMachineBuilder
{
    public V023GoraiStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<ArenaShrink>()
            .ActivateOnEnter<OctagonGaps>()
            .ActivateOnEnter<Unenlightenment>()
            .ActivateOnEnter<TorchingTorment>()
            .ActivateOnEnter<FightingSpirits>()
            .ActivateOnEnter<BiwaBreaker>()
            .ActivateOnEnter<StringSnap>()
            .ActivateOnEnter<SpikeOfFlame>()
            .ActivateOnEnter<FlameAndSulphur>()
            .ActivateOnEnter<MalformedPrayer>()
            .ActivateOnEnter<ImpurePurgation>()
            .ActivateOnEnter<PureShock>()
            .ActivateOnEnter<SelfDestruct>()
            .ActivateOnEnter<HumbleHammer>()
            .ActivateOnEnter<Thundercall>()
            .ActivateOnEnter<WorldlyPursuit>();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 945, NameID = 12373, SortOrder = 5)]
public class V023Gorai(ModuleInit init) : BossModule(init, new(741, -190), new ArenaBoundsSquare(22.5f))
{
    protected override void DrawEnemies(int pcSlot, Actor pc)
    {
        Arena.Actor(PrimaryActor, ArenaColor.Enemy);
        Arena.Actors(Enemies(OID.ShishuWhiteBaboon), ArenaColor.Enemy);
    }

    protected override void CalculateModuleAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        hints.PrioritizeTargetsByOID(OID.ShishuWhiteBaboon, 1);
    }
}
