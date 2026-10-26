namespace BossMod.Endwalker.Trial.T03Endsinger;

public enum OID : uint
{
    Boss = 0x34F3, // Teamcraft index
    Endsinger2 = 0x34F5, // Teamcraft index
    Endsinger3 = 0x34F6, // Teamcraft index
    Kakodaimon = 0x34F7, // Teamcraft index
    DoomedStars = 0x34F8, // Teamcraft index
    FeatherOfDespair = 0x34FA, // Teamcraft index
    Helper = 0x233C, // Helper type
}

public enum AID : uint
{
    Elegeia = 26156, // The Endsinger, cast, cactbot ~13.7s
    Galaxias = 27753, // The Endsinger, cast, cactbot ~56.1s
    Elenchos1 = 26180, // The Endsinger, cast, cactbot ~69.3s
    DeathsEmbrace = 26185, // The Endsinger, cactbot ~93.6s
    Aporrhoia = 26173, // The Endsinger, cactbot ~105.8s
    Elenchos2 = 26174, // The Endsinger, cactbot ~115.4s
    Hubris = 26194, // The Endsinger, cast, cactbot ~120.0s
    ElegeiaUnforgotten = 26242, // The Endsinger, cast, cactbot ~132.2s
    Fatalism1 = 26162, // The Endsinger, cactbot ~158.4s
    Ekstasis = 26181, // The Endsinger, cactbot ~238.1s
    Epigonoi = 26182, // The Endsinger, cast, cactbot ~241.9s
    Interstellar = 26189, // The Endsinger, cactbot ~274.4s
    Nemesis1 = 26190, // The Endsinger, cast, cactbot ~276.6s
    Nemesis2 = 26196, // The Endsinger, cactbot ~388.0s
    Planetes = 27480, // The Endsinger, cast, cactbot ~1000.0s
    MeteorRadiant = 26198, // Kakodaimon, cactbot ~1023.4s
    Crash = 26199, // Kakodaimon, cactbot ~1035.2s
    MeteorOutburst = 26204, // Kakodaimon, cactbot ~1056.6s
    Katastrophe = 26203, // Kakodaimon, cactbot ~1067.8s
    UltimateFate = 27481, // The Endsinger, cast, cactbot ~1212.2s
    Fatalism2 = 27483, // The Endsinger, cactbot ~2000.0s
    Telos = 26206, // The Endsinger, cactbot ~2086.1s
    Telomania1 = 26207, // The Endsinger, cactbot ~2099.3s
    Telomania2 = 26209, // The Endsinger, cactbot ~2114.6s
    DoomedStarsAoE1 = 26164, // cast, cactbot trigger
    ElenchosOutsides = 26178, // cast, cactbot trigger
    DoomedStarsAoE2 = 26158, // cast, cactbot trigger
}

public enum TetherID : uint
{
    Tether166 = 166, // cactbot
}

class T03EndsingerStates : StateMachineBuilder
{
    public T03EndsingerStates(BossModule module) : base(module)
    {
        // TODO: stub from cactbot IDs + Teamcraft OIDs; components and states from the first replay
        TrivialPhase();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 796, NameID = 10448)]
public class T03Endsinger(ModuleInit init) : BossModule(init, new(100, 100), new ArenaBoundsCircle(20));
