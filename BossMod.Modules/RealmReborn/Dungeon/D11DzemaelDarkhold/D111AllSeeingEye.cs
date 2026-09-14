namespace BossMod.RealmReborn.Dungeon.D11DzemaelDarkhold.D111AllSeeingEye;

public enum OID : uint
{
    Boss = 0x4A8A, // R2.700, All-seeing Eye (Duty Support / remaster)
    Helper = 0x233C, // R0.500
    CrystalOld = 0x60B, // R1.000, Corrupted Crystal (trash/legacy)
}

public enum AID : uint
{
    AutoAttack = 870, // Boss->player, no cast, single-target
    LongCast = 45567, // Boss->self, 7.2s cast
    HelperAOE = 45568, // Helper->self, 7.7s cast
    MidCast = 45569, // Boss->self, 4.7s cast
    ShortCast = 45570, // Boss->self, 2.7s cast
}

class LongCast(BossModule module) : Components.CastHint(module, AID.LongCast, "Boss cast");
class HelperAOE(BossModule module) : Components.CastHint(module, AID.HelperAOE, "Helper AOE");
class MidCast(BossModule module) : Components.CastHint(module, AID.MidCast, "Boss cast");
class ShortCast(BossModule module) : Components.CastHint(module, AID.ShortCast, "Boss cast");

class D111AllSeeingEyeStates : StateMachineBuilder
{
    public D111AllSeeingEyeStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<LongCast>()
            .ActivateOnEnter<HelperAOE>()
            .ActivateOnEnter<MidCast>()
            .ActivateOnEnter<ShortCast>();
    }
}

[ModuleInfo(Contributors = "Kagekazu", Incomplete = true, GroupType = BossModuleInfo.GroupType.CFC, GroupID = 13, NameID = 1397)]
public class D111AllSeeingEye(WorldState ws, Actor primary) : BossModule(ws, primary, new(48, 78), new ArenaBoundsSquare(25));
