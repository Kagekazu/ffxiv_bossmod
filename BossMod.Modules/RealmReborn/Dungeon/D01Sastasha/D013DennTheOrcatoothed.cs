namespace BossMod.RealmReborn.Dungeon.D01Sastasha.D013DennTheOrcatoothed;

public enum OID : uint
{
    Boss = 0x4B6, // R?, Denn the Orcatoothed (NameID 1206)
}

class D013DennTheOrcatoothedStates : StateMachineBuilder
{
    public D013DennTheOrcatoothedStates(BossModule module) : base(module)
    {
        TrivialPhase();
    }
}

[ModuleInfo(Contributors = "Kagekazu", Incomplete = true, GroupType = BossModuleInfo.GroupType.CFC, GroupID = 4, NameID = 1206)]
public class D013DennTheOrcatoothed(WorldState ws, Actor primary) : BossModule(ws, primary, primary.Position, new ArenaBoundsCircle(20));
