namespace BossMod.Endwalker.Trial.T06Golbez;

public enum OID : uint
{
    Boss = 0x3F54, // Teamcraft index
    ShadowDragon = 0x3F55, // Teamcraft index
    GaleSphere = 0x3F56, // Teamcraft index
    GolbezShadow = 0x3F5A, // Teamcraft index
    Helper = 0x233C, // Helper type
}

public enum AID : uint
{
    Terrastorm = 33893, // Golbez, cactbot ~25.2s
    CrescentBlade = 33899, // Golbez, cast, cactbot ~30.3s
    ArcticAssault = 33886, // Golbez, cactbot ~42.1s
    VoidMeteor = 33964, // Golbez, cast, cactbot ~60.9s
    LingeringSpark = 33895, // Golbez, cactbot ~74.2s
    BindingCold = 33970, // Golbez, cast, cactbot ~97.1s
    AzdajasShadow1 = 34558, // Golbez, cactbot ~113.6s
    BlackFang = 33905, // Golbez, cast, cactbot ~132.6s
    AzdajasShadow2 = 33910, // Golbez, cactbot ~156.8s
    ShadowCrescent = 33927, // Golbez, cast, cactbot ~167.1s
    RisingBeacon = 33935, // Golbez, cactbot ~169.4s
    BurningShade = 33939, // Golbez, cactbot ~172.2s
    Explosion1 = 33945, // Golbez, cactbot ~186.7s
    DoubleMeteor = 33972, // Golbez, cactbot ~191.1s
    GaleSphere = 33869, // Golbez, cactbot ~205.6s
    VoidStardust1 = 33955, // Golbez, cactbot ~234.3s
    VoidStardust2 = 33957, // Golbez, cactbot ~238.2s
    EventideFall = 33922, // Golbez, cast, cactbot ~285.2s
    ImmolatingShade = 33941, // Golbez, cast, cactbot ~331.4s
    Explosion2 = 33946, // Golbez, cactbot ~410.9s
}

class T06GolbezStates : StateMachineBuilder
{
    public T06GolbezStates(BossModule module) : base(module)
    {
        // TODO: stub from cactbot IDs + Teamcraft OIDs; components and states from the first replay
        TrivialPhase();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 949, NameID = 12365)]
public class T06Golbez(ModuleInit init) : BossModule(init, new(100, 100), new ArenaBoundsSquare(15));
