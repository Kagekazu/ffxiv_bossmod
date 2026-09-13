namespace BossMod.RealmReborn.Dungeon.D01Sastasha.D011Chopper;

public enum OID : uint
{
    Boss = 0x4B4, // R?, Chopper (NameID 1204)
}

class D011ChopperStates : StateMachineBuilder
{
    public D011ChopperStates(BossModule module) : base(module)
    {
        TrivialPhase();
    }
}

[ModuleInfo(Contributors = "Kagekazu", Incomplete = true, GroupType = BossModuleInfo.GroupType.CFC, GroupID = 4, NameID = 1204)]
public class D011Chopper(WorldState ws, Actor primary) : BossModule(ws, primary, primary.Position, new ArenaBoundsCircle(20));
