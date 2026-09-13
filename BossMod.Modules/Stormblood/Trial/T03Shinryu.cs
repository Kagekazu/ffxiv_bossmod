namespace BossMod.Stormblood.Trial.T03Shinryu;

public enum OID : uint
{
    Boss = 0x1983, // R22.000, x?
    Platform = 0x1EA1A1, // R2.000, x?, EventObj type : Destructible square tiles. 9 platforms.
    RightWing = 0x1B1A, // R15.000, x?, Part type
    LeftWing = 0x1B19, // R15.000, x?, Part type
    WaterSpout = 0x1E8536, // R2.000, x?, EventObj type : Water Spout
    WaterPuddles = 0x1E950D, // R0.500, x?, EventObj type
    Icicle = 0x1B16, // R2.500, x?
    EyeOfTheStorm = 0x1B17, // R1.000, Hurricane with pulsing knockback
    Cocoon1 = 0x1B13, // R3.000, x?
    MassiveCocoon = 0x1C86, // R6.000, x?
    Ginryu = 0x1B14, // R1.800, x?
    Hakkinryu = 0x1C83, // R3.600, x?
    Fetters = 0x1B15, // R1.000, x? （仮）temporary　鎖 : Fetters, then atb button mash.
    Tail = 0x1B12, // R17.940, x?, Helper type
    Helper = 0x18D6 // R0.500, x?, mixed types : Helper types~
}

public enum AID : uint
{
    AutoAttack = 8105, // RightWing/LeftWing->player, no cast, single-target
    TidalWave = 8075, // Helper->self, 10.0s cast, range 80+R width 60 rect
    TidalWaveCast = 8106, // Shinryu->self, 10.0s cast, single-target
    Levinbolt = 8092, // Helper->player, no cast, range 5 circle
    LevinboltVisual = 8091, // RightWing->self, 6.0s cast, single-target
    AkhMornVisual = 8100, // Shinryu->players, 4.0s cast, ??? : tank stack?
    AkhMorn1 = 8101, // Shinryu->players, no cast, ??? :
    SummonIcicle = 8095, // LeftWing->self, 4.0s cast, single-target
    IcicleImpact = 8096, // Icicle->self, no cast, range 6 circle
    Spikesicle = 8097, // Icicle->self, 2.5s cast, range 62+R width 10 rect
    Hellfire = 8076, // Helper->self, 10.0s cast, range 60 circle
    HellfireVisual = 8107, // Shinryu->self, 10.0s cast, single-target
    _Ability_ = 8074, // Shinryu->self, no cast, single-target
    MeteorImpact = 9291, // Helper->self, 5.0s cast, range 60 circle
    MeteorImpact1 = 8086, // Cocoon1/MassiveCocoon->self, 4.0s cast, range 60 circle
    _Ability_1 = 8514, // Cocoon1/MassiveCocoon->self, no cast, single-target
    Attack1 = 870, // Hakkinryu/Ginryu->player, no cast, single-target
    Collapse = 8728, // Hakkinryu->self, no cast, range 8+R ?-degree cone
    Protostar = 8085, // Shinryu->self, 6.0s cast, range 80 circle
    Protostar1 = 8123, // Helper->self, no cast, range 50 circle
    DarkMatter = 8088, // Shinryu->self, 3.0s cast, range 60 circle
    _Ability_2 = 8488, // Shinryu->self, no cast, single-target
    GyreCharge = 8104, // Shinryu->self, no cast, range 100+R width 60 rect
    GyreChargeVisual = 8180, // Helper->self, 6.3s cast, range 100+R width 60 rect
    _Ability_3 = 8081, // Shinryu->self, no cast, single-target
    TailSlap1 = 8083, // Tail->self, 3.0s cast, range 40 width 20 rect
    TailSlap2 = 9130, // Tail->self, 3.0s cast, range 40 width 20 rect

    _Ability_4 = 8084, // Tail->self, no cast, single-target
    _Ability_5 = 8142, // Shinryu->self, no cast, single-target
    _Ability_6 = 8082, // Shinryu->self, no cast, single-target
    IceStorm = 8098, // LeftWing->self, 6.0s cast, single-target
    BurningChains = 8144, // Helper->self, no cast : Burning chains tether on two players. Run apart to break chains
    IceStormRaidwide = 8099, // Helper->self, no cast, range 60 circle
    _Ability_7 = 8143, // Shinryu->self, no cast, single-target
    Dragonfist = 9455, // Shinryu->self, no cast, single-target
    DragonfistVisual = 9456, // Helper->self, 4.0s cast, range 16 circle
    DiamondDust = 8078, // Helper->self, 10.0s cast, range 60 circle
    DiamondDust1 = 8109, // Shinryu->self, 10.0s cast, single-target
    Fireball = 8732, // Ginryu->location, 2.5s cast, range 4 circle
    DeathSentence = 8731, // Hakkinryu->player, 4.0s cast, single-target
    SpikedTail = 8729, // Ginryu->player, 1.0s cast, single-target

    JudgmentBolt = 8077, // Helper->self, 10.0s cast, range 60 circle
    JudgmentBolt1 = 8108, // Shinryu->self, 10.0s cast, single-target

    EarthenFury = 8079, // Helper->self, 10.0s cast, range 60 circle
    EarthenFuryCast = 8110, // Shinryu->self, 10.0s cast, single-target
    EarthenFurySmash = 9146, // 18D6->location, Range 20, width 20 rectangle : cracks or destroys one square.
    AkhRhai = 8102, // Helper->location, no cast, range 4 circle
    AkhRhai1 = 8103, // Helper->location, no cast, range 4 circle
    HypernovaCast = 8089, // RightWing->self, 6.0s cast, single-target
    Hypernova = 8090, // Helper->players, no cast, range 8 circle stack

    SuperCycloneKB = 8984, // Helper->self, 0.5s cast, range 90 circle : distance 5 knockback
    AerialBlastKB = 8080, // Helper->self, 10.0s cast, range 60 circle : distance 15 knockback
    AerialBlast1 = 8111, // Shinryu->self, 10.0s cast, single-target
    BlazingTrail = 8730, // Ginryu->self, 3.0s cast, range 15+R width 11 rect

    EarthBreath = 8093, // Shinryu->self, 9.0s cast, range 6+R ?-degree cone
    EarthBreath1 = 8094, // Helper->self, 4.5s cast, range 80+R 60.000-degree cone
}

public enum IconID : uint
{
    LevinMarker = 24, // player : Levin Bolt Spread marker
    BurningChainsIcon = 97, // player :
    HyperNovaStackIcon = 62, // player->self
    BaitAwayIcon = 98, // player->self : green marker.  Might bait a tail slap? Unconfirmed.
    EarthBreathIcon = 23 // player->self
}

public enum SID : uint
{
    FireResistanceUp = 520,
    LightningResistanceDownII = 1260,
    Paralysis = 17,
    Fetters = 667,
    Affixed = 1267,
    BurningChains = 769,
    ThinIce = 911
}

public enum TetherID : uint
{
    BurningTether = 9
}

class AkhMornVisual(BossModule module) : Components.StackWithCastTargets(module, AID.AkhMornVisual, 6);
class Hellfire(BossModule module) : Components.RaidwideCast(module, AID.Hellfire);
class MeteorImpact(BossModule module) : Components.RaidwideCast(module, AID.MeteorImpact);
class MeteorImpact1(BossModule module) : Components.RaidwideCast(module, AID.MeteorImpact1);
class Protostar(BossModule module) : Components.RaidwideCast(module, AID.Protostar);
class DarkMatter(BossModule module) : Components.RaidwideCast(module, AID.DarkMatter);
class TailSlap1(BossModule module) : Components.StandardAOEs(module, AID.TailSlap1, new AOEShapeRect(40, 10));
class TailSlap2(BossModule module) : Components.StandardAOEs(module, AID.TailSlap2, new AOEShapeRect(40, 10));
class Spikesicle(BossModule module) : Components.StandardAOEs(module, AID.Spikesicle, new AOEShapeRect(64.5f, 5));
class BlazingTrail(BossModule module) : Components.StandardAOEs(module, AID.BlazingTrail, new AOEShapeRect(16.8f, 5.5f));
class EarthBreath1(BossModule module) : Components.StandardAOEs(module, AID.EarthBreath1, new AOEShapeCone(80, 30.Degrees()));
class IceStormRaidwide(BossModule module) : Components.RaidwideCast(module, AID.IceStormRaidwide);
class DragonfistVisual(BossModule module) : Components.StandardAOEs(module, AID.DragonfistVisual, 16);
class DiamondDust(BossModule module) : Components.RaidwideCast(module, AID.DiamondDust);
class Fireball(BossModule module) : Components.StandardAOEs(module, AID.Fireball, 4);
class DeathSentence(BossModule module) : Components.SingleTargetCast(module, AID.DeathSentence);
class SpikedTail(BossModule module) : Components.SingleTargetCast(module, AID.SpikedTail);
class JudgmentBolt(BossModule module) : Components.RaidwideCast(module, AID.JudgmentBolt);
class EarthenFury(BossModule module) : Components.RaidwideCast(module, AID.EarthenFury);
class SuperCycloneKB(BossModule module) : Components.KnockbackFromCastTarget(module, AID.SuperCycloneKB, 5);
class AerialBlastKB(BossModule module) : Components.KnockbackFromCastTarget(module, AID.AerialBlastKB, 15);

class T03ShinryuStates : StateMachineBuilder
{
    public T03ShinryuStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<AkhMornVisual>()
            .ActivateOnEnter<Hellfire>()
            .ActivateOnEnter<MeteorImpact>()
            .ActivateOnEnter<MeteorImpact1>()
            .ActivateOnEnter<Protostar>()
            .ActivateOnEnter<DarkMatter>()
            .ActivateOnEnter<TailSlap1>()
            .ActivateOnEnter<TailSlap2>()
            .ActivateOnEnter<Spikesicle>()
            .ActivateOnEnter<BlazingTrail>()
            .ActivateOnEnter<EarthBreath1>()
            .ActivateOnEnter<IceStormRaidwide>()
            .ActivateOnEnter<DragonfistVisual>()
            .ActivateOnEnter<DiamondDust>()
            .ActivateOnEnter<Fireball>()
            .ActivateOnEnter<DeathSentence>()
            .ActivateOnEnter<SpikedTail>()
            .ActivateOnEnter<JudgmentBolt>()
            .ActivateOnEnter<EarthenFury>()
            .ActivateOnEnter<SuperCycloneKB>()
            .ActivateOnEnter<AerialBlastKB>();
    }
}

[ModuleInfo(Contributors = "Kagekazu", Incomplete = true, GroupType = BossModuleInfo.GroupType.CFC, GroupID = 239, NameID = 5640)]
public class T03Shinryu(WorldState ws, Actor primary) : BossModule(ws, primary, primary.Position, new ArenaBoundsCircle(20));
