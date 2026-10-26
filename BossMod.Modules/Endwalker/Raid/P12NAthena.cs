namespace BossMod.Endwalker.Raid.P12NAthena;

public enum OID : uint
{
    Boss = 0x3F23, // Teamcraft index
    Anthropos1 = 0x3F24, // Teamcraft index
    LogouIdea = 0x3F25, // Teamcraft index
    ThymouIdea = 0x3F26, // Teamcraft index
    Anthropos2 = 0x3F27, // Teamcraft index
    Helper = 0x233C, // Helper type
}

public enum AID : uint
{
    OnTheSoul = 33497, // Athena, cast, cactbot ~12.0s
    Paradeigma = 33479, // Athena, cactbot ~43.7s
    RayOfLight1 = 33480, // Anthropos, cactbot ~57.1s
    WhiteFlame = 33481, // Anthropos, cactbot ~57.1s
    Glaukopis = 33493, // Athena, cast, cactbot ~92.5s
    SuperchainTheory = 33468, // Athena, cast, cactbot ~104.6s
    SuperchainCoil = 33470, // Athena, cactbot ~119.8s
    SuperchainBurst = 33469, // Athena, cactbot ~119.8s
    Parthenos = 33496, // Athena, cast, cactbot ~155.0s
    UnnaturalEnchainment = 33471, // Athena, cast, cactbot ~178.5s
    Sample = 33472, // Athena, cactbot ~179.6s
    UltimaBlade = 33483, // Athena, cactbot ~246.4s
    Palladion1 = 33486, // Athena, cactbot ~266.9s
    RayOfLight2 = 33492, // Anthropos, cactbot ~268.5s
    ClearCut = 33491, // Anthropos, cactbot ~274.1s
    Palladion = 33489, // Athena, cactbot ~505.8s
    TheossUltima = 33490, // Athena, cactbot ~517.8s
    Dialogos1 = 33494, // Athena, cast, cactbot ~582.8s
    Dialogos2 = 33495, // Athena, cactbot ~587.8s
    WingCleanup1 = 33474, // cast, cactbot trigger
    WingCleanup2 = 33473, // cast, cactbot trigger
}

public enum IconID : uint
{
    Icon139 = 139, // cactbot
}

class P12NAthenaStates : StateMachineBuilder
{
    public P12NAthenaStates(BossModule module) : base(module)
    {
        // TODO: stub from cactbot IDs + Teamcraft OIDs; components and states from the first replay
        TrivialPhase();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 942, NameID = 12377)]
public class P12NAthena(ModuleInit init) : BossModule(init, new(100, 100), new ArenaBoundsSquare(20));
