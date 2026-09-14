namespace BossMod.Shadowbringers.Trial.T04WarriorOfLight;

public enum OID : uint
{
    Boss = 0x2DDD, // R5.100, Warrior of Light P1
    Helper = 0x233C, // R0.500
    BossP2 = 0x2DDE, // R5.100, Warrior of Light P2
    SpectralNinja = 0x2DE3, // R1.350
    SpectralWhiteMage = 0x2DE4, // R1.350
    SpectralBlackMage = 0x2DE5, // R1.350
    SpectralDarkKnight = 0x2DE6, // R1.350
    SpectralBard = 0x2DE7, // R1.350
    SpectralWarrior = 0x2DE8, // R1.350
    SpectralSummoner = 0x2DE9, // R1.350
    WyrmOfLight = 0x2DEA, // R4.200
    SpectralEgi = 0x2DEB, // R1.800
}

public enum AID : uint
{
    AutoAttack = 872, // Boss/BossP2->player, no cast, single-target
    AbsoluteHoly = 20237, // Helper->self, no cast, range 6 circle
    AbsoluteBlizzardIIIHit = 20238, // Helper->self, no cast, range 70 circle
    AbsoluteFireIIIHit = 20239, // Helper->self, no cast, range 70 circle, Pyretic
    CoruscantSaberIn = 20240, // Boss->self, 7.0s cast, range 10 circle
    CoruscantSaberOut = 20241, // Boss->self, 7.0s cast, range 5-60 donut
    ImbuedAbsoluteFireIII = 20242, // Boss/BossP2->self, 3.0s cast, single-target
    ImbuedAbsoluteBlizzardIII = 20243, // Boss/BossP2->self, 3.0s cast, single-target
    RadiantBraver = 20246, // BossP2->self, no cast, range 60 90-degree cone
    RadiantBraverAOE = 20247, // Helper->self, no cast, range 60 90-degree cone
    RadiantDesperado = 20248, // BossP2->self, no cast, range 60 width 8 rect
    RadiantDesperadoRepeat = 20249, // BossP2->self, no cast, range 60 width 8 rect
    RadiantMeteor = 20250, // BossP2->self, 6.0s cast, single-target
    RadiantMeteorSpread = 20251, // Helper->player, 6.0s cast, range 20 circle
    SuitonSan = 20252, // Helper->self, 6.0s cast, range 60 width 60 rect
    BrimstoneEarth = 20254, // Helper->location, 8.0s cast, range 6 circle
    BrimstoneEarthRepeat = 20255, // Helper->location, no cast, range 6 circle
    DelugeOfDeath = 20256, // Helper->player, 5.0s cast, range 100 circle
    MeteorImpact = 20257, // Helper->self, no cast, range 4 circle
    Cauterize = 20261, // WyrmOfLight->self, 4.0s cast, range 40 width 20 rect
    Unknown20262 = 20262, // Helper->self, no cast
    TerrorUnleashed = 20263, // Boss->self, 3.0s cast, range 70 circle
    TheBitterEnd = 20264, // BossP2->player, 5.0s cast, single-target tankbuster
    ElddragonDive = 20265, // BossP2->self, 5.0s cast, range 70 circle
    SolemnConfiteor = 20266, // Helper->location, 3.0s cast, range 6 circle
    AbsoluteHolyCast = 20267, // BossP2->self, 5.0s cast, single-target
    AbsoluteBlizzardIII = 20269, // Boss->self, 5.0s cast, single-target
    AbsoluteFireIII = 20270, // Boss/BossP2->self, 5.0s cast, single-target
    ToTheLimit1 = 20276, // BossP2->self, 3.0s cast, single-target
    ToTheLimit2 = 20277, // BossP2->self, 3.0s cast, single-target
    ToTheLimit3 = 20278, // BossP2->self, 3.0s cast, single-target
    SpecterOfLight = 20279, // BossP2->self, 3.0s cast, single-target
    SuitonSanVisual = 20280, // SpectralNinja->self, 3.0s cast, single-target
    KatonSan = 20281, // SpectralNinja->self, 3.0s cast, single-target
    BrimstoneEarthVisual = 20282, // SpectralDarkKnight->self, 8.0s cast, single-target
    DelugeOfDeathVisual = 20283, // SpectralBard->self, 3.0s cast, single-target
    TwincastWHM = 20284, // SpectralWhiteMage->self, 3.0s cast, single-target
    TwincastBLM = 20285, // SpectralBlackMage->self, 3.0s cast, single-target
    SummonWyrm = 20289, // Boss/BossP2->self, 3.0s cast, single-target
    SwordOfLight = 20290, // Boss/BossP2->self, 3.0s cast, single-target
    SolemnConfiteorVisual = 20291, // Boss->self, 3.0s cast, single-target
    Unknown20293 = 20293, // Boss/BossP2->self, no cast
    Unknown20294 = 20294, // Helper->self, no cast
    RadiantSacrament = 20296, // Helper->self, 6.0s cast, range 60 width 40 rect
    ImbuedCoruscanceIn = 20299, // Boss->self, 7.0s cast, range 10 circle
    ImbuedCoruscanceOut = 20300, // BossP2->self, 7.0s cast, range 5-60 donut
    Unknown20593 = 20593, // Boss->self, no cast
    Unknown20611 = 20611, // Helper->self, no cast
    RadiantDesperadoCast = 20829, // BossP2->self, 6.0s cast, single-target
    RadiantBraverCast = 21076, // BossP2->self, 6.0s cast, single-target
    TwincastHit = 21278, // SpectralWhiteMage/BlackMage->self, no cast
    Ascendance = 21297, // Boss->self, 6.0s cast, range 60 circle
    AbsoluteTeleport = 21298, // Boss->self, 5.0s cast, single-target
    Unknown21379 = 21379, // BossP2->self, no cast
    UltimateCrossover = 21627, // BossP2->self, 7.0s cast, single-target
    UltimateCrossoverAOE = 21628, // Helper->self, 6.0s cast, range 60 circle
}

public enum SID : uint
{
    Pyretic = 960, // Helper->player
    ImbuedSaber = 2377, // Boss->Boss
}

public enum IconID : uint
{
    Stack = 161, // player
}

class CoruscantSaberIn(BossModule module) : Components.StandardAOEs(module, AID.CoruscantSaberIn, 10);
class CoruscantSaberOut(BossModule module) : Components.StandardAOEs(module, AID.CoruscantSaberOut, new AOEShapeDonut(5, 60));
class ImbuedCoruscanceIn(BossModule module) : Components.StandardAOEs(module, AID.ImbuedCoruscanceIn, 10);
class ImbuedCoruscanceOut(BossModule module) : Components.StandardAOEs(module, AID.ImbuedCoruscanceOut, new AOEShapeDonut(5, 60));
class RadiantMeteorSpread(BossModule module) : Components.SpreadFromCastTargets(module, AID.RadiantMeteorSpread, 20);
class SuitonSan(BossModule module) : Components.StandardAOEs(module, AID.SuitonSan, new AOEShapeRect(60, 30));
class BrimstoneEarth(BossModule module) : Components.StandardAOEs(module, AID.BrimstoneEarth, 6);
class Cauterize(BossModule module) : Components.StandardAOEs(module, AID.Cauterize, new AOEShapeRect(40, 10));
class TerrorUnleashed(BossModule module) : Components.RaidwideCast(module, AID.TerrorUnleashed, "Heal to full");
class TheBitterEnd(BossModule module) : Components.SingleTargetCast(module, AID.TheBitterEnd);
class ElddragonDive(BossModule module) : Components.RaidwideCast(module, AID.ElddragonDive);
class SolemnConfiteor(BossModule module) : Components.StandardAOEs(module, AID.SolemnConfiteor, 6);
class RadiantSacrament(BossModule module) : Components.StandardAOEs(module, AID.RadiantSacrament, new AOEShapeRect(60, 20));
class Ascendance(BossModule module) : Components.RaidwideCast(module, AID.Ascendance);
class UltimateCrossoverAOE(BossModule module) : Components.RaidwideCast(module, AID.UltimateCrossoverAOE);

class AbsoluteHoly(BossModule module) : Components.StackWithIcon(module, (uint)IconID.Stack, AID.AbsoluteHoly, 6, 5.1f);

class AbsoluteFireIce(BossModule module) : Components.StayMove(module)
{
    private Requirement _imbued;

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        switch ((AID)spell.Action.ID)
        {
            case AID.AbsoluteFireIII:
                Apply(Requirement.Stay, Module.CastFinishAt(spell));
                break;
            case AID.AbsoluteBlizzardIII:
                Apply(Requirement.NoMove, Module.CastFinishAt(spell));
                break;
            case AID.ImbuedAbsoluteFireIII:
                _imbued = Requirement.Stay;
                break;
            case AID.ImbuedAbsoluteBlizzardIII:
                _imbued = Requirement.NoMove;
                break;
            case AID.ImbuedCoruscanceIn:
            case AID.ImbuedCoruscanceOut:
                if (_imbued != Requirement.None)
                    Apply(_imbued, Module.CastFinishAt(spell));
                break;
        }
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if ((AID)spell.Action.ID is AID.AbsoluteFireIIIHit or AID.AbsoluteBlizzardIIIHit)
        {
            Array.Fill(PlayerStates, default);
            _imbued = Requirement.None;
        }
    }

    public override void OnStatusGain(Actor actor, in ActorStatus status)
    {
        if ((SID)status.ID == SID.Pyretic)
            SetState(Raid.FindSlot(actor.InstanceID), new(Requirement.Stay, status.ExpireAt, 1));
    }

    public override void OnStatusLose(Actor actor, in ActorStatus status)
    {
        if ((SID)status.ID == SID.Pyretic)
            ClearState(Raid.FindSlot(actor.InstanceID), 1);
    }

    private void Apply(Requirement req, DateTime act)
    {
        foreach (var (i, _) in Raid.WithSlot(true))
            SetState(i, new(req, act));
    }
}

class SpectralAdds(BossModule module) : Components.AddsMulti(module, [(uint)OID.SpectralNinja, (uint)OID.SpectralWhiteMage, (uint)OID.SpectralBlackMage, (uint)OID.SpectralDarkKnight, (uint)OID.SpectralBard, (uint)OID.SpectralWarrior, (uint)OID.SpectralSummoner, (uint)OID.SpectralEgi], 1);

class T04WarriorOfLightStates : StateMachineBuilder
{
    private readonly T04WarriorOfLight _module;

    public T04WarriorOfLightStates(T04WarriorOfLight module) : base(module)
    {
        _module = module;
        SimplePhase(0, Phase1, "P1")
            .Raw.Update = () => _module.BossP2() != null || Module.PrimaryActor.IsDeadOrDestroyed;
        SimplePhase(1, Phase2, "P2")
            .Raw.Update = () => Module.PrimaryActor.IsDeadOrDestroyed && (_module.BossP2()?.IsDead ?? false);
    }

    private void Shared(State s) => s
        .ActivateOnEnter<CoruscantSaberIn>()
        .ActivateOnEnter<CoruscantSaberOut>()
        .ActivateOnEnter<ImbuedCoruscanceIn>()
        .ActivateOnEnter<ImbuedCoruscanceOut>()
        .ActivateOnEnter<SuitonSan>()
        .ActivateOnEnter<BrimstoneEarth>()
        .ActivateOnEnter<Cauterize>()
        .ActivateOnEnter<TerrorUnleashed>()
        .ActivateOnEnter<SolemnConfiteor>()
        .ActivateOnEnter<RadiantSacrament>()
        .ActivateOnEnter<AbsoluteHoly>()
        .ActivateOnEnter<AbsoluteFireIce>()
        .ActivateOnEnter<SpectralAdds>()
        .ActivateOnEnter<Ascendance>();

    private void Phase1(uint id)
    {
        Shared(SimpleState(id, 10000, "P2"));
    }

    private void Phase2(uint id)
    {
        Shared(SimpleState(id, 10000, "Enrage")
            .ActivateOnEnter<RadiantMeteorSpread>()
            .ActivateOnEnter<TheBitterEnd>()
            .ActivateOnEnter<ElddragonDive>()
            .ActivateOnEnter<UltimateCrossoverAOE>());
    }
}

[ModuleInfo(Contributors = "Kagekazu", Incomplete = true, GroupType = BossModuleInfo.GroupType.CFC, GroupID = 738, NameID = 9462)]
public class T04WarriorOfLight(WorldState ws, Actor primary) : BossModule(ws, primary, new(100, 100), new ArenaBoundsCircle(20))
{
    public Actor? BossP2() => Enemies(OID.BossP2).FirstOrDefault(a => !a.IsDestroyed);

    protected override void DrawEnemies(int pcSlot, Actor pc)
    {
        Arena.Actor(PrimaryActor, ArenaColor.Enemy);
        Arena.Actor(BossP2(), ArenaColor.Enemy);
    }
}
