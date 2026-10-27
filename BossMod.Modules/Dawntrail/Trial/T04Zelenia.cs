namespace BossMod.Dawntrail.Trial.T04Zelenia;

public enum OID : uint
{
    Boss = 0x47C3, // R5.500, x1
    Helper = 0x233C, // R0.500, x?, Helper type
    BriarThorn1 = 0x47C4, // R1.000, x0 (spawn during fight)
    BriarThorn2 = 0x47C5, // R0.500, x0 (spawn during fight)
    RosebloodDrop = 0x485B, // R1.000, x0 (spawn during fight)
}

public enum AID : uint
{
    AutoAttack = 43055, // Boss->player, no cast, single-target
    Teleport = 43054, // Boss->location, no cast, single-target

    AlexandrianThunderIVVisual = 43480, // Boss->self, 6.0+0.7s cast, single-target
    AlexandrianThunderIVCircle1 = 43084, // Helper->self, 6.7s cast, range 8 circle
    AlexandrianThunderIVDonut1 = 43085, // Helper->self, 6.7s cast, range 8-24 donut
    AlexandrianThunderIVCircle2 = 43446, // Helper->self, 6.7s cast, range 8 circle
    AlexandrianThunderIVDonut2 = 43447, // Helper->self, 6.7s cast, range 8-24 donut

    ShockVisual = 43056, // Boss->self, 3.0s cast, single-target, visual (spreads)
    ShockLock = 43057, // Helper->self, no cast, single-target, locks voidzone
    Shock1 = 43058, // Helper->self, no cast, range 4 circle
    Shock2 = 43059, // Helper->self, no cast, range 4 circle
    Shock3 = 43060, // Helper->self, no cast, range 4 circle
    Shock4 = 43061, // Helper->self, no cast, range 4 circle
    Shock5 = 43062, // Helper->self, no cast, range 4 circle
    Shock6 = 43063, // Helper->self, no cast, range 4 circle

    PowerBreak1 = 43112, // Boss->self, 6.0s cast, range 24 width 64 rect
    PowerBreak2 = 43113, // Boss->self, 6.0s cast, range 24 width 64 rect

    HolyHazardVisual1 = 43124, // Boss->self, 7.0+0.7s cast, single-target, visual
    HolyHazardVisual2 = 43122, // Boss->self, 7.0+0.7s cast, single-target, visual
    HolyHazardVisual3 = 43120, // Boss->self, 7.0+0.7s cast, single-target, visual
    HolyHazardVisual4 = 43125, // Boss->self, 7.0+0.7s cast, single-target, visual
    HolyHazardVisual5 = 43121, // Boss->self, 7.0+0.7s cast, single-target, visual
    HolyHazardVisual6 = 43123, // Boss->self, 7.0+0.7s cast, single-target, visual
    HolyHazardVisual7 = 43065, // Boss->self, no cast, single-target, visual
    HolyHazard = 43126, // Helper->self, 7.7s cast, range 24 120-degree cone

    SpecterOfTheLostVisual = 43128, // Boss->self, 5.0+0.7s cast, single-target, visual (tank cones)
    SpecterOfTheLost = 43129, // Helper->player, 5.7s cast, range 50 45-degree cone

    ThunderSlashVisual1 = 43078, // Boss->self, 6.0+0.7s cast, single-target, visual
    ThunderSlashVisual2 = 43079, // Boss->self, 6.0+0.7s cast, single-target, visual
    ThunderSlash = 43083, // Helper->self, 6.7s cast, range 24 60-degree cone

    RosebloodBloomVisual1 = 43093, // Boss->self, 3.9s cast, range 50 circle, visual
    RosebloodBloomVisual2 = 43091, // Boss->self, 2.6+0.4s cast, single-target, visual
    RosebloodBloom = 43479, // Helper->self, 5.0s cast, range 50 circle, knockback 10 (ignore immunes)

    PerfumedQuietusVisual = 43094, // Boss->self, 4.0s cast, single-target, visual
    PerfumedQuietus = 43095, // Helper->self, 25.1s cast, range 50 circle, phase-transition raidwide (during cutscene stun)

    QueensCrusade = 43092, // Helper->self, 3.7s cast, range 4 circle, arena shrink telegraph
    AlexandrianThunderIIIVisual = 43100, // Boss->self, 3.0s cast, single-target, visual
    AlexandrianThunderIII1 = 43102, // Helper->location, 7.0s cast, range 4 circle + connected tile cones
    AlexandrianThunderIII2 = 43439, // Helper->location, 6.0s cast, range 4 circle + connected tile cones

    StockBreakVisual = 43086, // Boss->self, 7.0s cast, single-target, visual (stack, 4 hits)
    StockBreak1 = 43087, // Helper->location, no cast, range 6 circle
    StockBreak2 = 43088, // Helper->location, no cast, range 6 circle
    StockBreak3 = 43089, // Helper->location, no cast, range 6 circle
    StockBreak4 = 43090, // Helper->location, no cast, range 6 circle

    RosebloodDropSpawn = 43442, // Boss->self, 3.0s cast, single-target, visual
    RosebloodDropAOE = 43443, // RosebloodDrop->self, no cast, donut segment

    ValorousAscension = 43070, // Boss->self, 3.0s cast, single-target, visual (raidwide x3)
    ValorousAscension1 = 43071, // Helper->self, 3.7s cast, range 50 circle
    ValorousAscension2 = 43072, // Helper->self, no cast, range 50 circle
    ValorousAscension3 = 43073, // Helper->self, no cast, range 50 circle
    ValorousAscensionRect = 43074, // BriarThorn1->self, 5.0s cast, range 40 width 8 rect
    ThornedCatharsis = 43127, // Boss->self, 5.0s cast, range 50 circle, raidwide
}

public enum IconID : uint
{
    Shock = 581, // player
    StockBreak = 590, // player
}

class AlexandrianThunderIV(BossModule module) : Components.GenericAOEs(module)
{
    private static readonly AOEShapeCircle _in = new(8);
    private static readonly AOEShapeDonut _out = new(8, 24);
    private readonly List<(Actor Caster, AOEShape Shape)> _casts = [];

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor)
    {
        var next = DateTime.MaxValue;
        foreach (var (caster, _) in _casts)
        {
            if (caster.CastInfo is { } cast && Module.CastFinishAt(cast) is var at && at < next)
                next = at;
        }

        foreach (var (caster, shape) in _casts)
        {
            if (caster.CastInfo is not { } cast)
                continue;
            var at = Module.CastFinishAt(cast);
            if (at <= next.AddSeconds(1))
                yield return new(shape, cast.LocXZ, default, at);
        }
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if (ShapeFor(spell.Action) is { } shape)
            _casts.Add((caster, shape));
    }

    public override void OnCastFinished(Actor caster, ActorCastInfo spell)
    {
        if (ShapeFor(spell.Action) != null)
        {
            ++NumCasts;
            _casts.RemoveAll(c => c.Caster == caster);
        }
    }

    private static AOEShape? ShapeFor(ActionID action) => (AID)action.ID switch
    {
        AID.AlexandrianThunderIVCircle1 or AID.AlexandrianThunderIVCircle2 => _in,
        AID.AlexandrianThunderIVDonut1 or AID.AlexandrianThunderIVDonut2 => _out,
        _ => null
    };
}

class ShockBait(BossModule module) : Components.BaitAwayIcon(module, new AOEShapeCircle(4), (uint)IconID.Shock, AID.ShockLock, 8, centerAtTarget: true);

class ShockPuddles(BossModule module) : Components.GenericAOEs(module)
{
    private static readonly AOEShapeCircle _shape = new(4);
    private const int _cyclesPerPuddle = 2; // each puddle goes through Shock1-6 twice
    private readonly List<(WPos Origin, int Cycles)> _puddles = [];

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor)
    {
        foreach (var p in _puddles)
            yield return new(_shape, p.Origin);
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        switch ((AID)spell.Action.ID)
        {
            case AID.ShockLock:
                _puddles.Add((caster.Position, 0));
                break;
            case AID.Shock6:
                ++NumCasts;
                FinishCycle(caster.Position);
                break;
        }
    }

    private void FinishCycle(WPos pos)
    {
        if (_puddles.Count == 0)
            return;

        // fall back to the puddle that is furthest behind
        var index = _puddles.FindIndex(p => p.Origin.AlmostEqual(pos, 1));
        if (index < 0)
        {
            index = 0;
            for (var i = 1; i < _puddles.Count; ++i)
                if (_puddles[i].Cycles < _puddles[index].Cycles)
                    index = i;
        }

        var (origin, cycles) = _puddles[index];
        if (++cycles >= _cyclesPerPuddle)
            _puddles.RemoveAt(index);
        else
            _puddles[index] = (origin, cycles);
    }
}

class PowerBreak(BossModule module) : Components.GroupedAOEs(module, [AID.PowerBreak1, AID.PowerBreak2], new AOEShapeRect(24, 32));
class HolyHazard(BossModule module) : Components.StandardAOEs(module, AID.HolyHazard, new AOEShapeCone(24, 60.Degrees()), maxCasts: 2);
class SpecterOfTheLost(BossModule module) : Components.BaitAwayCast(module, AID.SpecterOfTheLost, new AOEShapeCone(50, 22.5f.Degrees()));
class ThunderSlash(BossModule module) : Components.StandardAOEs(module, AID.ThunderSlash, new AOEShapeCone(24, 30.Degrees()), maxCasts: 4);

class RosebloodBloom(BossModule module) : Components.KnockbackFromCastTarget(module, AID.RosebloodBloom, 10, ignoreImmunes: true)
{
    public override void AddAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        var safeRadius = Arena.Bounds.Radius - Distance;
        foreach (var c in Casters)
            hints.AddForbiddenZone(ShapeDistance.InvertedCircle(c.Position, safeRadius), Module.CastFinishAt(c.CastInfo));
    }
}

// TODO: Quietus resolves during cutscene stun — warn on Bloom knockback instead
class PerfumedQuietus(BossModule module) : Components.RaidwideCast(module, AID.RosebloodBloom);
class ThornedCatharsis(BossModule module) : Components.RaidwideCast(module, AID.ThornedCatharsis);
class ValorousAscension(BossModule module) : Components.RaidwideMultiHit(module, AID.ValorousAscension1, [AID.ValorousAscension1, AID.ValorousAscension2, AID.ValorousAscension3], 0, 0.95f, 3);

class StockBreak(BossModule module) : Components.UniformStackSpread(module, 6, 0, 8, 8, stackHits: 4)
{
    private static readonly AID[] _hitOrder = [AID.StockBreak1, AID.StockBreak2, AID.StockBreak3, AID.StockBreak4];

    public override void OnEventIcon(Actor actor, uint iconID, ulong targetID)
    {
        if (iconID == (uint)IconID.StockBreak)
            AddStack(actor, WorldState.FutureTime(8.2f));
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        var hit = Array.IndexOf(_hitOrder, (AID)spell.Action.ID);
        if (hit < 0)
            return;

        var hitsLeft = _hitOrder.Length - 1 - hit;
        if (hitsLeft == 0)
        {
            Stacks.Clear();
            return;
        }

        for (var i = 0; i < Stacks.Count; ++i)
        {
            var stack = Stacks[i];
            stack.Hits = hitsLeft;
            stack.Activation = WorldState.FutureTime(1.1f);
            Stacks[i] = stack;
        }
    }
}

class CenterVoid(BossModule module) : Components.GenericAOEs(module, AID.QueensCrusade)
{
    private static readonly AOEShapeCircle _hole = new(4);
    private DateTime _telegraph;

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor)
    {
        if (_telegraph != default)
            yield return new(_hole, Arena.Center, Activation: _telegraph);
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if (spell.Action == WatchedAction)
            _telegraph = Module.CastFinishAt(spell, 0.1f);
    }

    public override void OnMapEffect(byte index, uint state)
    {
        if (index != 1)
            return;

        if (state == 0x00020001)
        {
            _telegraph = default;
            Arena.Bounds = T04Zelenia.DonutArena;
        }
        else if (state == 0x00080004)
        {
            Arena.Bounds = T04Zelenia.DefaultArena;
        }
    }
}

// tile k is driven by map effect 4+k and faces 180+60k degrees
class RoseTiles(BossModule module) : BossComponent(module)
{
    public const int NumTiles = 6;
    public static readonly AOEShapeCone Slice = new(16, 30.Degrees());

    public BitMask Lit;

    public static Angle Direction(int tile) => (180 + 60 * tile).Degrees();

    public override void OnMapEffect(byte index, uint state)
    {
        var tile = index - 4;
        if (tile is < 0 or >= NumTiles)
            return;

        if (state == 0x00400100)
            Lit.Set(tile);
        else if (state == 0x00040020)
            Lit.Clear(tile);
    }

    public override void OnActorPlayActionTimelineEvent(Actor actor, ushort id)
    {
        if (id == 0x1E46 && TileFromSpawnRotation(actor.Rotation) is var tile && tile >= 0)
            Lit.Set(tile);
    }

    public BitMask ConnectedTo(int tile)
    {
        var group = new BitMask();
        if (!Lit[tile])
            return group;

        group.Set(tile);
        for (var t = (tile + 1) % NumTiles; t != tile && Lit[t]; t = (t + 1) % NumTiles)
            group.Set(t);
        for (var t = (tile + NumTiles - 1) % NumTiles; t != tile && Lit[t]; t = (t + NumTiles - 1) % NumTiles)
            group.Set(t);
        return group;
    }

    // spawn rotations seen in logs
    private static int TileFromSpawnRotation(Angle rotation) => (int)MathF.Round(rotation.Deg) switch
    {
        180 or 143 or -143 => 0,
        -120 or -156 or -83 => 1,
        -60 or -96 or -23 => 2,
        0 or 36 or -36 => 3,
        59 or 23 or 96 => 4,
        119 or 83 or 156 => 5,
        _ => -1
    };
}

class AlexandrianThunderIII(BossModule module) : Components.GenericAOEs(module)
{
    private static readonly AOEShapeCircle _strike = new(4);
    private RoseTiles? _tiles;
    private readonly List<(ulong Caster, WPos Target, BitMask Tiles, DateTime Activation)> _strikes = [];
    private DateTime _surge;
    private int _surgeCasts;

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor)
    {
        var tileActivation = new DateTime[RoseTiles.NumTiles];
        foreach (var s in _strikes)
        {
            yield return new(_strike, s.Target, default, s.Activation);
            foreach (var t in s.Tiles.SetBits())
                if (tileActivation[t] == default || s.Activation < tileActivation[t])
                    tileActivation[t] = s.Activation;
        }

        _tiles ??= Module.FindComponent<RoseTiles>();
        if (_surge != default && _tiles != null)
            foreach (var t in _tiles.Lit.SetBits())
                if (tileActivation[t] == default)
                    tileActivation[t] = _surge;

        for (var t = 0; t < RoseTiles.NumTiles; ++t)
            if (tileActivation[t] != default)
                yield return new(RoseTiles.Slice, Arena.Center, RoseTiles.Direction(t), tileActivation[t]);
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        switch ((AID)spell.Action.ID)
        {
            case AID.AlexandrianThunderIII1:
            case AID.AlexandrianThunderIII2:
                _strikes.Add((caster.InstanceID, spell.LocXZ, TilesHitFrom(spell.LocXZ), Module.CastFinishAt(spell)));
                break;
            case AID.AlexandrianThunderIVCircle2:
            case AID.AlexandrianThunderIVDonut2:
                if (_surge == default && _strikes.Count == 0)
                    _surge = Module.CastFinishAt(spell);
                break;
        }
    }

    public override void OnCastFinished(Actor caster, ActorCastInfo spell)
    {
        switch ((AID)spell.Action.ID)
        {
            case AID.AlexandrianThunderIII1:
            case AID.AlexandrianThunderIII2:
                ++NumCasts;
                _strikes.RemoveAll(s => s.Caster == caster.InstanceID);
                break;
            case AID.AlexandrianThunderIVCircle2:
            case AID.AlexandrianThunderIVDonut2:
                if (_surge != default && ++_surgeCasts >= 2)
                {
                    _surge = default;
                    _surgeCasts = 0;
                }
                break;
        }
    }

    private BitMask TilesHitFrom(WPos target)
    {
        var hit = new BitMask();
        _tiles ??= Module.FindComponent<RoseTiles>();
        if (_tiles == null)
            return hit;
        for (var t = 0; t < RoseTiles.NumTiles; ++t)
            if (_tiles.Lit[t] && !hit[t] && Intersect.CircleCone(target, _strike.Radius, Arena.Center, RoseTiles.Slice.Radius, RoseTiles.Direction(t).ToDirection(), RoseTiles.Slice.HalfAngle))
                hit |= _tiles.ConnectedTo(t);
        return hit;
    }
}

class ValorousAscensionRect(BossModule module) : Components.GenericAOEs(module, AID.ValorousAscensionRect)
{
    private static readonly AOEShapeRect _line = new(40, 4);
    private const int _maxShown = 2;
    private readonly List<(ulong Thorn, WPos Origin, Angle Direction, DateTime Activation)> _lines = [];

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor)
    {
        foreach (var l in _lines.OrderBy(l => l.Activation).Take(_maxShown))
            yield return new(_line, l.Origin, l.Direction, l.Activation);
    }

    public override void OnActorPlayActionTimelineEvent(Actor actor, ushort id)
    {
        if ((OID)actor.OID == OID.BriarThorn1 && id == 0x11DB)
            _lines.Add((actor.InstanceID, actor.Position, actor.Rotation, WorldState.FutureTime(11.1f)));
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if (spell.Action != WatchedAction)
            return;
        _lines.RemoveAll(l => l.Thorn == caster.InstanceID);
        _lines.Add((caster.InstanceID, caster.Position, spell.Rotation, Module.CastFinishAt(spell)));
    }

    public override void OnCastFinished(Actor caster, ActorCastInfo spell)
    {
        if (spell.Action == WatchedAction)
            _lines.RemoveAll(l => l.Thorn == caster.InstanceID);
    }
}

class T04ZeleniaStates : StateMachineBuilder
{
    public T04ZeleniaStates(BossModule module) : base(module)
    {
        // TODO: enrage never reached (kill at ~6:50)
        TrivialPhase()
            .ActivateOnEnter<ThornedCatharsis>()
            .ActivateOnEnter<PerfumedQuietus>()
            .ActivateOnEnter<ValorousAscension>()
            .ActivateOnEnter<ValorousAscensionRect>()
            .ActivateOnEnter<SpecterOfTheLost>()
            .ActivateOnEnter<PowerBreak>()
            .ActivateOnEnter<HolyHazard>()
            .ActivateOnEnter<ThunderSlash>()
            .ActivateOnEnter<RosebloodBloom>()
            .ActivateOnEnter<StockBreak>()
            .ActivateOnEnter<ShockBait>()
            .ActivateOnEnter<ShockPuddles>()
            .ActivateOnEnter<CenterVoid>()
            .ActivateOnEnter<AlexandrianThunderIV>()
            .ActivateOnEnter<RoseTiles>()
            .ActivateOnEnter<AlexandrianThunderIII>();
    }
}

[ModuleInfo(Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 1030, NameID = 13861)]
public class T04Zelenia(ModuleInit init) : BossModule(init, new(100, 100), DefaultArena)
{
    public static readonly ArenaBoundsCircle DefaultArena = new(16);
    public static readonly ArenaBoundsCustom DonutArena = new(16, DefaultArena.Clipper.Difference(new(CurveApprox.Circle(16, 0.05f)), new(CurveApprox.Circle(4, 0.05f))));
}
