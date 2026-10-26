namespace BossMod.Endwalker.Raid.P1NErichthonios;

public enum OID : uint
{
    Boss = 0x3521, // Teamcraft index
    Helper = 0x233C, // Helper type
}

public enum AID : uint
{
    GaolersFlailLeft = 28067, // Erichthonios, cactbot ~17.4s
    GaolersFlailRight = 28066, // Erichthonios, cactbot ~17.4s
    WardersWrath = 26100, // Erichthonios, cactbot ~39.0s
    PitilessFlail = 26085, // Erichthonios, cactbot ~51.5s
    TrueHoly = 26087, // Erichthonios, cactbot ~58.6s
    HeavyHand = 26099, // Erichthonios, cast, cactbot ~81.1s
    Intemperance = 26094, // Erichthonios, cactbot ~93.2s
    IntemperateTorment = 26095, // Erichthonios, cactbot ~105.1s
    HotSpell1 = 26096, // Erichthonios, cactbot ~107.7s
    ColdSpell1 = 21745, // Erichthonios, cactbot ~107.7s
    HotSpell2 = 27845, // Erichthonios, cactbot ~116.7s
    ColdSpell2 = 27846, // Erichthonios, cactbot ~116.7s
    ShiningCells = 26089, // Erichthonios, cast, cactbot ~162.7s
    Aetherchain = 26091, // Erichthonios, cactbot ~174.9s
    PowerfulFire = 26092, // Erichthonios, cactbot ~175.8s
    PowerfulLight = 26093, // Erichthonios, cactbot ~175.8s
    SlamShut = 26090, // Erichthonios, cast, cactbot ~260.8s
}

public enum IconID : uint
{
    Icon1 = 1, // cactbot
    Icon62 = 62, // cactbot
}

class P1NErichthoniosStates : StateMachineBuilder
{
    public P1NErichthoniosStates(BossModule module) : base(module)
    {
        // TODO: stub from cactbot IDs + Teamcraft OIDs; components and states from the first replay
        TrivialPhase();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 808, NameID = 10345)]
public class P1NErichthonios(ModuleInit init) : BossModule(init, new(100, 100), new ArenaBoundsSquare(20));
