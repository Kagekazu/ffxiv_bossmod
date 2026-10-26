namespace BossMod.Endwalker.Raid.P11NThemis;

public enum OID : uint
{
    Boss = 0x3EEC, // Teamcraft index
    IllusoryThemis = 0x3EED, // Teamcraft index
    ArcaneSphere1 = 0x3EEE, // Teamcraft index
    ArcaneSphere2 = 0x3EEF, // Teamcraft index
    ThymouIdea = 0x3EF0, // Teamcraft index
    ArcaneCylinder = 0x3EF1, // Teamcraft index
    Helper = 0x233C, // Helper type
}

public enum AID : uint
{
    Eunomia = 33250, // Themis, cast, cactbot ~12.1s
    DivisiveRuling1 = 33205, // Themis, cactbot ~25.5s
    RipplesOfGloom1 = 33207, // Themis, cactbot ~28.2s
    RipplesOfGloom2 = 33208, // Themis, cactbot ~28.2s
    DivineRuination1 = 33206, // Themis, cactbot ~40.4s
    DarkAndLight = 33219, // Themis, cactbot ~73.1s
    SigilsOfDiscord = 33220, // Themis, cactbot ~80.2s
    Dineis = 34597, // Themis, cast, cactbot ~90.3s
    EmissarysWill = 33223, // Themis, cactbot ~100.3s
    DismissalRuling1 = 33275, // Themis, cast, cactbot ~112.4s
    OuterDark = 34548, // Themis, cactbot ~116.0s
    DismissalRuling2 = 33274, // Themis, cast, cactbot ~123.1s
    InnerLight = 34547, // Themis, cactbot ~126.7s
    Dike = 33251, // Themis, cactbot ~132.8s
    UpheldRuling1 = 33210, // Themis, cactbot ~186.4s
    DarkPerimeter = 33214, // Themis, cactbot ~193.0s
    UpheldRuling2 = 33209, // Themis, cactbot ~201.0s
    Lightburst = 33213, // Themis, cactbot ~206.7s
    Styx = 33243, // Themis, cactbot ~216.3s
    ShadowedMessengers = 33236, // Themis, cactbot ~346.1s
    DivisiveRuling2 = 33239, // Illusory Themis, cast, cactbot ~357.6s
    DivineRuination2 = 33240, // Illusory Themis, cactbot ~360.7s
    RipplesOfGloom3 = 33241, // Illusory Themis, cactbot ~360.7s
    RipplesOfGloom4 = 33242, // Illusory Themis, cactbot ~360.7s
    BlindingLight = 33249, // Themis, cactbot ~367.0s
    DivisiveRulingOut = 33203, // cast, cactbot trigger
    DivisiveRulingIn = 33204, // cast, cactbot trigger
}

public enum IconID : uint
{
    Icon218 = 218, // cactbot
    Icon305 = 305, // cactbot
    Icon318 = 318, // cactbot
    Icon463 = 463, // cactbot
    Icon466 = 466, // cactbot
}

class P11NThemisStates : StateMachineBuilder
{
    public P11NThemisStates(BossModule module) : base(module)
    {
        // TODO: stub from cactbot IDs + Teamcraft OIDs; components and states from the first replay
        TrivialPhase();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 940, NameID = 12388)]
public class P11NThemis(ModuleInit init) : BossModule(init, new(100, 100), new ArenaBoundsCircle(20));
