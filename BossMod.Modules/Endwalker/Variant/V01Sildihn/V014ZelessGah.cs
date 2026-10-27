namespace BossMod.Endwalker.Variant.V01Sildihn.V014ZelessGah;

public enum OID : uint
{
    Boss = 0x39A9, // R9.000, x1
    InfernBrand1 = 0x39AD, // R2.000
    InfernBrand2 = 0x39AB, // R2.000
    BallOfFire = 0x39AF, // R1.000, x0 (spawn during fight)
    ArcaneFont = 0x39B1, // R0.5-1.0, x0 (spawn during fight)
    MyrrhIncenseBurner = 0x1EB7CF, // R2.000, EventObj type
    Portal = 0x1EB761, // R0.500, EventObj type
    Helper = 0x233C, // R0.500, Helper type
}

public enum AID : uint
{
    AutoAttack = 6499, // Boss->player, no cast, single-target
    Teleport = 29886, // Boss->location, no cast, single-target
    Burn = 29839, // BallOfFire->self, 1.5s cast, range 12 circle
    CastShadowVisual = 29850, // Boss->self, 4.8s cast, single-target
    CastShadow1 = 29851, // Helper->self, 5.5s cast, range 65 30-degree cone
    CastShadow2 = 29853, // Helper->self, 7.5s cast, range 65 30-degree cone
    CrypticPortal1 = 29842, // Boss->self, 5.0s cast, single-target
    CrypticPortal2 = 29843, // Boss->self, 8.0s cast, single-target
    FiresteelFracture = 29868, // Boss->self/player, 5.0s cast, range 40 90-degree cone, tankbuster
    InfernBrand = 29841, // Boss->self, 4.0s cast, single-target
    InfernWard = 29846, // Boss->self, 4.0s cast, single-target
    PureFireVisual = 29855, // Boss->self, 3.0s cast, single-target
    PureFire = 29856, // Helper->location, 3.0s cast, range 6 circle
    ShowOfStrength = 29870, // Boss->self, 5.0s cast, range 65 circle, raidwide + arena shrink
    ActivateIncense = 29845, // Helper->InfernBrand1/InfernBrand2, no cast, single-target
    ActivateLaser = 29847, // Helper->self, no cast, single-target
    TrespassersPyre = 29848, // Helper->player, 1.0s cast, single-target
    TrespassersPyreKB = 29890, // Helper->self, no cast, range 60 width 100 rect, pull 50 between centers
    InfernGaleVisual = 29858, // Boss->self, 4.0s cast, single-target, route 5
    InfernGale = 29859, // Helper->player, no cast, single-target, knockback 20 away from source
    BlazingBenifice = 29861, // ArcaneFont->self, 1.5s cast, range 100 width 10 rect, route 6
    InfernWellVisual = 29863, // Boss->self, 4.0s cast, single-target, route 7
    InfernWell = 29864, // Helper->player, no cast, single-target, pull 15 between hitboxes
    InfernWellAOE = 29866, // Helper->self, 1.5s cast, range 8 circle
}

public enum SID : uint
{
    MechanicStatus = 2397, // none->InfernBrand1/InfernBrand2, extra=0x1CA (knockback) / 0x1CB (pull) / 0x1C1 + 0x1F3 (laser wall)
}

// the opening raidwide shrinks the room to the inner 30x40 rectangle
class ArenaShrink(BossModule module) : OuterDeathwall(module, AID.ShowOfStrength, 0.8f, new AOEShapeRect(20, 15, 20), true, V014ZelessGah.DefaultBounds, 0x00080004, 0x14);

class ArcaneFont(BossModule module) : Components.GenericAOEs(module, AID.BlazingBenifice)
{
    private static readonly AOEShapeRect _rect = new(100, 5);
    private readonly List<AOEInstance> _aoes = [];
    private readonly List<bool> _moved = [];
    private readonly List<Actor> _cachedPortals = [];
    private readonly List<Actor> _fireballs = [];

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor) => _aoes;

    public override void OnActorCreated(Actor actor)
    {
        switch ((OID)actor.OID)
        {
            case OID.ArcaneFont:
                var dir = V014ZelessGah.Round90(actor.Rotation).ToDirection();
                _aoes.Add(new(_rect, actor.Position - 50 * dir, actor.Rotation, WorldState.FutureTime(Module.Enemies(OID.Portal).Count == 0 ? 10.1f : 12.8f)));
                _moved.Add(false);
                break;
            case OID.BallOfFire:
                _fireballs.Add(actor);
                break;
            case OID.Portal:
                if (_aoes.Count > 0 && _fireballs.Count > 0)
                {
                    _cachedPortals.Clear();
                    return;
                }
                if (_aoes.Count == 0 || !TryTeleport(actor))
                    _cachedPortals.Add(actor);
                break;
        }
    }

    public override void Update()
    {
        if (_cachedPortals.Count == 0)
            return;
        if (Module.Enemies(OID.BallOfFire).Count > 0)
        {
            _cachedPortals.Clear();
            return;
        }
        if (_aoes.Count == 0)
            return;
        for (var i = _cachedPortals.Count - 1; i >= 0; --i)
            if (TryTeleport(_cachedPortals[i]))
                _cachedPortals.RemoveAt(i);
    }

    public override void OnCastFinished(Actor caster, ActorCastInfo spell)
    {
        switch ((AID)spell.Action.ID)
        {
            case AID.Burn:
                _fireballs.Remove(caster);
                _cachedPortals.Clear();
                break;
            case AID.BlazingBenifice:
                if (++NumCasts >= _aoes.Count)
                {
                    NumCasts = 0;
                    _aoes.Clear();
                    _moved.Clear();
                }
                break;
        }
    }

    private bool TryTeleport(Actor portal)
    {
        var activation = WorldState.FutureTime(12.5f);
        var dir = V014ZelessGah.Round90(_aoes[0].Rotation).ToDirection();
        var found = false;
        for (var i = 0; i < _aoes.Count; ++i)
        {
            if (_moved[i])
                continue;
            var aoe = _aoes[i];
            aoe.Activation = activation;
            var distSq = (aoe.Origin - (portal.Position - 50 * dir)).LengthSq();
            if (distSq is > 99 and < 105 or < 1)
            {
                var offset = distSq < 10 ? new WDir(0, 10) : new WDir(0, -10);
                aoe.Origin = new WPos(MathF.Round(aoe.Origin.X), MathF.Round(aoe.Origin.Z)) + offset;
                _moved[i] = true;
                found = true;
            }
            _aoes[i] = aoe;
        }
        return found;
    }
}

class BallOfFire(BossModule module) : Components.GenericAOEs(module, AID.Burn)
{
    private static readonly AOEShapeCircle _circle = new(12);
    private readonly List<AOEInstance> _aoes = [];
    private readonly List<Actor> _cachedPortals = [];

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor) => _aoes;

    public override void OnActorCreated(Actor actor)
    {
        switch ((OID)actor.OID)
        {
            case OID.BallOfFire:
                var portals = Module.Enemies(OID.Portal);
                if (portals.Any(p => (int)p.Rotation.Deg is 0 or 180 && (p.Position - actor.Position).LengthSq() < 105))
                    return;
                _aoes.Add(new(_circle, actor.Position, default, WorldState.FutureTime(portals.Count == 0 ? 10.2f : 15.8f)));
                break;
            case OID.Portal:
                var rot = V014ZelessGah.Round90(actor.Rotation);
                if ((int)rot.Deg is not 0 and not 180)
                {
                    if (!TryTeleport(actor))
                        _cachedPortals.Add(actor);
                }
                else
                {
                    var index = _aoes.FindIndex(a => (a.Origin - actor.Position).LengthSq() < 105);
                    if (index >= 0)
                        _aoes.RemoveAt(index);
                }
                break;
        }
    }

    public override void Update()
    {
        for (var i = _cachedPortals.Count - 1; i >= 0; --i)
            if (TryTeleport(_cachedPortals[i]))
                _cachedPortals.RemoveAt(i);
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        var delay = (AID)spell.Action.ID switch
        {
            AID.InfernGaleVisual => 16.5f,
            AID.InfernWellVisual => 18.7f,
            _ => 0
        };
        if (delay > 0)
            SetActivation(WorldState.FutureTime(delay));
    }

    public override void OnCastFinished(Actor caster, ActorCastInfo spell)
    {
        switch ((AID)spell.Action.ID)
        {
            case AID.Burn:
                if ((++NumCasts & 3) == 0)
                    _aoes.Clear();
                break;
            case AID.BlazingBenifice:
                _cachedPortals.Clear();
                break;
        }
    }

    public override void OnActorEAnim(Actor actor, uint state)
    {
        if ((OID)actor.OID != OID.Portal)
            return;
        var offset = state switch
        {
            0x00400080 => -90,
            0x01000200 => 90,
            _ => 0
        };
        if (offset != 0)
        {
            var activation = WorldState.FutureTime(12.5f);
            SetActivation(activation);
            _aoes.Add(new(_circle, actor.Position + 10 * (V014ZelessGah.Round90(actor.Rotation) + offset.Degrees()).ToDirection(), default, activation));
        }
    }

    private bool TryTeleport(Actor portal)
    {
        var activation = WorldState.FutureTime(12.5f);
        var found = false;
        for (var i = 0; i < _aoes.Count; ++i)
        {
            var aoe = _aoes[i];
            aoe.Activation = activation;
            if ((aoe.Origin - portal.Position).LengthSq() < 105)
            {
                aoe.Origin = new WPos(MathF.Round(aoe.Origin.X) == 284 ? 294 : 284, portal.Position.Z);
                found = true;
            }
            _aoes[i] = aoe;
        }
        return found;
    }

    private void SetActivation(DateTime activation)
    {
        for (var i = 0; i < _aoes.Count; ++i)
            _aoes[i] = _aoes[i] with { Activation = activation };
    }
}

// the incense burners' mechanic is picked by the extra value of the status they receive
static class BrandMechanic
{
    public const ushort LaserWall = 0x1C1;
    public const ushort LaserWallEnd = 0x1F3;
    public const ushort Knockback = 0x1CA;
    public const ushort Pull = 0x1CB;

    public static bool Is(in ActorStatus status, ushort mechanic) => (SID)status.ID == SID.MechanicStatus && status.Extra == mechanic;
}

// a brand marked for the laser wall fires a thin line along its facing ~7.7s later; all walls drop together
class TrespassersPyre(BossModule module) : Components.GenericAOEs(module)
{
    private static readonly AOEShapeRect Wall = new(100, 1);
    private readonly Dictionary<ulong, (WPos Origin, Angle Direction)> _walls = [];
    private DateTime _activation;

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor)
    {
        foreach (var w in _walls.Values)
            yield return new(Wall, w.Origin, w.Direction, _activation);
    }

    public override void OnStatusGain(Actor actor, in ActorStatus status)
    {
        if (!BrandMechanic.Is(status, BrandMechanic.LaserWall))
            return;
        _walls[actor.InstanceID] = (actor.Position, actor.Rotation);
        _activation = WorldState.FutureTime(7.7f);
    }

    public override void OnStatusLose(Actor actor, in ActorStatus status)
    {
        if (BrandMechanic.Is(status, BrandMechanic.LaserWallEnd))
            _walls.Clear();
    }
}

// knockback 20 away from the marked brand
class InfernGale(BossModule module) : Components.Knockback(module, AID.InfernGale)
{
    private WPos _origin;
    private DateTime _activation;

    public override IEnumerable<Source> Sources(int slot, Actor actor)
    {
        if (_activation != default)
            yield return new(_origin, 20, _activation);
    }

    public override void OnStatusGain(Actor actor, in ActorStatus status)
    {
        base.OnStatusGain(actor, status);
        if (BrandMechanic.Is(status, BrandMechanic.Knockback))
        {
            _origin = actor.Position;
            _activation = WorldState.FutureTime(5.6f);
        }
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        base.OnEventCast(caster, spell);
        if (spell.Action == WatchedAction)
            _activation = default;
    }
}

// a pull towards the marked brand, which then explodes in an 8y circle; the cast itself refines the timing
class InfernWellAOE(BossModule module) : Components.GenericAOEs(module, AID.InfernWellAOE)
{
    private static readonly AOEShapeCircle Blast = new(8);
    public WPos Origin { get; private set; }
    public DateTime Activation { get; private set; }
    public bool Active => Activation != default;

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor)
    {
        if (Active)
            yield return new(Blast, Origin, default, Activation);
    }

    public bool Covers(WPos pos) => Active && pos.InCircle(Origin, Blast.Radius);

    public override void OnStatusGain(Actor actor, in ActorStatus status)
    {
        if (BrandMechanic.Is(status, BrandMechanic.Pull))
        {
            Origin = actor.Position;
            Activation = WorldState.FutureTime(9.1f);
        }
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if (spell.Action == WatchedAction && Active)
            Activation = Module.CastFinishAt(spell);
    }

    public override void OnCastFinished(Actor caster, ActorCastInfo spell)
    {
        if (spell.Action == WatchedAction)
            Activation = default;
    }
}

class InfernWellPull(BossModule module) : Components.Knockback(module, AID.InfernWell)
{
    private const float PullDistance = 15;
    private InfernWellAOE? _blast;
    private WPos _origin;
    private DateTime _activation;

    public override IEnumerable<Source> Sources(int slot, Actor actor)
    {
        if (_activation != default)
            yield return new(_origin, PullDistance, _activation, Kind: Kind.TowardsOrigin, MinDistance: 1);
    }

    // landing inside the explosion that follows is just as bad as getting pulled into a wall
    public override bool DestinationUnsafe(int slot, Actor actor, WPos pos)
    {
        _blast ??= Module.FindComponent<InfernWellAOE>();
        return _blast?.Covers(pos) ?? false;
    }

    public override void AddAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        base.AddAIHints(slot, actor, assignment, hints);
        // without immunity, stand far enough away that the pull can't drag you into the blast (8 radius + 15 pull)
        if (_activation != default && !IsImmune(slot, _activation))
            hints.AddForbiddenZone(ShapeDistance.Circle(_origin, 8 + PullDistance), _activation);
    }

    public override void OnStatusGain(Actor actor, in ActorStatus status)
    {
        base.OnStatusGain(actor, status);
        if (BrandMechanic.Is(status, BrandMechanic.Pull))
        {
            _origin = actor.Position;
            _activation = WorldState.FutureTime(7.6f);
        }
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        base.OnEventCast(caster, spell);
        if (spell.Action == WatchedAction)
            _activation = default;
    }
}

class PureFire(BossModule module) : Components.StandardAOEs(module, AID.PureFire, new AOEShapeCircle(6));
class FiresteelFracture(BossModule module) : Components.BaitAwayCast(module, AID.FiresteelFracture, new AOEShapeCone(50, 45.Degrees()), endsOnCastEvent: true);
class ShowOfStrength(BossModule module) : Components.RaidwideCast(module, AID.ShowOfStrength);
class CastShadow(BossModule module) : Components.GroupedAOEs(module, [AID.CastShadow1, AID.CastShadow2], new AOEShapeCone(50, 15.Degrees()), 6);

class V014ZelessGahStates : StateMachineBuilder
{
    public V014ZelessGahStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<ShowOfStrength>()
            .ActivateOnEnter<ArenaShrink>()
            .ActivateOnEnter<FiresteelFracture>()
            .ActivateOnEnter<PureFire>()
            .ActivateOnEnter<CastShadow>()
            .ActivateOnEnter<BallOfFire>()
            .ActivateOnEnter<ArcaneFont>()
            .ActivateOnEnter<TrespassersPyre>()
            .ActivateOnEnter<InfernGale>()
            .ActivateOnEnter<InfernWellAOE>()
            .ActivateOnEnter<InfernWellPull>();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 868, NameID = 11393, SortOrder = 5)]
public class V014ZelessGah(ModuleInit init) : BossModule(init, new(289, -105), new ArenaBoundsRect(24.5f, 29.5f))
{
    public static readonly ArenaBoundsRect DefaultBounds = new(15, 20);

    public static Angle Round90(Angle a) => (MathF.Round(a.Deg / 90) * 90).Degrees();
}
