namespace BossMod.Endwalker.Raid.P3NPhoinix;

public enum OID : uint
{
    Boss = 0x353B, // Teamcraft index
    Sparkfledged = 0x353C, // Teamcraft index
    Sunbird = 0x353E, // Teamcraft index
    Helper = 0x233C, // Helper type
}

public enum AID : uint
{
    ExperimentalFireplume1 = 26262, // Phoinix, cactbot ~13.8s
    ExperimentalFireplume2 = 26264, // Phoinix, cast, cactbot ~13.8s
    ScorchedExaltation = 26296, // Phoinix, cast, cactbot ~49.2s
    HeatOfCondemnation = 26290, // Phoinix, cast, cactbot ~61.4s
    DarkenedFire = 28378, // Phoinix, cactbot ~75.6s
    LeftCinderwing = 26292, // Phoinix, cast, cactbot ~105.6s
    RightCinderwing = 26293, // Phoinix, cast, cactbot ~105.6s
    TrailOfCondemnation = 26285, // Phoinix, cast, cactbot ~118.0s
    BlazingRain = 26274, // Phoinix, cactbot ~127.0s
    FlamesOfUndeath = 26275, // Phoinix, cactbot ~200.0s
    JointPyre = 26277, // Sparkfledged, cactbot ~206.2s
    DeadRebirth = 26281, // Phoinix, cast, cactbot ~221.2s
    FledglingFlight = 26282, // Phoinix, cactbot ~239.6s
    ExperimentalCharplume = 26268, // Phoinix, cactbot ~257.7s
    Charplume = 26269, // Phoinix, cactbot ~262.8s
    DevouringBrand1 = 26272, // Phoinix, cactbot ~291.3s
    SearingBreeze = 26295, // Phoinix, cactbot ~308.7s
    DevouringBrand2 = 26270, // Phoinix, cast, cactbot ~422.1s
    FlareOfCondemnation = 26288, // Phoinix, cactbot ~518.9s
    AshenEye = 26283, // cast, cactbot trigger
}

public enum IconID : uint
{
    Icon139 = 139, // cactbot
}

class P3NPhoinixStates : StateMachineBuilder
{
    public P3NPhoinixStates(BossModule module) : base(module)
    {
        // TODO: stub from cactbot IDs + Teamcraft OIDs; components and states from the first replay
        TrivialPhase();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 806, NameID = 10720)]
public class P3NPhoinix(ModuleInit init) : BossModule(init, new(100, 100), new ArenaBoundsCircle(20));
