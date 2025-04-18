﻿namespace BossMod.Endwalker.Hunt.RankS.Sphatika;

public enum OID : uint
{
    Boss = 0x3670, // R8.750, x1
}

public enum AID : uint
{
    AutoAttack = 872, // Boss->player, no cast, single-target
    Gnaw = 27617, // Boss->player, 5.0s cast, single-target tankbuster
    Infusion = 27618, // Boss->players, no cast, width 10 rect charge
    Caterwaul = 27619, // Boss->self, 5.0s cast, range 40 circle

    Brace1 = 27620, // Boss->self, 3.0s cast, single-target (??? forward->backward, lickwhip 624->626->715->627->715)
    Brace2 = 27621, // ???
    Brace3 = 27622, // Boss->self, 3.0s cast, single-target (??? backward->rightward, whiplick 625->631->714->633->714)
    Brace4 = 27623, // Boss->self, 3.0s cast, single-target (??? left->right, whiplick 625->632->714->633->714)
    LickwhipStance = 27624, // Boss->self, 5.0s cast, single-target, visual (lick -> whip using first bearing buff)
    WhiplickStance = 27625, // Boss->self, 5.0s cast, single-target, visual (whip -> lick using first bearing buff)
    LongLickForward = 27626, // Boss->self, 1.0s cast, range 40 180-degree cone aimed forward (with forward bearing)
    LongLickBackward = 27627, // Boss->self, 1.0s cast, range 40 180-degree cone aimed backward (with backward bearing)
    LongLickLeftward = 27628, // Boss->self, 1.0s cast, range 40 180-degree cone aimed left (with leftward bearing)
    LongLickRightward = 27629, // Boss->self, 1.0s cast, range 40 180-degree cone aimed right (with rightward bearing)
    HindWhipForward = 27630, // Boss->self, 1.0s cast, range 40 180-degree cone aimed backward (with forward bearing)
    HindWhipBackward = 27631, // Boss->self, 1.0s cast, range 40 180-degree cone aimed forward (with backward bearing)
    HindWhipLeftward = 27632, // Boss->self, 1.0s cast, range 40 180-degree cone aimed right (with leftward bearing)
    HindWhipRightward = 27633, // Boss->self, 1.0s cast, range 40 180-degree cone aimed left (with rightward bearing)
    LongLickSecond = 27714, // Boss->self, 1.0s cast, range 40 180-degree cone (after hind whip)
    HindWhipSecond = 27715, // Boss->self, 1.0s cast, range 40 180-degree cone (after long lick)
}

public enum SID : uint
{
    ForwardBearing = 2835, // Boss->Boss, extra=0x0
    BackwardBearing = 2836, // Boss->Boss, extra=0x0
    LeftwardBearing = 2837, // Boss->Boss, extra=0x0
    RightwardBearing = 2838, // Boss->Boss, extra=0x0
}

class Gnaw(BossModule module) : Components.SingleTargetCast(module, AID.Gnaw);
class Caterwaul(BossModule module) : Components.RaidwideCast(module, AID.Caterwaul);

class Stance(BossModule module) : Components.GenericAOEs(module)
{
    private readonly List<Angle> _pendingCleaves = [];

    private static readonly AOEShapeCone _shape = new(40, 90.Degrees());

    public override IEnumerable<AOEInstance> ActiveAOEs(int slot, Actor actor)
    {
        if (_pendingCleaves.Count > 0)
            yield return new(_shape, Module.PrimaryActor.Position, _pendingCleaves[0]); // TODO: activation
    }

    public override void AddGlobalHints(GlobalHints hints)
    {
        if (!(Module.PrimaryActor.CastInfo?.IsSpell() ?? false))
            return;
        var hint = (AID)Module.PrimaryActor.CastInfo!.Action.ID switch
        {
            AID.Brace1 or AID.Brace2 or AID.Brace3 or AID.Brace4 => "Select directions",
            AID.LickwhipStance => "Cleave sides literal",
            AID.WhiplickStance => "Cleave sides inverted",
            _ => ""
        };
        if (hint.Length > 0)
            hints.Add(hint);
    }

    public override void OnCastStarted(Actor caster, ActorCastInfo spell)
    {
        if (caster != Module.PrimaryActor)
            return;

        switch ((AID)spell.Action.ID)
        {
            case AID.LickwhipStance:
                InitCleaves(spell.Rotation, false);
                break;
            case AID.WhiplickStance:
                InitCleaves(spell.Rotation, true);
                break;
        }
    }

    public override void OnCastFinished(Actor caster, ActorCastInfo spell)
    {
        if (caster == Module.PrimaryActor && _pendingCleaves.Count > 0 &&
            (AID)spell.Action.ID is AID.LongLickForward or AID.LongLickBackward or AID.LongLickLeftward or AID.LongLickRightward or AID.LongLickSecond
                                 or AID.HindWhipForward or AID.HindWhipBackward or AID.HindWhipLeftward or AID.HindWhipRightward or AID.HindWhipSecond)
        {
            _pendingCleaves.RemoveAt(0);
        }
    }

    private void InitCleaves(Angle reference, bool inverted)
    {
        // bearings are resolved in UI order; it is forward > backward > left > right, see PartyListPriority column
        List<(Angle offset, int priority)> bearings = [];
        foreach (var s in Module.PrimaryActor.Statuses)
        {
            switch ((SID)s.ID)
            {
                case SID.ForwardBearing:
                    bearings.Add((0.Degrees(), 0));
                    break;
                case SID.BackwardBearing:
                    bearings.Add((180.Degrees(), 1));
                    break;
                case SID.LeftwardBearing:
                    bearings.Add((90.Degrees(), 2));
                    break;
                case SID.RightwardBearing:
                    bearings.Add((-90.Degrees(), 3));
                    break;
            }
        }
        bearings.SortBy(b => b.priority);

        _pendingCleaves.Clear();
        foreach (var b in bearings)
        {
            var dir = reference + b.offset;
            if (inverted)
                dir += 180.Degrees();
            _pendingCleaves.Add(dir);
            _pendingCleaves.Add(dir + 180.Degrees());
        }
    }
}

class SphatikaStates : StateMachineBuilder
{
    public SphatikaStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<Gnaw>()
            .ActivateOnEnter<Caterwaul>()
            .ActivateOnEnter<Stance>();
    }
}

[ModuleInfo(BossModuleInfo.Maturity.Verified, GroupType = BossModuleInfo.GroupType.Hunt, GroupID = (uint)BossModuleInfo.HuntRank.S, NameID = 10618)]
public class Sphatika(WorldState ws, Actor primary) : SimpleBossModule(ws, primary);
