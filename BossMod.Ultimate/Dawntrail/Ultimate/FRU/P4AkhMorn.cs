namespace BossMod.Dawntrail.Ultimate.FRU;

// TODO: can target change if boss is provoked mid cast?
class P4AkhMorn(BossModule module) : Components.UniformStackSpread(module, 4, 0, 4)
{
    public int NumCasts;
    private readonly FRUConfig _config = Service.Config.Get<FRUConfig>();

    public override void AddAIHints(int slot, Actor actor, PartyRolesConfig.Assignment assignment, AIHints hints)
    {
        if (Stacks.Count < 2)
        {
            base.AddAIHints(slot, actor, assignment, hints);
            return;
        }

        var group = _config.P5AkhMornAssignments[assignment];
        if (group < 0)
        {
            base.AddAIHints(slot, actor, assignment, hints);
            return;
        }

        var assigned = AssignedStack(group);
        if (assigned.Target == null)
        {
            base.AddAIHints(slot, actor, assignment, hints);
            return;
        }

        foreach (var s in Stacks)
        {
            if (s.Target == assigned.Target)
                hints.AddForbiddenZone(ShapeDistance.InvertedCircle(s.Target.Position, s.Radius), s.Activation);
            else
                hints.AddForbiddenZone(ShapeDistance.Circle(s.Target.Position, s.Radius), s.Activation);
        }
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if ((AID)spell.Action.ID is AID.AkhMornOracle or AID.AkhMornUsurper && WorldState.Actors.Find(caster.TargetID) is var target && target != null)
            AddStack(target, Module.CastFinishAt(spell, 0.9f));
    }

    public override void OnEventCast(Actor caster, ActorCastEvent spell)
    {
        if ((AID)spell.Action.ID == AID.AkhMornAOEOracle)
            ++NumCasts;
    }

    private Stack AssignedStack(int group)
    {
        var roles = Service.Config.Get<PartyRolesConfig>().SlotsPerAssignment(Raid);
        if (roles.Length > 0)
        {
            var tankRole = group == 0 ? PartyRolesConfig.Assignment.MT : PartyRolesConfig.Assignment.OT;
            var tank = Raid[roles[(int)tankRole]];
            var byTank = Stacks.FirstOrDefault(s => s.Target == tank);
            if (byTank.Target != null)
                return byTank;
        }

        // fallback: G1 west, G2 east
        var ordered = Stacks.OrderBy(s => s.Target.Position.X).ToList();
        return ordered[Math.Clamp(group, 0, ordered.Count - 1)];
    }
}
