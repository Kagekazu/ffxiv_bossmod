namespace BossMod.RealmReborn.Dungeon.D01Sastasha.D012CaptainMadison;

public enum OID : uint
{
    Boss = 0x566, // R?, Captain Madison (NameID 1382)
}

class D012CaptainMadisonStates : StateMachineBuilder
{
    public D012CaptainMadisonStates(BossModule module) : base(module)
    {
        TrivialPhase();
    }
}

[ModuleInfo(Contributors = "Kagekazu", Incomplete = true, GroupType = BossModuleInfo.GroupType.CFC, GroupID = 4, NameID = 1382)]
public class D012CaptainMadison(WorldState ws, Actor primary) : BossModule(ws, primary, primary.Position, new ArenaBoundsCircle(20));
