namespace BossMod.Endwalker.Raid.P8NHephaistos;

public enum OID : uint
{
    Boss = 0x3AC5, // Teamcraft index
    Gorgon = 0x3AC8, // Teamcraft index
    Helper = 0x233C, // Helper type
}

public enum AID : uint
{
    GenesisOfFlame = 30981, // Hephaistos, cast, cactbot ~12.0s
    Sunforge1 = 30956, // Hephaistos, cactbot ~26.4s
    ScorchingFang = 30958, // Hephaistos, cactbot ~27.4s
    Sunforge2 = 30957, // Hephaistos, cactbot ~36.6s
    SunsPinion = 30959, // Hephaistos, cactbot ~37.6s
    Flameviper = 30984, // Hephaistos, cast, cactbot ~56.1s
    ReforgedReflection1 = 31051, // Hephaistos, cactbot ~68.5s
    QuadrupedalCrush = 30980, // Hephaistos, cactbot ~179.1s
    BlazingFootfalls1 = 30976, // Hephaistos, cactbot ~191.6s
    Trailblaze = 30989, // Hephaistos, cactbot ~192.3s
    QuadrupedalImpact = 28931, // Hephaistos, cactbot ~194.6s
    RearingRampage = 31147, // Hephaistos, cast, cactbot ~208.0s
    FourfoldFires = 30962, // Hephaistos, cactbot ~226.8s
    AbyssalFires = 30961, // Hephaistos, cast, cactbot ~232.6s
    CthonicVent1 = 30965, // Suneater, cast, cactbot ~241.8s
    CthonicVent2 = 31053, // Suneater, cactbot ~249.8s
    CthonicVent3 = 30966, // Suneater, cactbot ~257.8s
    ReforgedReflection2 = 31052, // Hephaistos, cactbot ~284.7s
    IntoTheShadows = 30971, // Hephaistos, cactbot ~297.0s
    Petrifaction1 = 26403, // cactbot ~306.1s
    Gorgospit1 = 30975, // Hephaistos, cactbot ~312.3s
    Ektothermos = 30974, // Hephaistos, cast, cactbot ~327.7s
    VolcanicTorches1 = 30967, // Hephaistos, cactbot ~348.7s
    TorchFlame = 30968, // Hephaistos, cast, cactbot ~657.7s
    HemitheossFlare1 = 30982, // Hephaistos, cactbot ~669.9s
    HemitheossFlare2 = 30983, // Hephaistos, cactbot ~675.9s
    BlazingFootfalls2 = 30986, // Hephaistos, cactbot ~837.0s
    Gorgospit2 = 28631, // Illusory Hephaistos, cactbot ~977.7s
    VolcanicTorches2 = 29150, // Hephaistos, cactbot ~977.7s
    Petrifaction2 = 30972, // cast, cactbot trigger
}

class P8NHephaistosStates : StateMachineBuilder
{
    public P8NHephaistosStates(BossModule module) : base(module)
    {
        // TODO: stub from cactbot IDs + Teamcraft OIDs; components and states from the first replay
        TrivialPhase();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 883, NameID = 11399)]
public class P8NHephaistos(ModuleInit init) : BossModule(init, new(100, 100), new ArenaBoundsSquare(20));
