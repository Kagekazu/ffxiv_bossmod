namespace BossMod.Endwalker.Trial.T05Rubicante;

public enum OID : uint
{
    Boss = 0x3AEE, // Teamcraft index
    Helper = 0x233C, // Helper type
}

public enum AID : uint
{
    Inferno1 = 31978, // Rubicante, cast, cactbot ~12.0s
    HopeAbandonYe = 32550, // Rubicante, cactbot ~17.3s
    ArcaneRevelation = 31934, // Rubicante, cactbot ~26.7s
    OrdealOfPurgation1 = 31940, // Rubicante, cast, cactbot ~47.7s
    FieryExpiation = 31941, // Circle of Purgatory, cactbot ~52.7s
    ShatteringHeat1 = 31979, // Rubicante, cast, cactbot ~86.9s
    ArchInferno = 31943, // Rubicante, cactbot ~102.1s
    InfernoDevil = 31945, // Rubicante, cactbot ~102.1s
    InfernalSlaughter = 31950, // Rubicante, cactbot ~105.2s
    Conflagration = 31947, // Rubicante, cactbot ~112.2s
    RadialFlagration = 31948, // Rubicante, cactbot ~122.3s
    OrdealOfPurgation2 = 33000, // Rubicante, cast, cactbot ~163.1s
    GhastlyTorch = 31956, // Greater Flamesent, cactbot ~1011.1s
    GhastlyFlame = 31959, // Flamesent, cactbot ~1012.1s
    GhastlyWind = 31958, // Flamesent, cactbot ~1016.2s
    ShatteringHeat2 = 31957, // Flamesent, cactbot ~1022.2s
    BlazingRapture = 31954, // Rubicante, cast, cactbot ~2000.0s
    Inferno2 = 31962, // Rubicante, cast, cactbot ~2048.5s
    Flamerake1 = 31967, // Rubicante, cactbot ~2064.6s
    Flamerake2 = 31968, // Rubicante, cactbot ~2065.6s
    Flamerake3 = 31969, // Rubicante, cactbot ~2066.6s
    Soulscald = 31974, // Rubicante, cactbot ~2082.0s
    Dualfire = 32149, // Rubicante, cast, cactbot ~2099.3s
    SweepingImmolation = 31971, // Rubicante, cast, cactbot ~2114.1s
    TotalImmolation = 31973, // Rubicante, cactbot ~2114.5s
    Flamerake = 31965, // cast, cactbot trigger
}

public enum IconID : uint
{
    Icon230 = 230, // cactbot
}

class T05RubicanteStates : StateMachineBuilder
{
    public T05RubicanteStates(BossModule module) : base(module)
    {
        // TODO: stub from cactbot IDs + Teamcraft OIDs; components and states from the first replay
        TrivialPhase();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 886, NameID = 12054)]
public class T05Rubicante(ModuleInit init) : BossModule(init, new(100, 100), new ArenaBoundsCircle(20));
