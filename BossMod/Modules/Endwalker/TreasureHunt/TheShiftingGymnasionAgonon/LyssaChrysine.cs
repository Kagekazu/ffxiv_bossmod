namespace BossMod.Endwalker.TreasureHunt.ShiftingGymnasionAgonon.LyssaChrysine;

public enum OID : uint
{
    Boss = 0x3D43, //R=5
    BonusAddLyssa = 0x3D4E, //R=3.75, bonus loot adds
    BossHelper = 0x233C,
    IcePillars = 0x3D44,
    BonusAddLampas = 0x3D4D, //R=2.001, bonus loot adds
}

public enum AID : uint
{
    AutoAttack = 870, // Boss/BossAdd->player, no cast, single-target
    Icicall = 32307, // Boss->self, 2.5s cast, single-target, spawns ice pillars
    IcePillar = 32315, // IcePillars->self, 3.0s cast, range 6 circle
    SkullDasher = 32306, // Boss->player, 5.0s cast, single-target
    PillarPierce = 32316, // IcePillars->self, 3.0s cast, range 80 width 4 rect
    HeavySmash = 32314, // Boss->players, 5.0s cast, range 6 circle
    Howl = 32296, // Boss->self, 2.5s cast, single-target, calls adds
    FrigidNeedle = 32310, // Boss->self, 3.5s cast, single-target --> combo start FrigidNeedle2 --> CircleofIce2 (out-->in)
    FrigidNeedle2 = 32311, // BossHelper->self, 4.0s cast, range 10 circle
    CircleOfIce = 32312, // Boss->self, 3.5s cast, single-target --> combo start CircleofIce2 --> FrigidNeedle2 (in-->out)
    CircleOfIce2 = 32313, // BossHelper->self, 4.0s cast, range 10-20 donut
    HeavySmash2 = 32317, // BossAdd->location, 3.0s cast, range 6 circle
    FrigidStone = 32308, // Boss->self, 2.5s cast, single-target, activates helpers
    FrigidStone2 = 32309, // BossHelper->location, 3.0s cast, range 5 circle
    Telega = 9630, // BonusAdds->self, no cast, single-target, bonus add disappear
}

class HeavySmash2(BossModule module) : Components.StandardAOEs(module, AID.HeavySmash2, 6);
class FrigidStone2(BossModule module) : Components.StandardAOEs(module, AID.FrigidStone2, 5);

class OutInAOE(BossModule module) : Components.ConcentricAOEs(module, _shapes)
{
    private static readonly AOEShape[] _shapes = [new AOEShapeCircle(10), new AOEShapeDonut(10, 20)];

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID == AID.FrigidNeedle)
            AddSequence(Module.Center, Module.CastFinishAt(spell, 0.45f));
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if (Sequences.Count > 0)
        {
            var order = (AID)spell.Action.ID switch
            {
                AID.FrigidNeedle2 => 0,
                AID.CircleOfIce2 => 1,
                _ => -1
            };
            AdvanceSequence(order, Module.Center, WorldState.FutureTime(2));
        }
    }
}

class InOutAOE(BossModule module) : Components.ConcentricAOEs(module, _shapes)
{
    private static readonly AOEShape[] _shapes = [new AOEShapeDonut(10, 20), new AOEShapeCircle(10)];

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID == AID.CircleOfIce)
            AddSequence(Module.Center, Module.CastFinishAt(spell, 0.45f));
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if (Sequences.Count > 0)
        {
            var order = (AID)spell.Action.ID switch
            {
                AID.CircleOfIce2 => 0,
                AID.FrigidNeedle2 => 1,
                _ => -1
            };
            AdvanceSequence(order, Module.Center, WorldState.FutureTime(2));
        }
    }
}

class PillarPierce(BossModule module) : Components.StandardAOEs(module, AID.PillarPierce, new AOEShapeRect(80, 2));
class SkullDasher(BossModule module) : Components.SingleTargetCast(module, AID.SkullDasher);
class HeavySmash(BossModule module) : Components.StackWithCastTargets(module, AID.HeavySmash, 6);

class IcePillarSpawn(BossModule module) : Components.GenericAOEs(module)
{
    private readonly List<AOEInstance> _aoes = [];

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor) => _aoes.Take(4);

    public override void OnActorCreated(Actor actor)
    {
        if ((OID)actor.OID == OID.IcePillars)
            _aoes.Add(new(new AOEShapeCircle(6), actor.Position, default, WorldState.FutureTime(3.75f)));
    }
    public override void OnCastFinished(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID == AID.IcePillar)
            _aoes.RemoveAt(0);
    }
}

class Howl(BossModule module) : Components.CastHint(module, AID.Howl, "Calls adds");

class LyssaStates : StateMachineBuilder
{
    public LyssaStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<IcePillarSpawn>()
            .ActivateOnEnter<HeavySmash>()
            .ActivateOnEnter<SkullDasher>()
            .ActivateOnEnter<Howl>()
            .ActivateOnEnter<OutInAOE>()
            .ActivateOnEnter<InOutAOE>()
            .ActivateOnEnter<FrigidStone2>()
            .ActivateOnEnter<HeavySmash2>()
            .ActivateOnEnter<PillarPierce>()
            .Raw.Update = () => module.Enemies(OID.Boss).All(e => e.IsDead) && module.Enemies(OID.BonusAddLyssa).All(e => e.IsDead) && module.Enemies(OID.BonusAddLampas).All(e => e.IsDead);
    }
}

[ModuleInfo(BossModuleInfo.Maturity.Contributed, Contributors = "Malediktus", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 909, NameID = 12024)]
public class Lyssa(WorldState ws, Actor primary) : BossModule(ws, primary, new(100, 100), new ArenaBoundsCircle(20))
{
    protected override void DrawEnemies(int pcSlot, Actor pc)
    {
        Arena.Actor(PrimaryActor, ArenaColor.Enemy);
        foreach (var s in Enemies(OID.BonusAddLyssa))
            Arena.Actor(s, ArenaColor.Vulnerable);
        foreach (var s in Enemies(OID.BonusAddLampas))
            Arena.Actor(s, ArenaColor.Vulnerable);
    }

    protected override void CalculateModuleAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        foreach (var e in hints.PotentialTargets)
        {
            e.Priority = (OID)e.Actor.OID switch
            {
                OID.BonusAddLampas => 3,
                OID.BonusAddLyssa => 2,
                OID.Boss => 1,
                _ => 0
            };
        }
    }
}
