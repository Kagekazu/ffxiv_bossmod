namespace BossMod.Stormblood.Trial.T02Lakshmi;

public enum OID : uint
{
    Boss = 0x1E20, // R3.500, x1
    Helper = 0x18D6, // R0.500, x16, 523 type : Looks like a helper instance pops up where voidzones are cast also.
    Lakshmi2 = 0x1D27, // R1.000, x10
    Lakshmi3 = 0x1E23, // R0.000, x1
    DreamingKshatriya = 0x1E22, // R1.000, x2
    Actor1e24 = 0x1E24, // R5.000, x1 (spawn during fight)
    Vril = 0x1E21, // R1.000, x12 (spawn during fight)
    VoidZone = 0x1EA76C // R0.5 voidzone aoes : Spawn during fight.
}

public enum AID : uint
{
    AutoAttack = 8535, // Boss->player, no cast, single-target
    AetherDrain = 9357, // Vril->player, no cast, single-target
    AlluringArm = 9352, // Boss->self, 7.0s cast, single-target
    AlluringEmbrace1 = 9358, // Lakshmi3->self, no cast, range 0 circle
    AlluringEmbrace2 = 9366, // Helper->self, no cast, range 100 circle

    BlissfulArrow1 = 9353, // Helper->player, no cast, single-target
    BlissfulArrow2 = 9354, // Helper->player, no cast, single-target

    BlissfulSpear1 = 9355, // Helper->self, no cast, range 40 width 8 cross
    BlissfulSpear2 = 9356, // Helper->self, no cast, range 40 width 8 cross
    BlissfulSpear3 = 9364, // Helper->player, no cast, range 7 circle
    BlissfulSpear4 = 9365, // Helper->player, no cast, range 7 circle

    Chanchala = 9348, // Boss->self, 3.0s cast, single-target
    DivineDenial = 9349, // Boss->self, 8.0s cast, range 40 circle
    HandOfGrace = 9350, // Boss->self, 7.0s cast, single-target
    HandOfBeauty = 9351, // Boss->self, 7.0s cast, single-target
    Jagadishwari = 9026, // Boss->self, no cast, single-target
    Stotram1 = 9347, // Boss->self, 3.0s cast, range 40 circle
    Stotram2 = 9374, // Boss->self, 3.0s cast, range 40 circle

    ThePallOfLight1 = 9360, // Boss->player, 5.0s cast, range 7 circle
    ThePallOfLightStack = 9361, // Boss->players, 5.0s cast, range 7 circle

    ThePullOfLightTB1 = 9362, // Boss->player, 5.0s cast, single-target
    ThePullOfLightTB2 = 9363, // Boss->player, 5.0s cast, single-target

    ThePathOfLightCleave = 9359, // Boss->self, no cast, range 40+R ?-degree cone
    ThePathOfLightProtean = 9377, // Boss->self, no cast, range 40+R ?-degree cone

    Unknown1 = 9305, // Lakshmi3->self, no cast, single-target
    Unknown2 = 9306, // Helper->self, no cast, single-target

    TailSlap = 9612, // Dreamer->self, no cast, range 6+R ?-degree cone
    InnerDemons = 9613 // Dreamer->self, 4.0s cast, range 6+R circle
}

public enum IconID : uint
{
    ProteanCleave = 14, // player : 45 degree cleave
    Stackmarker = 62, // player
    SpreadCross = 107, // player  : This baitaway is the cross shape
    SpreadCircle = 109, // player : This baitaway is the circle shape
    Tankbuster = 218, // player : 2
}

public enum SID : uint
{
    TargetRight = 1374,
    TargetLeft = 1375,
    Bleeding = 320,
    Seduced = 1389,
    Chanchala = 1410,
    Weakness = 43,
    Transcendent = 418,
    Vril = 1290
}

class DivineDenial(BossModule module) : Components.RaidwideCast(module, AID.DivineDenial);
class Stotram1(BossModule module) : Components.RaidwideCast(module, AID.Stotram1);
class Stotram2(BossModule module) : Components.RaidwideCast(module, AID.Stotram2);
class ThePallOfLight1(BossModule module) : Components.StandardAOEs(module, AID.ThePallOfLight1, 7);
class ThePallOfLightStack(BossModule module) : Components.StandardAOEs(module, AID.ThePallOfLightStack, 7);
class ThePullOfLightTB1(BossModule module) : Components.SingleTargetCast(module, AID.ThePullOfLightTB1);
class ThePullOfLightTB2(BossModule module) : Components.SingleTargetCast(module, AID.ThePullOfLightTB2);

class InnerDemons(BossModule module) : Components.StandardAOEs(module, AID.InnerDemons, 6);

class T02LakshmiStates : StateMachineBuilder
{
    public T02LakshmiStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<DivineDenial>()
            .ActivateOnEnter<Stotram1>()
            .ActivateOnEnter<Stotram2>()
            .ActivateOnEnter<ThePallOfLight1>()
            .ActivateOnEnter<ThePallOfLightStack>()
            .ActivateOnEnter<ThePullOfLightTB1>()
            .ActivateOnEnter<ThePullOfLightTB2>()
            .ActivateOnEnter<InnerDemons>();
    }
}

[ModuleInfo(Contributors = "Kagekazu", Incomplete = true, GroupType = BossModuleInfo.GroupType.CFC, GroupID = 263, NameID = 6385)]
public class T02Lakshmi(WorldState ws, Actor primary) : BossModule(ws, primary, primary.Position, new ArenaBoundsCircle(20));
