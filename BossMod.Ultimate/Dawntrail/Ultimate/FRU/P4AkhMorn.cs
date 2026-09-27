namespace BossMod.Dawntrail.Ultimate.FRU;

// TODO: can target change if boss is provoked mid cast?
class P4AkhMorn(BossModule module) : Components.UniformStackSpread(module, 4, 0, 4)
{
    public int NumCasts;
    private readonly FRUConfig _config = Service.Config.Get<FRUConfig>();
    private const float StackOffset = 7f; // E/W from center so stacks don't sit under the bosses

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

        // G1 west, G2 east
        var side = group == 0 ? -1 : 1;
        var dest = Module.Center + new WDir(side * StackOffset, 0);
        var other = Module.Center + new WDir(-side * StackOffset, 0);
        var activation = Stacks[0].Activation;
        hints.AddForbiddenZone(ShapeDistance.InvertedCircle(dest, 1.5f), activation);
        hints.AddForbiddenZone(ShapeDistance.Circle(other, StackRadius), activation);
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
}
