namespace BossMod.Shadowbringers.Trial.T02Innocence;

public enum OID : uint
{
    Boss = 0x28FB, // R2.600, Innocence P1
    Helper = 0x233C, // R0.500
    ForgivenShame = 0x28FC, // R0.960
    ForgivenVenery = 0x28FD, // R1.500
    BossP2 = 0x28FE, // R4.400, Innocence P2
    NailOfCondemnation = 0x2900, // R0.500
    ThornOfCondemnation = 0x2901, // R1.000
    SwordOfCondemnation = 0x2902, // R0.000
    InnocenceAdd = 0x2BED, // R2.800
    ForgivenShame2 = 0x2BEE, // R0.960
    ForgivenVenery2 = 0x2BEF, // R1.500
}

public enum AID : uint
{
    AutoAttack = 870, // ForgivenShame/Venery->player, no cast, single-target
    Unknown14588 = 14588, // Helper->self, no cast
    AutoAttackBoss = 16016, // Boss->player, no cast, single-target
    Unknown16017 = 16017, // Boss->self, no cast
    Unknown16018 = 16018, // Boss->self, no cast
    ExaltedWing = 16019, // Helper->self, no cast, range 40 circle
    Unknown16020 = 16020, // Boss->self, no cast
    HeavenlyHost = 16021, // Boss->self, 3.0s cast, single-target
    GuidingLight = 16022, // Boss->self, 3.0s cast, single-target
    Sinsphere = 16023, // Helper->self, no cast, range 5 circle
    Sinburst = 16024, // Helper->self, no cast, range 40 circle
    Enthrall = 16025, // Boss->self, 4.0s cast, range 40 circle
    Realmrazer = 16026, // Boss->self, 5.0s cast, single-target
    RealmrazerAOE = 16027, // Helper->self, no cast, range 40 circle
    Daybreak = 16028, // Boss->self, 3.5s cast, single-target
    DaybreakAOE = 16029, // Helper->location, 3.5s cast, range 6 circle
    ScoldsBridle = 16030, // ForgivenShame->self, 6.0s cast, range 40 circle
    HolySword = 16031, // ForgivenVenery->player, 5.0s cast, single-target tankbuster
    AutoAttackP2 = 16032, // BossP2->player, no cast, single-target
    Unknown16033 = 16033, // BossP2->self, no cast
    Unknown16034 = 16034, // BossP2->self, no cast
    RighteousBolt = 16035, // BossP2->player, 5.0s cast, single-target tankbuster
    NailExplosion = 16041, // NailOfCondemnation->self, no cast
    SoulAndBody1 = 16049, // Helper->self, 3.0s cast, range 5-20 donut
    SoulAndBody2 = 16050, // Helper->self, 3.0s cast, range 5-20 donut
    HolyTrinity = 16051, // Helper->self, 3.0s cast, range 40 width 4 rect
    RightfulReprobation = 16053, // BossP2->self, 3.0s cast, single-target
    Reprobation = 16054, // ThornOfCondemnation->self, 3.0s cast, single-target
    ReprobationShort = 16055, // ThornOfCondemnation->self, 1.5s cast, single-target
    ReprobationLine = 16056, // Helper->self, 3.0s cast, range 21 width 4 rect
    GodRay = 16060, // BossP2->self, 4.5s cast, single-target
    GodRayRepeat = 16061, // BossP2->self, no cast
    GodRayCone = 16062, // Helper->self, 4.5s cast, range 5 100-degree cone
    GodRayDonut1 = 16063, // Helper->self, 3.5s cast, range 5-10 donut
    GodRayDonut2 = 16064, // Helper->self, 3.5s cast, range 10-20 donut
    FlamingSword = 16065, // SwordOfCondemnation->self, no cast, range 40 circle
    DropOfLightVisual = 16068, // BossP2->self, no cast, single-target
    DropOfLight = 16069, // Helper->self, no cast, range 10 circle
    LightPillar = 16070, // BossP2->self, no cast, range 40 width 6 rect
    BeatificVision = 16071, // BossP2->self, 5.0s cast, range 45 width 40 rect
    ReprobationLong = 16075, // Helper->self, 1.5s cast, range 42 width 4 rect
    Shadowreaver = 16106, // BossP2->self, 5.0s cast, range 40 circle
    ExaltedPlumes = 16114, // Helper->self, no cast, range 40 circle
    Unknown16121 = 16121, // Helper->self, no cast
    Unknown16122 = 16122, // Helper->self, no cast
    LightPillarCast = 16190, // BossP2->self, 5.0s cast, single-target
    Unknown16197 = 16197, // Helper->self, no cast
    WingedReprobation = 16572, // BossP2->self, 3.0s cast, single-target
    Unknown16708 = 16708, // Boss->self, no cast
    Unknown17072 = 17072, // BossP2->self, no cast
    Unknown17073 = 17073, // SwordOfCondemnation->self, no cast
    HolySwordInstant = 17175, // ForgivenVenery->player, no cast, single-target
    Manacle = 18064, // ForgivenShame2->location, 3.5s cast, range 6 circle
    HolySwordAdd = 18065, // ForgivenVenery2->ForgivenShame2, 9.0s cast, single-target
    GuiltyVerdict = 18066, // ForgivenVenery2->self, no cast, range 50 circle
    Unknown18184 = 18184, // Helper->self, no cast
}

public enum IconID : uint
{
    Stack = 138,
    Spread = 218,
}

public enum TetherID : uint
{
    HolySword = 88, // ForgivenVenery2->ForgivenShame2
}

class Enthrall(BossModule module) : Components.CastGaze(module, AID.Enthrall);
class Realmrazer(BossModule module) : Components.RaidwideCastDelay(module, AID.Realmrazer, AID.RealmrazerAOE, 0.5f);
class DaybreakAOE(BossModule module) : Components.StandardAOEs(module, AID.DaybreakAOE, 6);
class ScoldsBridle(BossModule module) : Components.RaidwideCast(module, AID.ScoldsBridle);
class HolySword(BossModule module) : Components.SingleTargetCast(module, AID.HolySword);
class RighteousBolt(BossModule module) : Components.SingleTargetCast(module, AID.RighteousBolt);
class SoulAndBody1(BossModule module) : Components.StandardAOEs(module, AID.SoulAndBody1, new AOEShapeDonut(5, 20));
class SoulAndBody2(BossModule module) : Components.StandardAOEs(module, AID.SoulAndBody2, new AOEShapeDonut(5, 20));
class HolyTrinity(BossModule module) : Components.StandardAOEs(module, AID.HolyTrinity, new AOEShapeRect(40, 2));
class ReprobationLine(BossModule module) : Components.StandardAOEs(module, AID.ReprobationLine, new AOEShapeRect(21, 2));
class ReprobationLong(BossModule module) : Components.StandardAOEs(module, AID.ReprobationLong, new AOEShapeRect(42, 2));
class GodRayCone(BossModule module) : Components.StandardAOEs(module, AID.GodRayCone, new AOEShapeCone(5, 50.Degrees()));
class GodRayDonut1(BossModule module) : Components.StandardAOEs(module, AID.GodRayDonut1, new AOEShapeDonutSector(5, 10, 50.Degrees()));
class GodRayDonut2(BossModule module) : Components.StandardAOEs(module, AID.GodRayDonut2, new AOEShapeDonutSector(10, 20, 50.Degrees()));
class BeatificVision(BossModule module) : Components.StandardAOEs(module, AID.BeatificVision, new AOEShapeRect(45, 20));
class Shadowreaver(BossModule module) : Components.RaidwideCast(module, AID.Shadowreaver);
class Manacle(BossModule module) : Components.StandardAOEs(module, AID.Manacle, 6);
class HolySwordAdd(BossModule module) : Components.SingleTargetCast(module, AID.HolySwordAdd, "Interrupt add");
class InnocenceAdds(BossModule module) : Components.AddsMulti(module, [(uint)OID.ForgivenShame, (uint)OID.ForgivenVenery, (uint)OID.ForgivenShame2, (uint)OID.ForgivenVenery2], 1);

class T02InnocenceStates : StateMachineBuilder
{
    private readonly T02Innocence _module;

    public T02InnocenceStates(T02Innocence module) : base(module)
    {
        _module = module;
        SimplePhase(0, Phase1, "P1")
            .Raw.Update = () => _module.BossP2() != null || Module.PrimaryActor.IsDeadOrDestroyed;
        SimplePhase(1, Phase2, "P2")
            .Raw.Update = () => Module.PrimaryActor.IsDeadOrDestroyed && (_module.BossP2()?.IsDead ?? false);
    }

    private void Phase1(uint id)
    {
        SimpleState(id, 10000, "P2")
            .ActivateOnEnter<Enthrall>()
            .ActivateOnEnter<Realmrazer>()
            .ActivateOnEnter<DaybreakAOE>()
            .ActivateOnEnter<ScoldsBridle>()
            .ActivateOnEnter<HolySword>()
            .ActivateOnEnter<InnocenceAdds>();
    }

    private void Phase2(uint id)
    {
        SimpleState(id, 10000, "Enrage")
            .ActivateOnEnter<RighteousBolt>()
            .ActivateOnEnter<SoulAndBody1>()
            .ActivateOnEnter<SoulAndBody2>()
            .ActivateOnEnter<HolyTrinity>()
            .ActivateOnEnter<ReprobationLine>()
            .ActivateOnEnter<ReprobationLong>()
            .ActivateOnEnter<GodRayCone>()
            .ActivateOnEnter<GodRayDonut1>()
            .ActivateOnEnter<GodRayDonut2>()
            .ActivateOnEnter<BeatificVision>()
            .ActivateOnEnter<Shadowreaver>()
            .ActivateOnEnter<Manacle>()
            .ActivateOnEnter<HolySwordAdd>()
            .ActivateOnEnter<InnocenceAdds>();
    }
}

[ModuleInfo(Contributors = "Kagekazu", Incomplete = true, GroupType = BossModuleInfo.GroupType.CFC, GroupID = 666, NameID = 8353)]
public class T02Innocence(WorldState ws, Actor primary) : BossModule(ws, primary, new(100, 100), new ArenaBoundsCircle(20))
{
    public Actor? BossP2() => Enemies(OID.BossP2).FirstOrDefault(a => !a.IsDestroyed);

    protected override void DrawEnemies(int pcSlot, Actor pc)
    {
        Arena.Actor(PrimaryActor, ArenaColor.Enemy);
        Arena.Actor(BossP2(), ArenaColor.Enemy);
    }
}
