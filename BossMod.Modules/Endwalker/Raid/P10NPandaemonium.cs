namespace BossMod.Endwalker.Raid.P10NPandaemonium;

public enum OID : uint
{
    Boss = 0x3F19, // Teamcraft index
    PandaemoniacPillar1 = 0x3F1A, // Teamcraft index
    PandaemoniacPillar2 = 0x3F1B, // Teamcraft index
    Helper = 0x233C, // Helper type
}

public enum AID : uint
{
    Silkspit1 = 33369, // Pand\\u00e6monium, cactbot ~10.9s
    Silkspit2 = 33370, // Pand\\u00e6monium, cactbot ~23.0s
    PandaemoniacPillars = 33373, // Pand\\u00e6monium, cast, cactbot ~35.4s
    Imprisonment = 33378, // Pand\\u00e6monium, cast, cactbot ~43.4s
    Ultima = 33403, // Pand\\u00e6monium, cast, cactbot ~50.1s
    Cannonspawn = 33380, // Pand\\u00e6monium, cast, cactbot ~67.8s
    PandaemoniacMeltdown = 33398, // Pand\\u00e6monium, cactbot ~80.0s
    SoulGrasp = 33400, // Pand\\u00e6monium, cast, cactbot ~90.1s
    EntanglingWeb = 34593, // Pand\\u00e6monium, cactbot ~101.3s
    PartedPlumes = 33392, // Pand\\u00e6monium, cactbot ~150.6s
    Touchdown = 33384, // Pand\\u00e6monium, cast, cactbot ~224.6s
    HarrowingHell1 = 33386, // Pand\\u00e6monium, cast, cactbot ~272.8s
    HarrowingHell2 = 33391, // Pand\\u00e6monium, cast, cactbot ~289.0s
    WickedStep = 33394, // Pand\\u00e6monium, cast, cactbot ~322.7s
}

public enum IconID : uint
{
    Icon462 = 462, // cactbot
}

class P10NPandaemoniumStates : StateMachineBuilder
{
    public P10NPandaemoniumStates(BossModule module) : base(module)
    {
        // TODO: stub from cactbot IDs + Teamcraft OIDs; components and states from the first replay
        TrivialPhase();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 938, NameID = 12354)]
public class P10NPandaemonium(ModuleInit init) : BossModule(init, new(100, 92.5f), new ArenaBoundsRect(30, 22.5f));
