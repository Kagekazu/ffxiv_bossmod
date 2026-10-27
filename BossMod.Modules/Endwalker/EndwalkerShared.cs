namespace BossMod.Endwalker;

abstract class OuterDeathwall(BossModule module, Enum trigger, float delay, AOEShape warning, bool safeInside, ArenaBounds shrunk, uint mapState, params byte[] mapIndices)
    : Components.GenericAOEs(module, trigger)
{
    protected readonly ArenaBounds Shrunk = shrunk;
    private DateTime _deadline;

    protected virtual bool ShouldWarn => Arena.Bounds.Radius > Shrunk.Radius;

    protected virtual DateTime WarningTime(ActorCastInfo spell) => Module.CastFinishAt(spell, delay);

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor)
    {
        if (_deadline != default)
            yield return new(warning, Arena.Center, default, _deadline, Inverted: safeInside);
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if (spell.Action == WatchedAction && ShouldWarn)
            _deadline = WarningTime(spell);
    }

    public override void OnMapEffect(byte index, uint state)
    {
        if (state == mapState && Array.IndexOf(mapIndices, index) >= 0)
        {
            _deadline = default;
            Arena.Bounds = Shrunk;
        }
    }
}

class ExpandingRings(BossModule module, Enum first, params AOEShape[] rings) : Components.ConcentricAOEs(module, rings)
{
    private readonly uint _firstId = (uint)(object)first;

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if (spell.Action.ID == _firstId)
            AddSequence(spell.LocXZ, Module.CastFinishAt(spell));
    }

    public override void OnCastFinished(Actor caster, ActorCastInfo spell)
    {
        if (Sequences.Count == 0 || spell.Action.ID < _firstId)
            return;
        var ring = (int)(spell.Action.ID - _firstId);
        if (ring < Shapes.Length && !AdvanceSequence(ring, spell.LocXZ, WorldState.FutureTime(2)))
            ReportError($"no ring sequence at {spell.LocXZ} for step {ring}");
    }
}
