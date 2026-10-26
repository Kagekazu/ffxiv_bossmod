namespace BossMod.Endwalker.Trial.T04Barbariccia;

public enum OID : uint
{
    Boss = 0x39D1, // Teamcraft index
    BarbaricciaShadow = 0x39D2, // Teamcraft index
    StiffBreeze = 0x39D3, // Teamcraft index
    Helper = 0x233C, // Helper type
}

public enum AID : uint
{
    VoidAeroIV = 30134, // Barbariccia, cast, cactbot ~15.1s
    SavageBarbery1 = 30145, // Barbariccia, cactbot ~34.6s
    HairRaid = 30146, // Barbariccia, cast, cactbot ~41.6s
    VoidAeroIII = 30135, // Barbariccia, cast, cactbot ~50.8s
    SavageBarbery2 = 30138, // Barbariccia, cast, cactbot ~67.3s
    TeasingTangles1 = 30174, // Barbariccia, cactbot ~101.4s
    Fetters = 30128, // Barbariccia, cactbot ~102.2s
    SecretBreeze1 = 30148, // Barbariccia, cactbot ~111.5s
    SecretBreeze2 = 30149, // Barbariccia, cactbot ~114.5s
    BrushWithDeath1 = 30115, // Barbariccia, cactbot ~118.7s
    DeadlyTwist = 30172, // Barbariccia, cactbot ~124.5s
    BrushWithDeath2 = 30116, // Barbariccia, cactbot ~143.1s
    HairSpray = 30171, // Barbariccia, cactbot ~160.0s
    VoidAero1 = 29802, // Barbariccia, cactbot ~205.7s
    VoidAero2 = 29803, // Barbariccia, cactbot ~210.5s
    CurlingIron = 30130, // Barbariccia, cactbot ~219.5s
    Voidstrom = 30141, // Barbariccia, cactbot ~225.3s
    Catabasis = 30179, // Barbariccia, cactbot ~230.3s
    BrutalRush1 = 30150, // Barbariccia, cactbot ~262.4s
    BrutalRush4 = 30151, // Barbariccia, cactbot ~267.3s
    WindingGale = 30154, // Barbariccia, cactbot ~270.1s
    BrushWithDeath3 = 30117, // Barbariccia, cactbot ~273.6s
    BoulderBreak = 29647, // Barbariccia, cast, cactbot ~295.6s
    KnuckleDrum = 30103, // Barbariccia, cactbot ~295.7s
    Trample = 30170, // Barbariccia, cactbot ~317.8s
    BoldBoulder = 30166, // Barbariccia, cast, cactbot ~317.9s
    TornadoChain1 = 30094, // Barbariccia, cactbot ~325.1s
    TornadoChain2 = 30095, // Barbariccia, cactbot ~327.7s
    BlowAway = 30101, // Barbariccia, cactbot ~351.3s
    TeasingTangles2 = 30175, // Barbariccia, cactbot ~362.5s
    Touchdown = 29805, // Barbariccia, cast, cactbot ~375.3s
    Impact = 30168, // Barbariccia, cast, cactbot ~404.3s
    DryBlows = 30099, // Barbariccia, cactbot ~406.7s
    SavageBarberyLine = 30144, // cast, cactbot trigger
}

public enum IconID : uint
{
    Icon62 = 62, // cactbot
    Icon100 = 100, // cactbot
    Icon352 = 352, // cactbot
    Icon373 = 373, // cactbot
}

class T04BarbaricciaStates : StateMachineBuilder
{
    public T04BarbaricciaStates(BossModule module) : base(module)
    {
        // TODO: stub from cactbot IDs + Teamcraft OIDs; components and states from the first replay
        TrivialPhase();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 870, NameID = 10298)]
public class T04Barbariccia(ModuleInit init) : BossModule(init, new(100, 100), new ArenaBoundsCircle(20));
