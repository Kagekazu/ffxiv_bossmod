namespace BossMod.Heavensward.Trial.T02Bismarck;

public enum OID : uint
{
    Boss = 0xEF8, // R6.000, Bismarck
    Helper = 0xEF9, // R0.500
    VaporBubble = 0xEFA, // R1.200, NameID 3834
    LanMaiiVundu = 0xEFC, // R1.500
    VukMaiiVundu = 0xEFD, // R1.300
    SoSanuwa = 0xEFE, // R3.500
    FunnelCloud = 0xEFF, // R1.000
    UlSanuwa = 0xF00, // R3.500
    MagitekFieldGenerator = 0xF60, // R1.000
    ChitinCarapace = 0x1147, // R5.000, Part, NameID 4423 — first shell target
    Corona = 0x1148, // R5.000, Part, NameID 4424 — second shell target
    Dragonkiller = 0x12EB, // R1.000, cannon actor once armed
    HelperB = 0x1214, // R0.500
    HelperC = 0x1303, // R1.000
    DragonkillerNorth = 0x1E9A28, // EventObj 2005544 (~-13, -17)
    DragonkillerSouth = 0x1E9A29, // EventObj 2005545 (~-14, 19)
}

public enum AID : uint
{
    AutoAttackAdd = 872, // LanMaiiVundu->player, no cast, single-target
    BaleenBombVisual = 4011, // Boss->self, 3.0s cast, single-target
    LightningBoltVisual = 4016, // Boss->self, 3.0s cast, single-target
    BismarckHit4017 = 4017, // Boss->self, no cast
    BismarckHit4019 = 4019, // Boss->self, no cast
    Windcaller = 4021, // Boss->self, 3.0s cast, single-target
    VaporBubbleHit = 4024, // VaporBubble->self, no cast
    PowerfulGust = 4029, // VukMaiiVundu->player, 1.0s cast, single-target
    DryFin = 4031, // SoSanuwa->self, no cast, range 9+R cone
    WetFin = 4033, // UlSanuwa->self, no cast, range 9+R cone
    HelperHit4038 = 4038, // Helper->self, no cast
    BismarckHit4039 = 4039, // Boss->self, no cast
    BismarckHit4040 = 4040, // Boss->self, no cast
    BismarckHit4041 = 4041, // Boss->self, no cast
    DragonkillerHit = 4042, // Dragonkiller->self, no cast
    Unknown4765 = 4765, // Helper->self, no cast, range 4
    GeneratorHit = 4778, // MagitekFieldGenerator->self, no cast
    ThunderheadVisual = 4906, // Boss->self, 3.0s cast, single-target
    CetaceanRage = 4918, // Boss->self, 6.0s cast, range 50
    BismarckHit4925 = 4925, // Boss->self, no cast
    BaleenBomb = 4932, // Helper->self, 3.5s cast, range 5 circle
    VacuumWave = 4933, // Helper->self, no cast, range 100 circle
    Thunderhead = 4935, // Helper->self, 3.0s cast, range 5
    LightningBolt = 4936, // Helper->self, 4.0s cast, range 4 circle
    HelperHit5062 = 5062, // Helper->self, no cast
    HelperHit5064 = 5064, // Helper->self, no cast
}

public enum TetherID : uint
{
    Tether35 = 35,
    Tether39 = 39,
    Tether40 = 40,
}

class BaleenBomb(BossModule module) : Components.StandardAOEs(module, AID.BaleenBomb, 5);
class LightningBolt(BossModule module) : Components.StandardAOEs(module, AID.LightningBolt, 4);
class Thunderhead(BossModule module) : Components.StandardAOEs(module, AID.Thunderhead, 5);
class CetaceanRage(BossModule module) : Components.RaidwideCast(module, AID.CetaceanRage);
class PowerfulGust(BossModule module) : Components.SingleTargetCast(module, AID.PowerfulGust);
class BismarckAdds(BossModule module) : Components.AddsMulti(module, [OID.LanMaiiVundu, OID.VukMaiiVundu, OID.SoSanuwa, OID.UlSanuwa, OID.VaporBubble, OID.FunnelCloud, OID.Dragonkiller], 1);

class T02BismarckStates : StateMachineBuilder
{
    public T02BismarckStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<BaleenBomb>()
            .ActivateOnEnter<LightningBolt>()
            .ActivateOnEnter<Thunderhead>()
            .ActivateOnEnter<CetaceanRage>()
            .ActivateOnEnter<PowerfulGust>()
            .ActivateOnEnter<BismarckAdds>();
    }
}

[ModuleInfo(Contributors = "Kagekazu", Incomplete = true, GroupType = BossModuleInfo.GroupType.CFC, GroupID = 88, NameID = 3649)]
public class T02Bismarck(WorldState ws, Actor primary) : BossModule(ws, primary, new(0, 0), new ArenaBoundsCircle(24));
