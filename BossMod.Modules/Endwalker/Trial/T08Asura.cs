namespace BossMod.Endwalker.Trial.T08Asura;

public enum OID : uint
{
    Boss = 0x409B, //R=5.0
    Helper = 0x233C,
    Helper2 = 0x40A9,
    PhantomAsura = 0x40F8, //R=5.0
    AsuraImage = 0x40A2, //R=45.0
}

public enum AID : uint
{
    AutoAttack = 870, // Boss->player, no cast, single-target
    LowerRealm = 36001, // Boss->self, 5.0s cast, range 40 circle, raidwide
    Teleport = 35966, // Boss->location, no cast, single-target, boss teleports mid
    AsuriChakra = 35994, // Boss->self, 6.0s cast, range 5 circle
    Chakra1 = 35995, // Helper->self, 6.0s cast, range 6-8 donut
    Chakra2 = 35996, // Helper->self, 6.0s cast, range 9-11 donut
    Chakra3 = 35997, // Helper->self, 6.0s cast, range 12-14 donut
    Chakra4 = 35998, // Helper->self, 6.0s cast, range 15-17 donut
    Chakra5 = 35999, // Helper->self, 6.0s cast, range 18-20 donut
    CuttingJewel = 36000, // Boss->players, 5.0s cast, range 4 circle, tankbuster
    Ephemerality = 35990, // Boss->self, 5.0s cast, range 20 circle, raidwide
    Laceration = 35991, // PhantomAsura->self, 1.0s cast, range 9 circle
    Divinity = 36008, // Boss->self, no cast, single-target
    Divinity2 = 36009, // AsuraImage->self, no cast, single-target
    DivineAwakening = 35967, // Boss->self, no cast, single-target
    DivineAwakening2 = 35968, // AsuraImage->self, no cast, single-target
    IconographyPedestalPurge = 35969, // Boss->self, 5.0s cast, range 10 circle
    IconicExecution = 36017, // Boss->self, 4.0s cast, single-target
    IconicExecution2 = 36018, // Boss->self, 3.0s cast, single-target
    PedestalPurge = 35970, // AsuraImage->self, 4.0s cast, range 27 circle (lumina data says range 60, which is obvious bs)
    IconographyWheelOfDeincarnation = 35971, // Boss->self, 5.0s cast, range 8-40 donut
    WheelOfDeincarnation = 35972, // AsuraImage->self, 4.0s cast, range 15-96 donut
    IconographyBladewise = 35973, // Boss->self, 5.0s cast, range 50 width 6 rect
    Bladewise = 35974, // AsuraImage->self, 4.0s cast, range 100 width 28 rect
    RemoveStatus = 36010, // AsuraImage->self, no cast, single-target, removes status 2552 (unknown effect) from itself
    KhadgaTelegraph1 = 36011, // Helper->self, 2.0s cast, range 20 180-degree cone
    KhadgaTelegraph2 = 36013, // Helper->self, 2.0s cast, range 20 180-degree cone
    KhadgaTelegraph3 = 36012, // Helper->self, 2.0s cast, range 20 180-degree cone
    SixBladedKhadga = 35976, // Boss->self, 13.0s cast, single-target
    Khadga1 = 35977, // Boss->self, no cast, range 20 180-degree cone
    Khadga2 = 35981, // Boss->self, no cast, range 20 180-degree cone
    Khadga3 = 35980, // Boss->self, no cast, range 20 180-degree cone
    Khadga4 = 35978, // Boss->self, no cast, range 20 180-degree cone
    Khadga5 = 35982, // Boss->self, no cast, range 20 180-degree cone
    Khadga6 = 35979, // Boss->self, no cast, range 20 180-degree cone
    ManyFaces1 = 35983, // Boss->self, 4.0s cast, single-target
    ManyFaces2 = 36014, // Boss->self, no cast, single-target
    TheFaceOfWrathA = 35984, // Boss->self, 8.0s cast, single-target
    TheFaceOfWrathB = 35986, // Boss->self, 8.0s cast, single-target
    TheFaceOfWrath = 36022, // Helper->self, no cast, single-target
    FaceMechanicWrath = 36015, // Helper->self, 8.0s cast, range 20 180-degree cone
    FaceMechanicDelight = 36016, // Helper->self, 8.0s cast, range 20 180-degree cone
    TheFaceOfDelightA = 35989, // Boss->self, 8.0s cast, single-target
    TheFaceOfDelightB = 35987, // Boss->self, 8.0s cast, single-target
    TheFaceOfDelight = 36023, // Helper->self, no cast, single-target
    TheFaceOfDelightSnapshot = 36007, // Helper->self, no cast, range 20 180-degree cone
    TheFaceOfWrathSnapshot = 36006, // Helper->self, no cast, range 20 180-degree cone
    MyriadAspects = 36019, // Boss->self, 3.0s cast, single-target
    MyriadAspects1 = 36020, // Helper->self, 4.0s cast, range 40 30-degree cone
    MyriadAspects2 = 36021, // Helper->self, 6.0s cast, range 40 30-degree cone
    Bladescatter = 35992, // Boss->self, 5.0s cast, single-target
    Scattering = 35993, // Helper->self, 3.0s cast, range 20 width 6 rect
    OrderedChaos = 36002, // Boss->self, no cast, single-target
    OrderedChaos2 = 36003, // Helper->player, 5.0s cast, range 5 circle
}

public enum IconID : uint
{
    Tankbuster = 342, // player
    Khadga1 = 454, // Helper, icon 1
    Khadga2 = 455, // Helper, icon 2
    Khadga3 = 456, // Helper, icon 3
    Khadga4 = 457, // Helper, icon 4
    Khadga5 = 458, // Helper, icon 5
    Khadga6 = 459, // Helper, icon 6
    Spreadmarker = 139, // player
}

// the opening raidwide trims the outer half-yalm of the arena
class ArenaShrink(BossModule module) : OuterDeathwall(module, AID.LowerRealm, 0.8f, new AOEShapeDonut(19, 20), false, T08Asura.DefaultBounds, 0x00020001, 0x01);

class AsuriChakra(BossModule module) : Components.StandardAOEs(module, AID.AsuriChakra, new AOEShapeCircle(5));
class Chakra1(BossModule module) : Components.StandardAOEs(module, AID.Chakra1, new AOEShapeDonut(6, 8));
class Chakra2(BossModule module) : Components.StandardAOEs(module, AID.Chakra2, new AOEShapeDonut(9, 11));
class Chakra3(BossModule module) : Components.StandardAOEs(module, AID.Chakra3, new AOEShapeDonut(12, 14));
class Chakra4(BossModule module) : Components.StandardAOEs(module, AID.Chakra4, new AOEShapeDonut(15, 17));
class Chakra5(BossModule module) : Components.StandardAOEs(module, AID.Chakra5, new AOEShapeDonut(18, 20));

class Laceration(BossModule module) : Components.GenericAOEs(module)
{
    private static readonly AOEShapeCircle circle = new(9);

    private DateTime _activation;

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor)
    {
        foreach (var c in Module.Enemies(OID.PhantomAsura))
            if (_activation != default)
                yield return new(circle, c.Position, default, _activation);
    }

    public override void OnActorPlayActionTimelineEvent(Actor actor, ushort id)
    {
        if (id == 0x11D6)
            _activation = WorldState.FutureTime(5);
    }

    public override void OnCastFinished(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID == AID.Laceration)
            _activation = default;
    }
}

class ManyFaces(BossModule module) : Components.GenericAOEs(module)
{
    private static readonly AOEShapeCone cone = new(20, 90.Degrees());
    private DateTime _activation;
    private bool delight;
    private bool wrath;
    private Angle _rotationWrath;
    private Angle _rotationDelight;

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor)
    {
        if (delight)
            yield return new(cone, Module.Center, _rotationDelight, _activation);
        if (wrath)
            yield return new(cone, Module.Center, _rotationWrath, _activation);
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID is AID.TheFaceOfWrathA or AID.TheFaceOfWrathB)
            delight = true;
        if ((AID)spell.Action.ID is AID.TheFaceOfDelightA or AID.TheFaceOfDelightB)
            wrath = true;
        if ((AID)spell.Action.ID == AID.FaceMechanicWrath)
        {
            _activation = Module.CastFinishAt(spell);
            _rotationDelight = spell.Rotation;
        }
        if ((AID)spell.Action.ID == AID.FaceMechanicDelight)
        {
            _activation = Module.CastFinishAt(spell);
            _rotationWrath = spell.Rotation;
        }
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if ((AID)spell.Action.ID is AID.TheFaceOfDelightSnapshot or AID.TheFaceOfWrathSnapshot)
        {
            delight = false;
            wrath = false;
        }
    }
}

class MyriadAspects(BossModule module) : Components.GenericAOEs(module)
{
    private static readonly AOEShapeCone cone = new(40, 15.Degrees());
    private DateTime _activation1;
    private DateTime _activation2;
    private readonly List<ActorCastInfo> _spell1 = [];
    private readonly List<ActorCastInfo> _spell2 = [];

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor)
    {
        if (NumCasts < 6 && _spell1.Count > 0)
            foreach (var c in _spell1)
                yield return new(cone, Module.Center, c.Rotation, _activation1);
        if (NumCasts >= 6 && _spell2.Count > 0)
            foreach (var c in _spell2)
                yield return new(cone, Module.Center, c.Rotation, _activation2);
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID == AID.MyriadAspects1)
        {
            _activation1 = Module.CastFinishAt(spell);
            _spell1.Add(spell);
        }
        if ((AID)spell.Action.ID == AID.MyriadAspects2)
        {
            _activation2 = Module.CastFinishAt(spell);
            _spell2.Add(spell);
        }
    }

    public override void OnCastFinished(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID is AID.MyriadAspects1 or AID.MyriadAspects2)
            ++NumCasts;
        if (NumCasts == 12)
        {
            NumCasts = 0;
            _spell1.Clear();
            _spell2.Clear();
        }
    }
}

class SixBladedKhadga(BossModule module) : Components.GenericAOEs(module)
{
    private readonly List<ActorCastInfo> _spell = [];
    private DateTime _start;
    private static readonly AOEShapeCone Cone = new(20, 90.Degrees());

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor)
    {
        if (_spell.Count > 0)
            yield return new(Cone, Module.PrimaryActor.Position, _spell[0].Rotation, _start, ArenaColor.Danger);
        if (_spell.Count > 1)
            yield return new(Cone, Module.PrimaryActor.Position, _spell[1].Rotation, _start.AddSeconds(2));
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID is AID.KhadgaTelegraph1 or AID.KhadgaTelegraph2 or AID.KhadgaTelegraph3)
        {
            _spell.Add(spell);
            if (_start == default)
                _start = WorldState.FutureTime(12.9f);
        }
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if ((AID)spell.Action.ID is AID.Khadga1 or AID.Khadga2 or AID.Khadga3 or AID.Khadga4 or AID.Khadga5 or AID.Khadga6)
        {
            ++NumCasts;
            _spell.RemoveAt(0);
            _start.AddSeconds(2);
            if (NumCasts == 6)
            {
                NumCasts = 0;
                _start = default;
            }
        }
    }
}

class LowerRealm(BossModule module) : Components.RaidwideCast(module, AID.LowerRealm);
class Ephemerality(BossModule module) : Components.RaidwideCast(module, AID.Ephemerality);
class CuttingJewel(BossModule module) : Components.BaitAwayCast(module, AID.CuttingJewel, new AOEShapeCircle(4), true);
class CuttingJewelHint(BossModule module) : Components.SingleTargetCast(module, AID.CuttingJewel);
class IconographyPedestalPurge(BossModule module) : Components.StandardAOEs(module, AID.IconographyPedestalPurge, 10);
class PedestalPurge(BossModule module) : Components.StandardAOEs(module, AID.PedestalPurge, 60);
class IconographyWheelOfDeincarnation(BossModule module) : Components.StandardAOEs(module, AID.IconographyWheelOfDeincarnation, new AOEShapeDonut(8, 40));
class WheelOfDeincarnation(BossModule module) : Components.StandardAOEs(module, AID.WheelOfDeincarnation, new AOEShapeDonut(48, 96));
class IconographyBladewise(BossModule module) : Components.StandardAOEs(module, AID.IconographyBladewise, new AOEShapeRect(50, 3));
class Bladewise(BossModule module) : Components.StandardAOEs(module, AID.Bladewise, new AOEShapeRect(100, 14));
class Scattering(BossModule module) : Components.StandardAOEs(module, AID.Scattering, new AOEShapeRect(20, 3));
class OrderedChaos(BossModule module) : Components.SpreadFromCastTargets(module, AID.OrderedChaos, 5);

class T08AsuraStates : StateMachineBuilder
{
    public T08AsuraStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<ArenaShrink>()
            .ActivateOnEnter<Ephemerality>()
            .ActivateOnEnter<LowerRealm>()
            .ActivateOnEnter<AsuriChakra>()
            .ActivateOnEnter<Chakra1>()
            .ActivateOnEnter<Chakra2>()
            .ActivateOnEnter<Chakra3>()
            .ActivateOnEnter<Chakra4>()
            .ActivateOnEnter<Chakra5>()
            .ActivateOnEnter<CuttingJewel>()
            .ActivateOnEnter<CuttingJewelHint>()
            .ActivateOnEnter<Laceration>()
            .ActivateOnEnter<IconographyPedestalPurge>()
            .ActivateOnEnter<PedestalPurge>()
            .ActivateOnEnter<IconographyWheelOfDeincarnation>()
            .ActivateOnEnter<WheelOfDeincarnation>()
            .ActivateOnEnter<IconographyBladewise>()
            .ActivateOnEnter<Bladewise>()
            .ActivateOnEnter<SixBladedKhadga>()
            .ActivateOnEnter<MyriadAspects>()
            .ActivateOnEnter<Scattering>()
            .ActivateOnEnter<OrderedChaos>()
            .ActivateOnEnter<ManyFaces>();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Malediktus, Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 944, NameID = 12351)]
public class T08Asura(ModuleInit init) : BossModule(init, new(100, 100), new ArenaBoundsCircle(19.5f))
{
    public static readonly ArenaBoundsCircle DefaultBounds = new(19);
}
