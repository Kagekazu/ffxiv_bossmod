namespace BossMod.RealmReborn.Dungeon.D07Brayflox.D073Hellbender;

public enum OID : uint
{
    Boss = 0x1AB, // Hellbender

    Aiatar = 0x1AD, // Aiatar

    QueerBubble = 0x428 // Queer Bubble
}

public enum AID : uint
{
    AutoAttack = 872, // Boss->player, no cast, single target
    BogBubble = 980, // Boss->player, no cast, range 6 circle aoe
    PeculiarLight = 982, // Boss->self, 3.5s cast, range 8 circle aoe
    StagnantSpray = 448, // Boss->self, 2.5s cast, range 8 120-degree cone aoe
    Effluvium = 979, // Boss->player, no cast, single target

    AutoAttackAiatar = 870, // Boss-player, no cast, single target
    Touchdown = 564 // Boss->self, no cast, range 10 circle aoe
}

public enum SID : uint
{
    Bind = 280
}

class PeculiarLight(BossModule module) : Components.StandardAOEs(module, AID.PeculiarLight, 8);
class StagnantSpray(BossModule module) : Components.StandardAOEs(module, AID.StagnantSpray, new AOEShapeCone(8, 60.Degrees()));

class D073HellbenderStates : StateMachineBuilder
{
    public D073HellbenderStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<PeculiarLight>()
            .ActivateOnEnter<StagnantSpray>();
    }
}

[ModuleInfo(Contributors = "Kagekazu", Incomplete = true, GroupType = BossModuleInfo.GroupType.CFC, GroupID = 8, NameID = 1286)]
public class D073Hellbender(WorldState ws, Actor primary) : BossModule(ws, primary, primary.Position, new ArenaBoundsCircle(20));
