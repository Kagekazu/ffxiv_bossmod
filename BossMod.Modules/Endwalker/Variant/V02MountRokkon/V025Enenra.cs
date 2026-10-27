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

class ArenaShrink(BossModule module) : OuterDeathwall(module, AID.FlagrantCombustion, 2.9f, new AOEShapeDonut(20, 21), false, V025Enenra.DefaultBounds, 0x00020001, 0x28);

class IntoTheFire(BossModule module) : Components.GenericAOEs(module, AID.IntoTheFire)
{
    private static readonly AOEShapeRect _halfArena = new(50, 25);
    private static readonly float _diagonalReach = 11 * MathF.Sqrt(2);
    private readonly List<AOEInstance> _sweeps = [];

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor) => _sweeps;

    public override void OnActorEAnim(Actor actor, uint state)
    {
        if (state != 0x00010002 || Telegraph((OID)actor.OID) is not var (originAngle, reach, facing))
            return;
        var origin = actor.Position + (actor.Rotation + originAngle).ToDirection() * reach;
        origin = new(MathF.Round(origin.X), MathF.Round(origin.Z));
        _sweeps.Add(new(_halfArena, origin, actor.Rotation + facing, WorldState.FutureTime(16.6f)));
    }

    public override void OnCastFinished(Actor caster, ActorCastInfo spell)
    {
        if (spell.Action != WatchedAction || _sweeps.Count == 0)
            return;
        var index = _sweeps.FindIndex(s => s.Origin.AlmostEqual(caster.Position, 2));
        _sweeps.RemoveAt(index >= 0 ? index : 0);
    }

    private static (Angle, float, Angle)? Telegraph(OID wisp) => wisp switch
    {
        OID.SmokeVisual1 => (-45.Degrees(), _diagonalReach, 90.Degrees()),
        OID.SmokeVisual2 => (45.Degrees(), _diagonalReach, -90.Degrees()),
        OID.SmokeVisual3 => (default(Angle), 22f, 180.Degrees()),
        _ => null
    };
}

class StringRock(BossModule module) : ExpandingRings(module, AID.KiseruClamor, new AOEShapeCircle(6), new AOEShapeDonut(6, 12), new AOEShapeDonut(12, 18), new AOEShapeDonut(18, 24));

class ClearingSmoke(BossModule module) : Components.KnockbackFromCastTarget(module, AID.ClearingSmoke, 16, stopAtWall: true)
{
    private Smoldering? _smoldering;

    public override void AddAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        _smoldering ??= Module.FindComponent<Smoldering>();
        if (Casters.Count == 0 || _smoldering is not { Casters.Count: > 0 })
            return;
        hints.AddForbiddenZone(ShapeDistance.InvertedCircle(Module.Center, 4), Module.CastFinishAt(Casters[0].CastInfo));
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
            .ActivateOnEnter<FlagrantCombustion>()
            .ActivateOnEnter<ArenaShrink>()
            .ActivateOnEnter<Snuff>()
            .ActivateOnEnter<PipeCleaner>()
            .ActivateOnEnter<Uplift>()
            .ActivateOnEnter<StringRock>()
            .ActivateOnEnter<SmokeRings>()
            .ActivateOnEnter<Smoldering>()
            .ActivateOnEnter<ClearingSmoke>()
            .ActivateOnEnter<IntoTheFire>();
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
