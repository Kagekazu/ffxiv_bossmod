namespace BossMod.RealmReborn.Dungeon.D11DzemaelDarkhold.D113Batraal;

public enum OID : uint
{
    Boss = 0x4A8D, // R4.600, Batraal (Duty Support / remaster)
    CorruptedCrystal = 0x4A8E, // R1.000
    CorruptedCrystalOld = 0x60B, // R1.000, legacy crystal casts still seen
}

public enum AID : uint
{
    AutoAttack = 870, // Boss->player, no cast, single-target
    Hit45577 = 45577, // Boss->self, no cast
    Hit45578 = 45578, // Boss->self, no cast
    Cast45579 = 45579, // Boss->self, 4.7s cast
    Cast45580 = 45580, // Boss->self, 2.7s cast
    Hit45581 = 45581, // Boss->self, no cast
    Cast45582 = 45582, // Boss->self, 2.7s cast
    AetherialSurge = 1167, // CorruptedCrystalOld->self, 2.7s cast, range ~6 circle
}

class Cast45579(BossModule module) : Components.CastHint(module, AID.Cast45579, "Boss cast");
class Cast45580(BossModule module) : Components.CastHint(module, AID.Cast45580, "Boss cast");
class Cast45582(BossModule module) : Components.CastHint(module, AID.Cast45582, "Boss cast");
class AetherialSurge(BossModule module) : Components.StandardAOEs(module, AID.AetherialSurge, 6);
class Crystals(BossModule module) : Components.AddsMulti(module, [OID.CorruptedCrystal, OID.CorruptedCrystalOld], 2);

class D113BatraalStates : StateMachineBuilder
{
    public D113BatraalStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<Cast45579>()
            .ActivateOnEnter<Cast45580>()
            .ActivateOnEnter<Cast45582>()
            .ActivateOnEnter<AetherialSurge>()
            .ActivateOnEnter<Crystals>();
    }
}

[ModuleInfo(Contributors = "Kagekazu", Incomplete = true, GroupType = BossModuleInfo.GroupType.CFC, GroupID = 13, NameID = 1396)]
public class D113Batraal(WorldState ws, Actor primary) : BossModule(ws, primary, new(85, -175), new ArenaBoundsSquare(25));
