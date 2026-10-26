namespace BossMod.Endwalker.Raid.P5NProtoCarbuncle;

public enum OID : uint
{
    Boss = 0x39E6, // Teamcraft index
    LivelyBait = 0x39E9, // Teamcraft index
    Helper = 0x233C, // Helper type
}

public enum AID : uint
{
    SearingRay1 = 30423, // Proto-Carbuncle, cast, cactbot ~19.1s
    RubyGlow = 30420, // Proto-Carbuncle, cactbot ~28.2s
    SearingRay2 = 30424, // Proto-Carbuncle, cast, cactbot ~39.7s
    Crunch = 30448, // Proto-Carbuncle, cast, cactbot ~51.9s
    TopazStones = 30430, // Proto-Carbuncle, cactbot ~59.1s
    SonicHowl = 30450, // Proto-Carbuncle, cast, cactbot ~97.6s
    ToxicCrunch = 30449, // Proto-Carbuncle, cast, cactbot ~220.9s
    VenomSquall = 30444, // Proto-Carbuncle, cactbot ~238.0s
    VenomRain = 30445, // Proto-Carbuncle, cactbot ~245.8s
    VenomPool = 30447, // Proto-Carbuncle, cactbot ~252.8s
    StarvingStampede1 = 30437, // Proto-Carbuncle, cactbot ~311.8s
    TopazCluster = 30438, // Proto-Carbuncle, cactbot ~340.2s
    AcidicSlaver = 30955, // cast, cactbot trigger
    StarvingStampede2 = 31209, // cast, cactbot trigger
    TopazStonesCollect = 30431, // cast, cactbot trigger
}

public enum IconID : uint
{
    Icon100 = 100, // cactbot
    Icon376 = 376, // cactbot
}

class P5NProtoCarbuncleStates : StateMachineBuilder
{
    public P5NProtoCarbuncleStates(BossModule module) : base(module)
    {
        // TODO: stub from cactbot IDs + Teamcraft OIDs; components and states from the first replay
        TrivialPhase();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 872, NameID = 11440)]
public class P5NProtoCarbuncle(ModuleInit init) : BossModule(init, new(100, 100), new ArenaBoundsSquare(15));
