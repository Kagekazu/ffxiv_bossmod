namespace BossMod.Endwalker.Raid.P6NHegemone;

public enum OID : uint
{
    Boss = 0x3A35, // Teamcraft index
    Parasitos = 0x3A36, // Teamcraft index
    Helper = 0x233C, // Helper type
}

public enum AID : uint
{
    AethericPolyominoid = 30803, // Hegemone, cactbot ~10.1s
    PolyominousDarkIV = 30805, // Hegemone, cactbot ~20.0s
    ChorosIxou11 = 30807, // Hegemone, cast, cactbot ~30.9s
    ChorosIxou12 = 30808, // Hegemone, cast, cactbot ~30.9s
    HemitheossDarkIV = 30798, // Hegemone, cast, cactbot ~62.6s
    Transmission1 = 30799, // Hegemone, cactbot ~74.3s
    ReekHavoc1 = 31213, // Parasitos, cactbot ~91.1s
    Synergy = 30811, // Hegemone, cactbot ~109.9s
    StropheIxou = 31249, // Hegemone, cactbot ~149.1s
    DarkAshes = 30815, // Hegemone, cactbot ~211.8s
    AetherialExchange = 30797, // Hegemone, cactbot ~244.1s
    PolyominoidSigma = 30806, // Hegemone, cactbot ~250.8s
}

public enum IconID : uint
{
    Icon101 = 101, // cactbot
    Icon167 = 167, // cactbot
    Icon168 = 168, // cactbot
    Icon343 = 343, // cactbot
}

class P6NHegemoneStates : StateMachineBuilder
{
    public P6NHegemoneStates(BossModule module) : base(module)
    {
        // TODO: stub from cactbot IDs + Teamcraft OIDs; components and states from the first replay
        TrivialPhase();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 880, NameID = 11381)]
public class P6NHegemone(ModuleInit init) : BossModule(init, new(100, 100), new ArenaBoundsSquare(20));
