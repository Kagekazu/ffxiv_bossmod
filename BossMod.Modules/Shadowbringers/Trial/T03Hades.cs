namespace BossMod.Shadowbringers.Trial.T03Hades;

public enum OID : uint
{
    Hades = 0x2AA2, // R0.000, x?
    HadesBig = 0x294A, // R20.000, x? -> Big
    Hades2 = 0x294C, // R0.000, x?
    Hades3 = 0x233C, // R0.500, x?, Helper type
    Boss = 0x2949, // R6.750, x?
    Actor1ea1a1 = 0x1EA1A1, // R2.000, x?, EventObj type
    Hades5 = 0x29DB, // R1.000, x?, Part type
    Hades6 = 0x29D9, // R1.000, x?, Part type
    Hades7 = 0x29DA, // R1.000, x?, Part type
    Hades8 = 0x29D8, // R1.000, x?, Part type
    BrokenFaithObject = 0x1EAD28, // R0.500, x?, EventObj type
    ShadowOfTheAncients = 0x27AE, // R1.250, x?
    ShadowOfTheAncients1 = 0x27B7, // R1.250, x?
    AetherialGaol = 0x294B, // R8.000, x?
    DoomTower = 0x1EAD29, // R0.500, x?, EventObj type
}

public enum AID : uint
{
    _AutoAttack = 872, // 2949->player, no cast, single-target
    RavenousAssault = 16728, // 2949->player, 5.0s cast, range 5 circle
    BadFaith = 16715, // 233C->self, 5.0s cast, range 20 width 20 rect
    BadFaith1 = 16716, // 233C->self, 5.0s cast, range 20 width 20 rect
    BadFaithVisual = 16713, // 2949->self, 5.0s cast, single-target
    BadFaithVisual1 = 16714, // 2949->self, 5.0s cast, single-target
    Double = 16719, // 2949->self, 3.0s cast, single-target
    DarkEruptionVisual = 16720, // 2949->self, 3.0s cast, single-target
    DarkEruption = 16723, // 233C->location, no cast, range 6 circle
    DarkEruptionAOE = 16722, // 233C->location, 3.0s cast, range 6 circle
    DarkEruption3 = 16721, // 2949->self, no cast, single-target
    BrokenFaith = 16717, // 2949->self, 3.0s cast, single-target
    BrokenFaith1 = 16718, // 233C->self, no cast, range 10 circle
    ShadowSpread = 16726, // 233C->self, 3.0s cast, range 40 30.000-degree cone
    ShadowSpread1 = 16724, // 2949->self, 3.0s cast, single-target
    ShadowSpread2 = 16725, // 2949->self, no cast, single-target
    ShadowSpread3 = 16727, // 233C->self, 3.0s cast, range 40 30.000-degree cone
    _Spell_ = 17817, // 2949->self, no cast, single-target
    _Spell_1 = 17816, // 233C->self, no cast, single-target
    AncientRuin = 17814, // 27AE->player, no cast, single-target
    AncientWaterIII = 17812, // 27B7->players, 5.0s cast, range 6 circle
    AncientDarkness = 17811, // 27B7->player, 5.0s cast, range 5 circle
    AncientAero = 17813, // 27B7->self, 5.0s cast, range 40 width 8 rect
    AncientDarkIV = 17815, // 2949->self, 5.0s cast, range 100 circle
    _AutoAttack_ = 16764, // 294A->player, no cast, single-target
    Titanomachy = 16768, // 294A->self, 4.0s cast, range 100 circle
    ShadowStream = 16732, // 294A->self, 5.0s cast, range 100 width 16 rect
    DualStrike = 16738, // 233C->player, 5.0s cast, range 5 circle
    DualStrike1 = 16737, // 294A->self, 5.0s cast, single-target
    EchoOfTheLost = 16740, // 294A->self, 7.0s cast, range 100 ?-degree cone
    WailOfTheLost = 16741, // 294A->self, 5.0s cast, range 40 width 40 rect
    PolydegmonsPurgation = 16754, // 233C->self, 5.0s cast, range 100 width 16 rect
    PolydegmonsPurgation1 = 16753, // 233C->self, 5.0s cast, range 100 width 16 rect
    PolydegmonsPurgation2 = 16752, // 294A->self, 5.0s cast, single-target
    HellbornYawp = 16750, // 294A->self, 5.0s cast, single-target
    HellbornYawp1 = 16751, // 233C->self, 4.0s cast, range 100 60.000-degree cone
    EchoOfTheLost1 = 16739, // 294A->self, 7.0s cast, range 100 ?-degree cone
    Captivity = 16744, // 294A->self, 5.0s cast, single-target
    CaptivityCast = 16745, // 233C->player, no cast, range 8 circle
    __ = 16767, // 233C->self, no cast, single-target
    _Spell_2 = 16746, // 233C->player, no cast, single-target
    _Spell_3 = 16747, // 294A->self, no cast, single-target
    _Spell_4 = 16749, // 294A->self, no cast, single-target
    NetherBlast = 16755, // 29D8/29D9/29DA/29DB->player, no cast, range 6 circle
    LifeInCaptivity = 16757, // 294A->self, 4.0s cast, range 100 circle
    _Spell_5 = 17452, // 233C->player, no cast, single-target
    BlackCauldron = 16758, // 294A->self, no cast, single-target
    BlackCauldron1 = 16730, // 233C->self, no cast, range 100 circle
    TheDarkDevours = 16759, // 294A->self, 3.0s cast, single-target
    TheDarkDevours1 = 16761, // 233C->self, no cast, range 100 circle
    TheDarkDevours2 = 16762, // 233C->self, no cast, range 100 circle
    ChorusOfTheLost = 16748, // 294A->self, 30.0s cast, range 100 circle
}

public enum IconID : uint
{
    Tankbuster = 343, //Player -> self : vfx tank_lockonae_5m_5s_01k1
    SpreadIcon = 139, //Player -> self : vfx target_ae_s5f
    _Gen3 = 62, //Player -> self : vfx com_share0c
    _Gen4 = 96, //Player -> self : vfx loc05sp_05af
    _Gen5 = 40, // Player -> self : vfx m0117_earth_shake_01s
    CaptivityBait = 120, //Player - Self : vfx loc08sp_05at
}

public enum SID : uint
{
    Doom = 210,
    VulnerabilityUp = 202,
    _Gen_ = 2056,
    Double = 661,
    Stun = 149,
    Fetters = 770,
    Fetters1 = 1614,
    LightBeyondDarkness = 1929,
    LightInTheDark = 1904,
    Bleeding = 320,
}

public enum TetherID : uint
{
    DottedLineTether = 17
}

class RavenousAssault(BossModule module) : Components.StandardAOEs(module, AID.RavenousAssault, 5);
class ShadowSpread(BossModule module) : Components.StandardAOEs(module, AID.ShadowSpread, new AOEShapeCone(40, 15.Degrees()));
class ShadowSpread3(BossModule module) : Components.StandardAOEs(module, AID.ShadowSpread3, new AOEShapeCone(40, 15.Degrees()));
class HellbornYawp1(BossModule module) : Components.StandardAOEs(module, AID.HellbornYawp1, new AOEShapeCone(100, 30.Degrees()));
class BadFaith(BossModule module) : Components.StandardAOEs(module, AID.BadFaith, new AOEShapeRect(20, 10));
class BadFaith1(BossModule module) : Components.StandardAOEs(module, AID.BadFaith1, new AOEShapeRect(20, 10));
class DarkEruptionAOE(BossModule module) : Components.StandardAOEs(module, AID.DarkEruptionAOE, 6);
class AncientWaterIII(BossModule module) : Components.StandardAOEs(module, AID.AncientWaterIII, 6);
class AncientDarkness(BossModule module) : Components.StandardAOEs(module, AID.AncientDarkness, 5);
class AncientAero(BossModule module) : Components.StandardAOEs(module, AID.AncientAero, new AOEShapeRect(40, 4));
class AncientDarkIV(BossModule module) : Components.RaidwideCast(module, AID.AncientDarkIV);
class Titanomachy(BossModule module) : Components.RaidwideCast(module, AID.Titanomachy);
class ShadowStream(BossModule module) : Components.StandardAOEs(module, AID.ShadowStream, new AOEShapeRect(100, 8));
class DualStrike(BossModule module) : Components.StandardAOEs(module, AID.DualStrike, 5);
class WailOfTheLost(BossModule module) : Components.StandardAOEs(module, AID.WailOfTheLost, new AOEShapeRect(40, 20));
class PolydegmonsPurgation(BossModule module) : Components.StandardAOEs(module, AID.PolydegmonsPurgation, new AOEShapeRect(100, 8));
class PolydegmonsPurgation1(BossModule module) : Components.StandardAOEs(module, AID.PolydegmonsPurgation1, new AOEShapeRect(100, 8));
class LifeInCaptivity(BossModule module) : Components.RaidwideCast(module, AID.LifeInCaptivity);
class ChorusOfTheLost(BossModule module) : Components.RaidwideCast(module, AID.ChorusOfTheLost);

class T03HadesStates : StateMachineBuilder
{
    public T03HadesStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<RavenousAssault>()
            .ActivateOnEnter<ShadowSpread>()
            .ActivateOnEnter<ShadowSpread3>()
            .ActivateOnEnter<HellbornYawp1>()
            .ActivateOnEnter<BadFaith>()
            .ActivateOnEnter<BadFaith1>()
            .ActivateOnEnter<DarkEruptionAOE>()
            .ActivateOnEnter<AncientWaterIII>()
            .ActivateOnEnter<AncientDarkness>()
            .ActivateOnEnter<AncientAero>()
            .ActivateOnEnter<AncientDarkIV>()
            .ActivateOnEnter<Titanomachy>()
            .ActivateOnEnter<ShadowStream>()
            .ActivateOnEnter<DualStrike>()
            .ActivateOnEnter<WailOfTheLost>()
            .ActivateOnEnter<PolydegmonsPurgation>()
            .ActivateOnEnter<PolydegmonsPurgation1>()
            .ActivateOnEnter<LifeInCaptivity>()
            .ActivateOnEnter<ChorusOfTheLost>();
    }
}

[ModuleInfo(Contributors = "Kagekazu", Incomplete = true, GroupType = BossModuleInfo.GroupType.CFC, GroupID = 687, NameID = 8352)]
public class T03Hades(WorldState ws, Actor primary) : BossModule(ws, primary, new(100, 100), new ArenaBoundsCircle(20));
