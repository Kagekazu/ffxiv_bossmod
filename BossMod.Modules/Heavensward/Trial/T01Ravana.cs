namespace BossMod.Heavensward.Trial.T01Ravana;

public enum OID : uint
{
    Boss = 0xEB4, // R3.500, Ravana
    MoonGana = 0xEB6, // R1.000
    SpiritGana = 0xEB8, // R0.600
    RavanasWill = 0xEBA, // R1.000
    Chandrahas = 0xEBC, // R0.700
    IronGate = 0x10A1, // R7.000
    HelperA = 0x1201, // R3.500
    HelperB = 0x1335, // R3.500
    Helper = 0x13D2, // R0.500
}

public enum AID : uint
{
    AutoAttack = 5093, // Boss->player, no cast, single-target
    DragonflyAvatar = 3712, // Boss->self, no cast, single-target
    ScorpionAvatar = 3713, // Boss->self, no cast, single-target
    BeetleAvatar = 3714, // Boss->self, no cast, single-target
    BlindingBlade = 3715, // Boss->player, no cast, range 7 tankbuster
    TheSeeingTail = 3716, // Boss->self, 1.2s cast, single-target
    TheSeeingWing = 3717, // Boss->self, 1.2s cast, single-target
    Revengeance = 3718, // Helper->self, no cast, single-target
    PreludeToSlaughterCast = 3719, // Boss->self, 14.7s cast, range 15 circle
    PreludeToSlaughterVisual = 3720, // Boss->self, 2.7s cast, single-target
    PreludeToSlaughterRect = 3721, // Helper->self, no cast, range 40 width 8 rect
    PreludeToSlaughterCircle = 3722, // Boss->self, 2.7s cast, range 20 circle
    SlaughterCast = 3723, // Boss->self, 15.7s cast, range 40 cone
    SlaughterVisual = 3724, // Boss->self, 2.7s cast, single-target
    SlaughterRect = 3725, // Helper->self, 3.7s cast, range 44 width 8 rect
    SlaughterCircle = 3726, // HelperA->self, no cast, range 12 circle
    TapasyaNear = 3727, // Boss->self, no cast, range 9 cone
    TapasyaFar = 3728, // Helper->self, no cast, range 12 cone
    FallingLaughter = 3730, // MoonGana/SpiritGana->self, 9.7s cast, single-target
    BloodyFuller = 3731, // Boss->self, 4.7s cast, range 100 circle
    ChandrahasSpawn = 3732, // Chandrahas->self, no cast
    LaughingMoon = 3734, // HelperA->self, no cast, range 40
    ChandrahasRaidwide = 3735, // HelperB->self, no cast, range 100 circle
    TheRoseOfConviction = 3736, // Boss->self, no cast, single-target
    TheRoseOfConquest = 3737, // RavanasWill->players, no cast, range 6 circle
    PillarsOfHeaven = 3738, // Boss->self, 2.7s cast, range 40 circle
    Surpanakha = 3739, // Boss->self, no cast, range 40 cone
    TheRoseOfHate = 3740, // Boss->self, 2.7s cast, range 40 width 8 rect
    SwiftSlaughter = 3741, // Boss->self, 16.7s cast, single-target enrage
    BladesOfCarnageAndLiberation = 4986, // Boss->self, no cast, single-target
    FireAdd = 5015, // MoonGana->player, 0.7s cast, single-target
    BlizzardAdd = 5016, // SpiritGana->player, 0.7s cast, single-target
    SlaughterCross = 5052, // Helper->self, 3.7s cast, range 40 width 8 rect
    PreludeCircleRepeat = 5059, // Helper->self, 2.7s cast, range 20 circle
}

public enum IconID : uint
{
    Stack = 41,
    Icon50 = 50,
    Icon51 = 51,
    Icon52 = 52,
    Icon53 = 53,
    Icon57 = 57,
}

public enum TetherID : uint
{
    Will = 17, // RavanasWill->player
}

class PreludeToSlaughterCast(BossModule module) : Components.StandardAOEs(module, AID.PreludeToSlaughterCast, 15);
class PreludeToSlaughterCircle(BossModule module) : Components.StandardAOEs(module, AID.PreludeToSlaughterCircle, 20);
class PreludeCircleRepeat(BossModule module) : Components.StandardAOEs(module, AID.PreludeCircleRepeat, 20);
class SlaughterRect(BossModule module) : Components.StandardAOEs(module, AID.SlaughterRect, new AOEShapeRect(44, 4));
class SlaughterCross(BossModule module) : Components.StandardAOEs(module, AID.SlaughterCross, new AOEShapeRect(40, 4));
class TheRoseOfHate(BossModule module) : Components.StandardAOEs(module, AID.TheRoseOfHate, new AOEShapeRect(40, 4));
class BloodyFuller(BossModule module) : Components.RaidwideCast(module, AID.BloodyFuller);
class PillarsOfHeaven(BossModule module) : Components.RaidwideCast(module, AID.PillarsOfHeaven);
class SwiftSlaughter(BossModule module) : Components.CastHint(module, AID.SwiftSlaughter, "Enrage!", true);
class RavanaAdds(BossModule module) : Components.AddsMulti(module, [OID.MoonGana, OID.SpiritGana, OID.Chandrahas, OID.IronGate], 1);

class T01RavanaStates : StateMachineBuilder
{
    public T01RavanaStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<PreludeToSlaughterCast>()
            .ActivateOnEnter<PreludeToSlaughterCircle>()
            .ActivateOnEnter<PreludeCircleRepeat>()
            .ActivateOnEnter<SlaughterRect>()
            .ActivateOnEnter<SlaughterCross>()
            .ActivateOnEnter<TheRoseOfHate>()
            .ActivateOnEnter<BloodyFuller>()
            .ActivateOnEnter<PillarsOfHeaven>()
            .ActivateOnEnter<SwiftSlaughter>()
            .ActivateOnEnter<RavanaAdds>();
    }
}

[ModuleInfo(Contributors = "Kagekazu", Incomplete = true, GroupType = BossModuleInfo.GroupType.CFC, GroupID = 86, NameID = 3660)]
public class T01Ravana(WorldState ws, Actor primary) : BossModule(ws, primary, new(0, 0), new ArenaBoundsCircle(20));
