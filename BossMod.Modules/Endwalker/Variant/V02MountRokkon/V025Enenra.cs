namespace BossMod.Endwalker.Variant.V02MountRokkon.V025Enenra;

public enum OID : uint
{
    Boss = 0x3EAD, // R2.800, x1
    EnenraClone = 0x3EAE, // R2.800, x0 (spawn during fight)
    SmokeVisual1 = 0x1EB88F, // R0.500, EventObj type
    SmokeVisual2 = 0x1EB890, // R0.500, EventObj type
    SmokeVisual3 = 0x1EB891, // R0.500, EventObj type
    Smoldering = 0x1EB892, // R0.500, EventObj type
    Helper = 0x233C, // R0.500, Helper type
}

public enum AID : uint
{
    AutoAttack = 6497, // Boss/EnenraClone->player, no cast, single-target
    Teleport1 = 32846, // Boss/EnenraClone->location, no cast, single-target
    Teleport2 = 33986, // Boss/EnenraClone->location, no cast, single-target
    Teleport3 = 32837, // EnenraClone->location, no cast, single-target
    KiseruClamor = 32840, // Boss/EnenraClone->location, 3.0s cast, range 6 circle, first of the cascading rings
    BedrockUplift1 = 32841, // Helper->self, 5.0s cast, range 6-12 donut
    BedrockUplift2 = 32842, // Helper->self, 7.0s cast, range 12-18 donut
    BedrockUplift3 = 32843, // Helper->self, 9.0s cast, range 18-24 donut
    ClearingSmokeVisual = 32866, // EnenraClone/Boss->self, 8.0s cast, single-target
    ClearingSmoke = 32850, // Helper->self, 11.5s cast, range 21 circle, knockback 16
    SmokeRingsVisual = 32867, // EnenraClone/Boss->self, 8.0s cast, single-target
    SmokeRings = 32851, // Helper->self, 11.5s cast, range 16 circle
    FlagrantCombustion = 32834, // Boss/EnenraClone->self, 5.0s cast, range 50 circle, raidwide
    OutOfTheSmoke = 32844, // Boss/EnenraClone->self, 12.0s cast, single-target
    IntoTheFireVisual = 32845, // Boss/EnenraClone->self, 1.0s cast, single-target
    IntoTheFire = 32856, // Helper->self, 1.5s cast, range 50 width 50 rect
    PipeCleaner = 32852, // Boss->self, 5.0s cast, single-target
    PipeCleanerAOE = 32853, // Boss->self, no cast, range 50 width 6 rect, tethered player
    SmokeAndMirrors = 32835, // Boss->self, 2.5s cast, single-target
    SmokeStackBoss = 32838, // Boss->location, 2.0s cast, single-target, recombine
    SmokeStackClone = 32839, // EnenraClone->location, 2.0s cast, single-target, recombine
    Smoldering = 32848, // Helper->self, 7.0s cast, range 8 circle
    SmolderingDamnation = 32847, // Boss->self, 4.0s cast, single-target
    Snuff = 32854, // Boss->player, 5.0s cast, range 6 circle, tankbuster
    Uplift = 32855, // Helper->location, 3.5s cast, range 6 circle
}

public enum TetherID : uint
{
    PipeCleaner = 17, // player->Boss
}

class ArenaChange(BossModule module) : Components.GenericAOEs(module)
{
    private static readonly AOEShapeDonut _donut = new(20, 21);
    private AOEInstance? _aoe;

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor) => Utils.ZeroOrOne(_aoe);

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID == AID.FlagrantCombustion && Arena.Bounds.Radius > 20)
            _aoe = new(_donut, Arena.Center, default, Module.CastFinishAt(spell, 2.9f));
    }

    public override void OnMapEffect(byte index, uint state)
    {
        if (index == 0x28 && state == 0x00020001)
        {
            Arena.Bounds = V025Enenra.DefaultBounds;
            _aoe = null;
        }
    }
}

class IntoTheFire(BossModule module) : Components.GenericAOEs(module)
{
    private static readonly AOEShapeRect _rect = new(50, 25);
    private const float Offset = 15.556349f;
    private readonly List<AOEInstance> _aoes = [];

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor) => _aoes;

    public override void OnActorEAnim(Actor actor, uint state)
    {
        if (state != 0x00010002)
            return;
        var rot = actor.Rotation;
        (WDir offset, Angle dir)? smoke = (OID)actor.OID switch
        {
            OID.SmokeVisual1 => (Offset * (rot - 45.Degrees()).ToDirection(), rot + 90.Degrees()),
            OID.SmokeVisual2 => (Offset * (rot + 45.Degrees()).ToDirection(), rot - 90.Degrees()),
            OID.SmokeVisual3 => (22 * rot.ToDirection(), rot + 180.Degrees()),
            _ => null
        };
        if (smoke is var (offset, dir))
        {
            var pos = actor.Position + offset;
            _aoes.Add(new(_rect, new(MathF.Round(pos.X), MathF.Round(pos.Z)), dir, WorldState.FutureTime(16.6f)));
        }
    }

    public override void OnCastFinished(Actor caster, ActorCastInfo spell)
    {
        if (_aoes.Count > 0 && (AID)spell.Action.ID == AID.IntoTheFire)
            _aoes.RemoveAt(0);
    }
}

class StringRock(BossModule module) : Components.ConcentricAOEs(module, _shapes)
{
    private static readonly AOEShape[] _shapes = [new AOEShapeCircle(6), new AOEShapeDonut(6, 12), new AOEShapeDonut(12, 18), new AOEShapeDonut(18, 24)];

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID == AID.KiseruClamor)
            AddSequence(spell.LocXZ, Module.CastFinishAt(spell));
    }

    public override void OnCastFinished(Actor caster, ActorCastInfo spell)
    {
        if (Sequences.Count == 0)
            return;
        var order = (AID)spell.Action.ID switch
        {
            AID.KiseruClamor => 0,
            AID.BedrockUplift1 => 1,
            AID.BedrockUplift2 => 2,
            AID.BedrockUplift3 => 3,
            _ => -1
        };
        AdvanceSequence(order, spell.LocXZ, WorldState.FutureTime(2));
    }
}

class ClearingSmoke(BossModule module) : Components.KnockbackFromCastTarget(module, AID.ClearingSmoke, 16, stopAtWall: true)
{
    private readonly Smoldering? _smoldering = module.FindComponent<Smoldering>();

    public override void AddAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        if (Casters.Count > 0 && _smoldering?.Casters.Count > 0)
            hints.AddForbiddenZone(ShapeDistance.InvertedCircle(Arena.Center, 4), Module.CastFinishAt(Casters[0].CastInfo));
    }
}

class PipeCleaner(BossModule module) : Components.BaitAwayTethers(module, new AOEShapeRect(60, 5), (uint)TetherID.PipeCleaner);
class Uplift(BossModule module) : Components.StandardAOEs(module, AID.Uplift, new AOEShapeCircle(6));
class Snuff(BossModule module) : Components.BaitAwayCast(module, AID.Snuff, new AOEShapeCircle(6), true);
class Smoldering(BossModule module) : Components.StandardAOEs(module, AID.Smoldering, new AOEShapeCircle(8), 8);
class FlagrantCombustion(BossModule module) : Components.RaidwideCast(module, AID.FlagrantCombustion);
class SmokeRings(BossModule module) : Components.StandardAOEs(module, AID.SmokeRings, new AOEShapeCircle(16));

class V025EnenraStates : StateMachineBuilder
{
    public V025EnenraStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<ArenaChange>()
            .ActivateOnEnter<IntoTheFire>()
            .ActivateOnEnter<StringRock>()
            .ActivateOnEnter<Smoldering>()
            .ActivateOnEnter<ClearingSmoke>()
            .ActivateOnEnter<PipeCleaner>()
            .ActivateOnEnter<Uplift>()
            .ActivateOnEnter<Snuff>()
            .ActivateOnEnter<FlagrantCombustion>()
            .ActivateOnEnter<SmokeRings>();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 945, NameID = 12393, SortOrder = 7)]
public class V025Enenra(ModuleInit init) : BossModule(init, new(900, -900), new ArenaBoundsCircle(20.5f))
{
    public static readonly ArenaBoundsCircle DefaultBounds = new(20);

    protected override void DrawEnemies(int pcSlot, Actor pc)
    {
        Arena.Actor(PrimaryActor, ArenaColor.Enemy);
        Arena.Actors(Enemies(OID.EnenraClone), ArenaColor.Enemy);
    }
}
