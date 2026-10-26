namespace BossMod.Endwalker.Raid.P4NHesperos;

public enum OID : uint
{
    Boss = 0x3602, // Teamcraft index
    ExplosiveAether = 0x3603, // Teamcraft index
    Helper = 0x233C, // Helper type
}

public enum AID : uint
{
    ElegantEvisceration = 27216, // Hesperos, cast, cactbot ~15.8s
    SettingTheScene1 = 27083, // Hesperos, cactbot ~31.0s
    Pinax = 27087, // Hesperos, cactbot ~43.2s
    LevinstrikePinax = 27199, // Hesperos, cast, cactbot ~53.3s
    Decollation = 27217, // Hesperos, cast, cactbot ~65.4s
    WellPinax = 27198, // Hesperos, cast, cactbot ~98.9s
    HellSkewer = 27215, // Hesperos, cast, cactbot ~112.1s
    AcidPinax = 27196, // Hesperos, cast, cactbot ~165.7s
    Bloodrake = 27200, // Hesperos, cast, cactbot ~199.3s
    BeloneCoils = 27101, // Hesperos, cast, cactbot ~208.4s
    DirectionalShift1 = 27210, // Hesperos, cast, cactbot ~248.4s
    DirectionalShift2 = 27211, // Hesperos, cast, cactbot ~248.4s
    DirectionalShift3 = 27212, // Hesperos, cast, cactbot ~248.4s
    DirectionalShift4 = 27213, // Hesperos, cast, cactbot ~248.4s
    ShiftingStrike1 = 27214, // Hesperos, cactbot ~249.9s
    DirectionalShift5 = 28078, // Hesperos, cast, cactbot ~266.2s
    DirectionalShift6 = 28079, // Hesperos, cast, cactbot ~266.2s
    DirectionalShift7 = 28080, // Hesperos, cast, cactbot ~266.2s
    DirectionalShift8 = 28081, // Hesperos, cast, cactbot ~266.2s
    ShiftingStrike2 = 28082, // Hesperos, cactbot ~267.9s
    BeloneBursts = 27097, // Hesperos, cast, cactbot ~300.5s
    Pinax1 = 27197, // Hesperos, cast, cactbot ~368.4s
}

class P4NHesperosStates : StateMachineBuilder
{
    public P4NHesperosStates(BossModule module) : base(module)
    {
        // TODO: stub from cactbot IDs + Teamcraft OIDs; components and states from the first replay
        TrivialPhase();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 800, NameID = 10742)]
public class P4NHesperos(ModuleInit init) : BossModule(init, new(100, 100), new ArenaBoundsSquare(20));
