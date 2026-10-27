namespace BossMod.Dawntrail.Dungeon.D07StrayboroughDeadwalk.D071HisRoyalHeadnessLeonoggI;

public enum OID : uint
{
    Boss = 0x4183, // R3.600, x1
    Helper = 0x233C, // R0.500, x?, Helper type
    LittleLadyNogginette = 0x41BD, // R1.000, x0 (spawn during fight)
    LittleLordNoggington = 0x41BB, // R1.000, x0 (spawn during fight)
    NobleNoggin = 0x4205, // R1.000, x0 (spawn during fight)
}

public enum AID : uint
{
    AutoAttack = 872, // Boss->player, no cast, single-target
    MaliciousMist = 36529, // Boss->self, 5.0s cast, range 50 circle, raidwide + arena shrink
    FallingNightmareVisual = 36526, // Boss->self, 3.0s cast, single-target, visual
    FallingNightmare1 = 36532, // NobleNoggin->self, 2.0s cast, range 2 circle
    FallingNightmare2 = 36536, // NobleNoggin->self, 1.0s cast, range 2 circle
    MorbidFascination = 36528, // Boss->self, no cast, single-target
    TeamSpirit = 36527, // Boss->self, 3.0s cast, single-target, visual (spawn dolls)
    SpiritedChargeVisual = 36598, // Boss->self, 3.0s cast, single-target, visual
    SpiritedChargeStart = 36533, // doll->self, no cast, single-target
    SpiritedCharge = 36534, // Helper->self, no cast, range 2 width 1 rect
    Overattachment = 36535, // doll->player, no cast, single-target
    EvilSchemeVisual = 39682, // Boss->self, 6.0s cast, single-target, visual (exaflare)
    EvilSchemeFirst = 39683, // Helper->self, 6.0s cast, range 4 circle
    EvilSchemeRest = 39684, // Helper->self, no cast, range 4 circle
    LoomingNightmareVisual = 39685, // Boss->self, 5.0s cast, single-target, visual (chase)
    LoomingNightmareFirst = 39686, // Helper->self, 2.0s cast, range 4 circle
    LoomingNightmareRest = 39687, // Helper->self, no cast, range 4 circle
    ScreamVisual1 = 36530, // Boss->self, 5.0s cast, single-target, visual
    ScreamVisual2 = 36541, // Boss->self, no cast, single-target, visual
    Scream = 36531, // Helper->self, 5.0s cast, range 20 60-degree cone
}

public enum IconID : uint
{
    LoomingNightmare = 197, // player
}

class MaliciousMist(BossModule module) : Components.RaidwideCast(module, AID.MaliciousMist);

class MistDeathwall(BossModule module) : Components.GenericAOEs(module, AID.MaliciousMist)
{
    private static readonly AOEShapeDonut _ring = new(D071HisRoyalHeadnessLeonoggI.DefaultBounds.Radius, D071HisRoyalHeadnessLeonoggI.StartingBounds.Radius + 0.5f);
    private DateTime _shrinkAt;

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor)
    {
        if (_shrinkAt != default)
            yield return new(_ring, Arena.Center, Activation: _shrinkAt);
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if (spell.Action == WatchedAction && Arena.Bounds == D071HisRoyalHeadnessLeonoggI.StartingBounds)
            _shrinkAt = Module.CastFinishAt(spell, 0.9f);
    }

    public override void OnMapEffect(byte index, uint state)
    {
        if (index != 0 || state != 0x00020001)
            return;
        _shrinkAt = default;
        Arena.Bounds = D071HisRoyalHeadnessLeonoggI.DefaultBounds;
    }
}

class LoomingNightmare(BossModule module) : Components.GenericChasingAOEs(module)
{
    private static readonly AOEShapeCircle _puddle = new(4);
    private const float _step = 3;
    private const float _interval = 1.6f;
    private int _chases;

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID != AID.LoomingNightmareFirst)
            return;

        Actor? target = null;
        var bestDistSq = float.MaxValue;
        foreach (var p in Raid.WithoutSlot())
        {
            var distSq = (p.Position - caster.Position).LengthSq();
            if (distSq < bestDistSq && !IsChased(p))
            {
                target = p;
                bestDistSq = distSq;
            }
        }
        if (target == null)
            return;

        // note: first two chases had 5 puddles, later ones 4
        var puddles = ++_chases <= 2 ? 5 : 4;
        Chasers.Add(new(_puddle, target, caster.Position, 0, puddles, Module.CastFinishAt(spell), _interval, _step));
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if ((AID)spell.Action.ID is AID.LoomingNightmareFirst or AID.LoomingNightmareRest && Advance(caster.Position, _step, WorldState.CurrentTime) == null)
            ReportError($"no chaser near {caster.Position}");
    }

    public override void Update()
    {
        Chasers.RemoveAll(c => c.Target.IsDead || c.Target.IsDestroyed);
    }

    public override void AddAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        base.AddAIHints(slot, actor, assignment, hints);

        foreach (var c in Chasers)
            if (c.Target == actor)
                hints.AddForbiddenZone(ShapeDistance.Circle(Arena.Center, 13.5f), c.NextActivation);
    }

    private bool IsChased(Actor player)
    {
        foreach (var c in Chasers)
            if (c.Target == player)
                return true;
        return false;
    }
}

class FallingNightmare(BossModule module) : Components.GenericAOEs(module)
{
    private static readonly AOEShapeCircle _shape = new(2);
    private readonly Dictionary<ulong, AOEInstance> _pending = [];

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor) => _pending.Values;

    public override void OnActorPlayActionTimelineEvent(Actor actor, ushort id)
    {
        if ((OID)actor.OID == OID.NobleNoggin && id == 0x11D1)
            _pending[actor.InstanceID] = new(_shape, actor.Position, default, WorldState.FutureTime(3));
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if (IsNightmare(spell.Action))
            _pending[caster.InstanceID] = new(_shape, caster.Position, default, Module.CastFinishAt(spell));
    }

    public override void OnCastFinished(Actor caster, ActorCastInfo spell)
    {
        if (IsNightmare(spell.Action))
        {
            ++NumCasts;
            _pending.Remove(caster.InstanceID);
        }
    }

    public override void OnActorDestroyed(Actor actor) => _pending.Remove(actor.InstanceID);

    private static bool IsNightmare(ActionID action) => (AID)action.ID is AID.FallingNightmare1 or AID.FallingNightmare2;
}

class SpiritedCharge(BossModule module) : Components.GenericAOEs(module)
{
    private static readonly AOEShapeRect _path = new(6, 1);
    private readonly HashSet<ulong> _charging = [];

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor)
    {
        foreach (var id in _charging)
            if (WorldState.Actors.Find(id) is { IsDestroyed: false } doll)
                yield return new(_path, doll.Position, doll.Rotation);
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if ((AID)spell.Action.ID == AID.SpiritedChargeStart)
            _charging.Add(caster.InstanceID);
    }

    public override void OnActorPlayActionTimelineEvent(Actor actor, ushort id)
    {
        if (id == 0x1E3C)
            _charging.Remove(actor.InstanceID);
    }

    public override void OnActorDestroyed(Actor actor) => _charging.Remove(actor.InstanceID);
}

class EvilScheme(BossModule module) : Components.Exaflare(module, 4)
{
    private const float _spacing = 4;

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID != AID.EvilSchemeFirst)
            return;
        var line = new Line
        {
            Next = caster.Position,
            Advance = spell.Rotation.ToDirection() * _spacing,
            NextExplosion = Module.CastFinishAt(spell, 1.6f),
            TimeToMove = 1.5f,
            ExplosionsLeft = 5,
        };
        line.MaxShownExplosions = line.ExplosionsLeft;
        Lines.Add(line);
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if ((AID)spell.Action.ID is not AID.EvilSchemeFirst and not AID.EvilSchemeRest)
            return;

        ++NumCasts;
        var line = Lines.MinBy(l => (l.Next - caster.Position).LengthSq());
        if (line == null || !line.Next.AlmostEqual(caster.Position, 1))
        {
            ReportError($"unexpected explosion at {caster.Position}");
            return;
        }

        AdvanceLine(line, caster.Position);
        if (line.ExplosionsLeft <= 0)
            Lines.Remove(line);
    }
}

class Scream(BossModule module) : Components.StandardAOEs(module, AID.Scream, new AOEShapeCone(20, 30.Degrees()), maxCasts: 4);

class D071HisRoyalHeadnessLeonoggIStates : StateMachineBuilder
{
    public D071HisRoyalHeadnessLeonoggIStates(BossModule module) : base(module)
    {
        // TODO: Morbid Fascination and Scream never seen (two clears)
        TrivialPhase()
            .ActivateOnEnter<MaliciousMist>()
            .ActivateOnEnter<MistDeathwall>()
            .ActivateOnEnter<Scream>()
            .ActivateOnEnter<SpiritedCharge>()
            .ActivateOnEnter<FallingNightmare>()
            .ActivateOnEnter<EvilScheme>()
            .ActivateOnEnter<LoomingNightmare>();
    }
}

[ModuleInfo(Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 981, NameID = 13073)]
public class D071HisRoyalHeadnessLeonoggI(ModuleInit init) : BossModule(init, ArenaCenter, StartingBounds)
{
    public static readonly WPos ArenaCenter = new(0, 150);
    public static readonly ArenaBoundsCircle StartingBounds = new(19.5f);
    public static readonly ArenaBoundsCircle DefaultBounds = new(14);
}
