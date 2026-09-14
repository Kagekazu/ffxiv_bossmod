namespace BossMod.RealmReborn.Dungeon.D11DzemaelDarkhold.D112Taulurd;

public enum OID : uint
{
    Boss = 0x4A8B, // R1.560, Taulurd
    DeepvoidSlave = 0x4A8C, // R1.040
}

public enum AID : uint
{
    AutoAttack = 872, // Boss->player, no cast, single-target
    BossCast = 45572, // Boss->self, 2.7s cast
    BossHit45573 = 45573, // Boss->self, no cast
    BossHit45574 = 45574, // Boss->self, no cast
    SlaveCast = 45575, // DeepvoidSlave->self, 3.7s cast
    SlaveHit = 45576, // DeepvoidSlave->self, no cast
}

class BossCast(BossModule module) : Components.CastHint(module, AID.BossCast, "Boss cast");
class SlaveCast(BossModule module) : Components.CastHint(module, AID.SlaveCast, "Add cast");
class Slaves(BossModule module) : Components.Adds(module, (uint)OID.DeepvoidSlave, 1);

class D112TaulurdStates : StateMachineBuilder
{
    public D112TaulurdStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<BossCast>()
            .ActivateOnEnter<SlaveCast>()
            .ActivateOnEnter<Slaves>();
    }
}

[ModuleInfo(Contributors = "Kagekazu", Incomplete = true, GroupType = BossModuleInfo.GroupType.CFC, GroupID = 13, NameID = 1415)]
public class D112Taulurd(WorldState ws, Actor primary) : BossModule(ws, primary, new(-93, -33), new ArenaBoundsSquare(20));
