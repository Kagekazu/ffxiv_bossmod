namespace BossMod.Endwalker.Dungeon.D07Smileton.D072Frameworker;

public enum OID : uint
{
    Boss = 0x34D1, // R4.050, x1
    Helper = 0x233C, // R0.500, Helper type
    SmileySupporter = 0x34D2, // R2.300, x2
    PrintedWorker = 0x34E9, // R4.050, x2
}

public enum AID : uint
{
    AutoAttack = 870, // Boss->player, no cast, single-target
    CircularSaw = 26437, // Boss->self, 5.0s cast, range 40 circle, raidwide
    LeapForwardBoss = 26438, // Boss->location, 7.0s cast, range 15 circle
    LeapForwardWorker = 26439, // PrintedWorker->location, 7.0s cast, range 15 circle
    OmnidimensionalOnslaught = 26440, // Boss->self, 5.0s cast, single-target, visual
    OmnidimensionalOnslaughtAOE = 26441, // Helper->self, 5.0s cast, range 40 45-degree cone, cardinals then intercardinals
    OnslaughtVisual = 26442, // Helper->Boss, no cast, single-target
    PrintWorkersSupporter = 26443, // SmileySupporter->self, no cast, single-target
    PrintWorkers = 28092, // Boss->self, 3.0s cast, single-target, spawns printed workers
}

public enum TetherID : uint
{
    Supporter = 23, // SmileySupporter->Boss
}

class CircularSaw(BossModule module) : Components.RaidwideCast(module, AID.CircularSaw);
class LeapForward(BossModule module) : Components.GroupedAOEs(module, [AID.LeapForwardBoss, AID.LeapForwardWorker], new AOEShapeCircle(15));

class OmnidimensionalOnslaught(BossModule module) : Components.GenericAOEs(module, AID.OmnidimensionalOnslaughtAOE)
{
    private static readonly AOEShapeCone _shape = new(40, 22.5f.Degrees());
    private readonly List<AOEInstance> _aoes = [];

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor)
    {
        var deadline = _aoes.FirstOrDefault().Activation.AddSeconds(1);
        return _aoes.TakeWhile(aoe => aoe.Activation < deadline);
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if (spell.Action == WatchedAction)
        {
            _aoes.Add(new(_shape, caster.Position, spell.Rotation, Module.CastFinishAt(spell)));
            _aoes.SortBy(aoe => aoe.Activation);
        }
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if (spell.Action == WatchedAction && _aoes.Count > 0)
        {
            ++NumCasts;
            _aoes.RemoveAt(0);
        }
    }
}

class D072FrameworkerStates : StateMachineBuilder
{
    public D072FrameworkerStates(BossModule module) : base(module)
    {
        // TODO: Steel Beam tankbuster AID unknown; check whether both Onslaught sets start casting together
        TrivialPhase()
            .ActivateOnEnter<CircularSaw>()
            .ActivateOnEnter<LeapForward>()
            .ActivateOnEnter<OmnidimensionalOnslaught>();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 794, NameID = 10333)]
public class D072Frameworker(ModuleInit init) : BossModule(init, new(64, -112), new ArenaBoundsCircle(20));
