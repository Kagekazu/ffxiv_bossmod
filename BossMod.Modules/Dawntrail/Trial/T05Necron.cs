namespace BossMod.Dawntrail.Trial.T05Necron;

public enum OID : uint
{
    Boss = 0x4870, // R20.000, Necron
    Helper = 0x233C, // R0.500
    IcyHands1 = 0x4903, // R3.575
    IcyHands2 = 0x4904, // R3.575
    LoomingSpecter1 = 0x4907, // R15.750
    IcyHands3 = 0x4908, // R4.400
    IcyHands4 = 0x4909, // R3.575
    AzureAether1 = 0x490A, // R1.000
    AzureAether2 = 0x4948, // R1.000
    NecronHelper = 0x4945, // R1.000
    AutoAttacker = 0x49B1, // R0.000, Part
    LoomingSpecter2 = 0x49DD, // R3.000
}

public enum AID : uint
{
    FearOfDeath = 44521, // Boss->self, 5.0s cast, range 100 circle
    FearOfDeathPuddle = 44522, // Helper->location, 3.0s cast, range 3 circle
    ChokingGrasp = 44523, // IcyHands->self, 3.0s cast, range 24 width 6 rect
    ColdGripLeft = 44524, // Boss->self, 5.0+1.0s cast, single-target visual
    ColdGripRight = 44525, // Boss->self, 5.0+1.0s cast, single-target visual
    ExistentialDread = 44526, // Helper->self, 1.0s cast, range 30 width 24 rect
    SoulReaping = 44527, // Boss->self, 6.0s cast, single-target visual
    Aetherblight = 44528, // Boss->self, 5.0s cast, single-target visual
    AetherblightRepeat1 = 44529, // Boss->self, no cast, single-target
    AetherblightRepeat2 = 44530, // Boss->self, no cast, single-target
    RelentlessReaping = 44531, // Boss->self, 15.0s cast, single-target visual
    MementoMori = 44532, // Boss->self, 5.0s cast, range 37 width 12 rect
    GrandCross = 44533, // Boss->location, 7.0s cast, range 50 circle
    GrandCrossLaser = 44534, // Helper->self, 0.3s cast, range 100 width 4 rect
    GrandCrossProximity = 44535, // Helper->self, 5.0s cast, range 100 width 100 rect
    GrandCrossPuddle = 44536, // Helper->location, 3.0s cast, range 3 circle
    NeutronRingCast = 44538, // Boss->location, 7.0s cast, single-target visual
    NeutronRing = 44539, // Helper->self, no cast, range 50 circle
    FearOfDeathPuddleAdds = 44540, // Helper->location, 3.0s cast, range 3 circle
    DarknessOfEternityCast = 44541, // Boss->self, 10.0s cast, single-target visual
    DarknessOfEternity = 44542, // Helper->self, no cast, range 50 circle
    Inevitability = 44544, // Helper->self, no cast, range 50 circle
    InvitationVisual = 44545, // LoomingSpecter1->self, 4.3+0.7s cast, range 36 width 10 rect
    BlueShockwaveCast = 44546, // Boss->self, 6.0+1.0s cast, single-target visual
    BlueShockwave = 44547, // Helper->self, no cast, range 100 100-degree cone
    MassMacabre = 44548, // Boss->self, 4.0s cast, single-target visual
    GrandCrossArena = 44603, // Helper->location, 7.0s cast, range 9-60 donut
    SpecterOfDeath = 44605, // Boss->self, 5.0s cast, single-target visual
    CropRotation = 44609, // Boss->self, 3.0s cast, single-target visual
    ColdGripAOE = 44611, // Helper->self, 6.0s cast, range 30 width 12 rect
    AutoAttack = 44614, // AutoAttacker->player, no cast, single-target
    Invitation = 44817, // Helper->self, 5.0s cast, range 36 width 10 rect
    SeasonsOfBlight = 45166, // Boss->self, 10.0s cast, single-target visual
    AetherblightCircle = 45181, // Helper->self, 1.0s cast, range 20 circle
    AetherblightDonut = 45182, // Helper->self, 1.0s cast, range 16-60 donut
}

public enum IconID : uint
{
    StoreCircle = 604, // Boss
    StoreDonut = 605, // Boss
    BlueShockwave = 615, // Boss
    Store621 = 621, // Boss
    Store622 = 622, // Boss
}

class FearOfDeath(BossModule module) : Components.RaidwideCast(module, AID.FearOfDeath);
class FearOfDeathPuddle(BossModule module) : Components.StandardAOEs(module, AID.FearOfDeathPuddle, 3);
class FearOfDeathPuddleAdds(BossModule module) : Components.StandardAOEs(module, AID.FearOfDeathPuddleAdds, 3);
class ChokingGrasp(BossModule module) : Components.StandardAOEs(module, AID.ChokingGrasp, new AOEShapeRect(24, 3));
class ExistentialDread(BossModule module) : Components.StandardAOEs(module, AID.ExistentialDread, new AOEShapeRect(30, 12));
class ColdGripAOE(BossModule module) : Components.StandardAOEs(module, AID.ColdGripAOE, new AOEShapeRect(30, 6));
class MementoMori(BossModule module) : Components.StandardAOEs(module, AID.MementoMori, new AOEShapeRect(37, 6));
class GrandCross(BossModule module) : Components.RaidwideCast(module, AID.GrandCross);
class GrandCrossLaser(BossModule module) : Components.StandardAOEs(module, AID.GrandCrossLaser, new AOEShapeRect(100, 2));
class GrandCrossProximity(BossModule module) : Components.StandardAOEs(module, AID.GrandCrossProximity, new AOEShapeRect(100, 4.5f));
class GrandCrossPuddle(BossModule module) : Components.StandardAOEs(module, AID.GrandCrossPuddle, 3);
class NeutronRing(BossModule module) : Components.RaidwideCastDelay(module, AID.NeutronRingCast, AID.NeutronRing, 2.6f);
class DarknessOfEternity(BossModule module) : Components.RaidwideCastDelay(module, AID.DarknessOfEternityCast, AID.DarknessOfEternity, 6.4f);
class Invitation(BossModule module) : Components.StandardAOEs(module, AID.Invitation, new AOEShapeRect(36, 5));
class AetherblightCircle(BossModule module) : Components.StandardAOEs(module, AID.AetherblightCircle, 20);
class AetherblightDonut(BossModule module) : Components.StandardAOEs(module, AID.AetherblightDonut, new AOEShapeDonut(16, 60));
class IcyHandsAdds(BossModule module) : Components.AddsMulti(module, [OID.IcyHands1, OID.IcyHands2, OID.IcyHands3, OID.IcyHands4], 1);

class BlueShockwave(BossModule module) : Components.TankSwap(module, default(AID), AID.BlueShockwave, AID.BlueShockwave, 4.1f, new AOEShapeCone(100, 50.Degrees()), false)
{
    public override void OnEventIcon(Actor actor, uint iconID, ulong targetID)
    {
        if (iconID == (uint)IconID.BlueShockwave)
        {
            _source = actor;
            _prevTarget = targetID;
            _activation = WorldState.FutureTime(7.2f);
        }
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        base.OnEventCast(caster, spell);
        if (NumCasts >= 2)
        {
            CurrentBaits.Clear();
            _source = null;
            _activation = default;
        }
    }
}

class GrandCrossArenaChange(BossModule module) : Components.GenericAOEs(module)
{
    private DateTime _activation;
    public int NumChanges;

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor)
    {
        if (_activation != default)
            yield return new(new AOEShapeDonut(9, 60), Arena.Center, default, _activation);
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID == AID.GrandCrossArena)
            _activation = Module.CastFinishAt(spell);
    }

    public override void OnEventDirectorUpdate(uint updateID, uint param1, uint param2, uint param3, uint param4)
    {
        if (updateID == 0x8000000D)
        {
            if (param1 == 2)
            {
                NumChanges++;
                _activation = default;
                Arena.Bounds = new ArenaBoundsCircle(9);
            }
            else if (param1 == 1)
            {
                NumChanges++;
                Arena.Bounds = new ArenaBoundsRect(18, 15);
            }
        }
    }
}

class T05NecronStates : StateMachineBuilder
{
    public T05NecronStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<FearOfDeath>()
            .ActivateOnEnter<FearOfDeathPuddle>()
            .ActivateOnEnter<FearOfDeathPuddleAdds>()
            .ActivateOnEnter<ChokingGrasp>()
            .ActivateOnEnter<ExistentialDread>()
            .ActivateOnEnter<ColdGripAOE>()
            .ActivateOnEnter<MementoMori>()
            .ActivateOnEnter<GrandCross>()
            .ActivateOnEnter<GrandCrossLaser>()
            .ActivateOnEnter<GrandCrossProximity>()
            .ActivateOnEnter<GrandCrossPuddle>()
            .ActivateOnEnter<GrandCrossArenaChange>()
            .ActivateOnEnter<NeutronRing>()
            .ActivateOnEnter<DarknessOfEternity>()
            .ActivateOnEnter<Invitation>()
            .ActivateOnEnter<AetherblightCircle>()
            .ActivateOnEnter<AetherblightDonut>()
            .ActivateOnEnter<BlueShockwave>()
            .ActivateOnEnter<IcyHandsAdds>();
    }
}

[ModuleInfo(Contributors = "Kagekazu", Incomplete = true, GroupType = BossModuleInfo.GroupType.CFC, GroupID = 1061, NameID = 14093)]
public class T05Necron(WorldState ws, Actor primary) : BossModule(ws, primary, new(100, 100), new ArenaBoundsRect(18, 15));
