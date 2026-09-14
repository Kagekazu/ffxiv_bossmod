namespace BossMod.Heavensward.Trial.T03Thordan;

public enum OID : uint
{
    Boss = 0x1073, // R3.800, King Thordan
    SerZephirin = 0x1074, // R2.200
    SerAdelphel = 0x1075, // R2.200
    SerJanlenoux = 0x1076, // R2.200
    SerVellguine = 0x1077, // R2.200
    SerPaulecrain = 0x1078, // R2.200
    SerIgnasse = 0x1079, // R2.200
    SerGrinnaux = 0x107A, // R2.200
    SerHermenost = 0x107B, // R2.200
    SerGuerrique = 0x107C, // R2.200
    SerCharibert = 0x107D, // R2.200
    SerHaumeric = 0x107E, // R2.200
    SerNoudenet = 0x107F, // R2.200
    Ascalon = 0x1080, // R3.800
    MeteorCircle = 0x1082, // R1.000
    CometCircle = 0x1083, // R1.000
    Helper = 0x1084, // R0.500
}

public enum AID : uint
{
    AutoAttackKnight = 870, // SerAdelphel/Janlenoux->player, no cast, single-target
    KnightHit4120 = 4120, // knights->player, no cast, single-target
    KnightHit4121 = 4121, // knights->player, no cast, single-target
    KnightHit4185 = 4185, // knights->self, no cast
    ThordanHit4186 = 4186, // Boss->self, no cast
    ThordanHit4187 = 4187, // Boss->self, no cast
    ThordanHit4190 = 4190, // Boss->self, no cast
    ThordanCast4200 = 4200, // Boss->self, 3.7s cast
    ThordanCast4201 = 4201, // Boss->self, 4.7s cast
    ThordanHit4202 = 4202, // Boss->self, no cast
    HelperHit4203 = 4203, // Helper->self, no cast
    ThordanHit4204 = 4204, // Boss->self, no cast
    ZephirinCast = 4205, // SerZephirin->self, 14.7s cast
    GrinnauxCast4212 = 4212, // SerGrinnaux->self, 5.2s cast
    GrinnauxCast4213 = 4213, // Helper->self, 5.7s cast
    HermenostCast4214 = 4214, // SerHermenost->self, 4.9s cast
    HermenostCast4215 = 4215, // Helper->self, 7.7s cast
    HermenostHit4216 = 4216, // Helper->self, no cast
    GuerriqueHit4217 = 4217, // SerGuerrique->self, no cast
    GuerriqueCast4218 = 4218, // Helper->self, 2.7s cast
    GuerriqueCast4219 = 4219, // Helper->self, 2.7s cast
    GuerriqueCast4220 = 4220, // Helper->self, 2.7s cast
    GuerriqueCast4221 = 4221, // Helper->self, 2.7s cast
    VellguineCast = 4222, // SerVellguine->self, 2.7s cast
    PaulecrainCast = 4223, // SerPaulecrain->self, 3.7s cast
    AscalonCast4225 = 4225, // Helper->self, 5.7s cast
    IgnasseHit = 4226, // SerIgnasse->self, no cast
    CharibertCast4227 = 4227, // SerCharibert->self, 2.2s cast
    CharibertCast4228 = 4228, // Helper->self, 2.7s cast
    CharibertCast4229 = 4229, // Helper->self, 2.7s cast
    CharibertCast4230 = 4230, // Helper->self, 2.7s cast
    CharibertCast4231 = 4231, // Helper->self, 2.7s cast
    CharibertHit4232 = 4232, // Helper->self, no cast
    HaumericCast = 4233, // SerHaumeric->self, 2.2s cast
    HaumericHit = 4234, // Helper->self, no cast
    NoudenetCast = 4235, // SerNoudenet->self, 2.7s cast
}

class Knights(BossModule module) : Components.AddsMulti(module, [
    OID.SerZephirin, OID.SerAdelphel, OID.SerJanlenoux, OID.SerVellguine, OID.SerPaulecrain, OID.SerIgnasse,
    OID.SerGrinnaux, OID.SerHermenost, OID.SerGuerrique, OID.SerCharibert, OID.SerHaumeric, OID.SerNoudenet,
    OID.MeteorCircle, OID.CometCircle
], 1);

class T03ThordanStates : StateMachineBuilder
{
    public T03ThordanStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<Knights>();
    }
}

[ModuleInfo(Contributors = "Kagekazu", Incomplete = true, GroupType = BossModuleInfo.GroupType.CFC, GroupID = 90, NameID = 3632)]
public class T03Thordan(WorldState ws, Actor primary) : BossModule(ws, primary, new(0, 0), new ArenaBoundsCircle(20));
