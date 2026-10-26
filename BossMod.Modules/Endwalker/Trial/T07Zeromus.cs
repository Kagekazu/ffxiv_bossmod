namespace BossMod.Endwalker.Trial.T07Zeromus;

public enum OID : uint
{
    Boss = 0x404E, // Teamcraft index
    Helper = 0x233C, // Helper type
}

public enum AID : uint
{
    AbyssalNox = 35575, // Zeromus, cactbot ~11.7s
    AbyssalEchoes = 35578, // Zeromus, cactbot ~27.7s
    SableThread = 35566, // Zeromus, cactbot ~38.8s
    DarkMatter = 35638, // Zeromus, cactbot ~73.0s
    Flare = 35602, // Zeromus, cast, cactbot ~89.2s
    Nox1 = 36135, // Zeromus, cactbot ~95.3s
    ProminenceSpine = 35606, // Zeromus, cactbot ~97.3s
    Nox2 = 36137, // Zeromus, cactbot ~101.3s
    VoidBio = 35607, // Zeromus, cactbot ~116.6s
    BigBang = 35587, // Zeromus, cast, cactbot ~145.8s
    TheDarkBeckons = 35594, // Zeromus, cactbot ~172.7s
    TheDarkDivides = 35593, // Zeromus, cactbot ~177.7s
    MeteorImpact1 = 35595, // Zeromus, cactbot ~195.1s
    BlackHole = 36133, // Zeromus, cactbot ~225.5s
    VoidMeteor = 35596, // Zeromus, cactbot ~256.6s
    MeteorImpact2 = 35601, // Comet, cactbot ~257.8s
    MeteorImpact3 = 35599, // Zeromus, cactbot ~274.1s
    Explosion = 36147, // Comet, cactbot ~280.1s
    BigCrunch = 35588, // Zeromus, cast, cactbot ~319.9s
    VisceralWhirl = 35579, // Zeromus, cast, cactbot ~442.8s
    RendTheRift = 35609, // Zeromus, cactbot ~1006.0s
    Nostalgia = 35610, // Zeromus, cast, cactbot ~1023.3s
    Bury1 = 35612, // Zeromus, cactbot ~1024.1s
    Bury2 = 35613, // Zeromus, cactbot ~1025.1s
    Bury3 = 35614, // Zeromus, cactbot ~1026.1s
    Bury4 = 35615, // Zeromus, cactbot ~1027.1s
    Roar5 = 35616, // Zeromus, cactbot ~1029.1s
    Roar6 = 35617, // Zeromus, cactbot ~1030.1s
    PrimalRoar7 = 35618, // Zeromus, cactbot ~1033.1s
    FlowOfTheAbyss = 36089, // Zeromus, cactbot ~1047.4s
    DimensionalSurge = 35637, // Zeromus, cactbot ~1049.4s
    ChasmicNails1 = 35628, // Zeromus, cactbot ~1064.2s
    ChasmicNails2 = 35629, // Zeromus, cactbot ~1065.0s
    ChasmicNails3 = 35630, // Zeromus, cactbot ~1065.8s
    ChasmicNails4 = 35631, // Zeromus, cactbot ~1066.5s
    ChasmicNails5 = 35632, // Zeromus, cactbot ~1067.3s
    VisceralWhirlNWSafe = 35582, // cast, cactbot trigger
    FracturedEventideNESafe = 35571, // cast, cactbot trigger
    FracturedEventideNWSafe = 35572, // cast, cactbot trigger
}

public enum IconID : uint
{
    Icon23 = 23, // cactbot
    Icon100 = 100, // cactbot
    Icon197 = 197, // cactbot
    Icon364 = 364, // cactbot
    Icon376 = 376, // cactbot
}

class T07ZeromusStates : StateMachineBuilder
{
    public T07ZeromusStates(BossModule module) : base(module)
    {
        // TODO: stub from cactbot IDs + Teamcraft OIDs; components and states from the first replay
        TrivialPhase();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 964, NameID = 12586)]
public class T07Zeromus(ModuleInit init) : BossModule(init, new(100, 100), new ArenaBoundsSquare(20));
