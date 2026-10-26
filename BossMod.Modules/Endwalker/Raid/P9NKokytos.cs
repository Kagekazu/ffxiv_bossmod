namespace BossMod.Endwalker.Raid.P9NKokytos;

public enum OID : uint
{
    Boss = 0x3ED5, // Teamcraft index
    Comet = 0x3ED6, // Teamcraft index
    Helper = 0x233C, // Helper type
}

public enum AID : uint
{
    GluttonysAugur = 33046, // Kokytos, cactbot ~18.2s
    Ravening1 = 33048, // Kokytos, cactbot ~26.4s
    FireIII1 = 33102, // Kokytos, cactbot ~48.4s
    FireIII2 = 33056, // Kokytos, cactbot ~56.3s
    BlizzardIII1 = 33103, // Kokytos, cactbot ~68.1s
    BlizzardIII2 = 33057, // Kokytos, cactbot ~76.0s
    Dualspell = 33053, // Kokytos, cactbot ~82.6s
    FireIII3 = 33054, // Kokytos, cactbot ~90.3s
    BlizzardIII3 = 33055, // Kokytos, cactbot ~90.3s
    GlobalSpell = 33089, // Kokytos, cast, cactbot ~99.8s
    Disgorge1 = 33060, // Kokytos, cactbot ~103.0s
    Ravening2 = 33049, // Kokytos, cactbot ~115.2s
    ArchaicRockbreaker = 33064, // Kokytos, cast, cactbot ~136.6s
    Shockwave = 33065, // Kokytos, cactbot ~138.2s
    ArchaicRockbreaker1 = 33066, // Kokytos, cactbot ~145.1s
    ArchaicDemolish = 33071, // Kokytos, cast, cactbot ~156.7s
    AscendantFist = 33073, // Kokytos, cast, cactbot ~170.9s
    OutsideRoundhouse = 33068, // Kokytos, cactbot ~183.4s
    Disgorge2 = 33061, // Kokytos, cactbot ~190.5s
    Ravening3 = 33050, // Kokytos, cactbot ~203.5s
    Charybdis1 = 33074, // Kokytos, cactbot ~220.8s
    BeastlyRoar = 33080, // Kokytos, cast, cactbot ~226.9s
    BeastlyBile = 33085, // Kokytos, cactbot ~229.0s
    PulverizingPounce = 33087, // Kokytos, cactbot ~238.1s
    GluttonousRampage = 33081, // Kokytos, cactbot ~253.4s
    Touchdown = 33079, // Kokytos, cactbot ~257.9s
    Comet = 33076, // Kokytos, cactbot ~268.8s
    EclipticMeteor1 = 33160, // Kokytos, cactbot ~279.8s
    Burst = 33078, // Comet, cast, cactbot ~285.9s
    Disgorge3 = 33062, // Kokytos, cactbot ~288.8s
    IceflameSummoning = 34529, // Kokytos, cactbot ~325.2s
    FrontCombination = 30874, // Kokytos, cactbot ~421.8s
    RearCombinationInsideRoundhouse = 33098, // cast, cactbot trigger
    Charybdis2 = 33075, // cast, cactbot trigger
    RearCombinationOutsideRoundhouse = 33097, // cast, cactbot trigger
    FrontCombinationOutsideRoundhouse = 33095, // cast, cactbot trigger
    EclipticMeteor2 = 33083, // cast, cactbot trigger
    FrontCombinationInsideRoundhouse = 33096, // cast, cactbot trigger
}

public enum IconID : uint
{
    Icon161 = 161, // cactbot
    Icon314 = 314, // cactbot
    Icon453 = 453, // cactbot
}

class P9NKokytosStates : StateMachineBuilder
{
    public P9NKokytosStates(BossModule module) : base(module)
    {
        // TODO: stub from cactbot IDs + Teamcraft OIDs; components and states from the first replay
        TrivialPhase();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 936, NameID = 12369)]
public class P9NKokytos(ModuleInit init) : BossModule(init, new(100, 100), new ArenaBoundsCircle(20));
