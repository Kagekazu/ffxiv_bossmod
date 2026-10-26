namespace BossMod.Endwalker.Raid.P7NAgdistis;

public enum OID : uint
{
    Boss = 0x3A28, // Teamcraft index
    ForbiddenFruit1 = 0x3A29, // Teamcraft index
    ForbiddenFruit2 = 0x3A2A, // Teamcraft index
    ImmatureIo = 0x3A2B, // Teamcraft index
    ImmatureStymphalide = 0x3A2C, // Teamcraft index
    Helper = 0x233C, // Helper type
}

public enum AID : uint
{
    BoughOfAttis1 = 30713, // Agdistis, cast, cactbot ~15.8s
    BoughOfAttisLeft = 30715, // Agdistis, cast, cactbot ~23.4s
    Right = 30716, // Agdistis, cast, cactbot ~23.4s
    HemitheossHoly = 30728, // Agdistis, cactbot ~37.6s
    HemitheossGlareIII1 = 30712, // Agdistis, cactbot ~44.0s
    BoughOfAttis2 = 30718, // Agdistis, cast, cactbot ~54.1s
    ImmortalsObol = 30709, // Agdistis, cast, cactbot ~81.9s
    HemitheossAeroII = 30729, // Agdistis, cactbot ~119.7s
    SparkOfLife = 30731, // Agdistis, cast, cactbot ~128.9s
    ForbiddenFruit = 30721, // Agdistis, cactbot ~143.2s
    StaticMoon = 30722, // Immature Io, cast, cactbot ~154.9s
    BladesOfAttis = 30725, // Agdistis, cast, cactbot ~166.9s
    StymphalianStrike = 30723, // Immature Stymphalide, cast, cactbot ~195.8s
    HemitheossGlareIII2 = 31226, // Agdistis, cast, cactbot ~216.9s
    ShadowOfAttis = 30780, // Agdistis, cactbot ~492.3s
    Burst = 30781, // Agdistis, cactbot ~505.4s
    HemitheossAeroIV = 30784, // Agdistis, cast, cactbot ~576.9s
}

public enum IconID : uint
{
    Icon311 = 311, // cactbot
    Icon364 = 364, // cactbot
}

class P7NAgdistisStates : StateMachineBuilder
{
    public P7NAgdistisStates(BossModule module) : base(module)
    {
        // TODO: stub from cactbot IDs + Teamcraft OIDs; components and states from the first replay
        TrivialPhase();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 876, NameID = 11374)]
public class P7NAgdistis(ModuleInit init) : BossModule(init, new(100, 100), new ArenaBoundsCircle(27));
