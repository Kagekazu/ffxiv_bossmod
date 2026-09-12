namespace BossMod.Shadowbringers.Trial.T01Titania;

public enum OID : uint
{
    Boss = 0x27ED, // R3.600, Titania
    Helper = 0x233C, // R0.500
    Mustardseed = 0x27EE, // R2.400
    Peaseblossom = 0x27EF, // R2.850
    Puck = 0x27F0, // R2.850
    MustardseedGiant = 0x27F2, // R12.000
    PeaseblossomGiant = 0x27F3, // R12.100
    PuckGiant = 0x27F4, // R12.000
    SpiritOfFlame = 0x2878, // R2.000
}

public enum AID : uint
{
    AutoAttack = 872, // Boss/Peaseblossom/Puck->player, no cast, single-target
    FrostRune = 15658, // Boss->self, 3.0s cast, single-target
    GrowthRune = 15662, // Boss->self, 3.0s cast, single-target
    MidsummerNightsDream = 15664, // Boss->self, 4.0s cast, single-target
    BeingMortal = 15666, // Boss->self, 4.0s cast, single-target
    Peasebomb = 15668, // Peaseblossom->self, 5.0s cast, single-target
    LeafstormVisual = 15672, // Mustardseed->self, 2.5s cast, single-target
    LoveInIdleness = 15677, // Boss->self, 4.0s cast, single-target
    LeafstormGiant = 15678, // MustardseedGiant->self, 4.5s cast, single-target
    PeasebombGiant = 15679, // PeaseblossomGiant->self, 5.0s cast, single-target
    PucksRebukeVisual = 15682, // PuckGiant->self, no cast, single-target
    MistRune = 15685, // Boss->self, 3.0s cast, single-target
    FlameRune = 15687, // Boss->self, 3.0s cast, single-target
    FrostRuneAOE = 15694, // Helper->self, 6.0s cast, range 10 circle
    WoodsEmbrace = 15696, // Helper->self, no cast, range 4 width 6 cross
    BeingMortalAOE = 15697, // Helper->self, 12.5s cast, range 60 circle
    Pease = 15698, // Helper->player, 5.0s cast, range 6 circle spread
    HardSwipe = 15699, // Peaseblossom->player, 4.0s cast, single-target tankbuster
    Pummel = 15700, // Puck->player, 4.0s cast, single-target tankbuster
    Leafstorm = 15701, // Helper->self, 3.0s cast, range 50 20-degree cone
    PucksCaprice = 15702, // PuckGiant->self, 4.0s cast, range 50 circle
    PucksBreath = 15703, // PuckGiant->player, 5.0s cast, range 6 circle stack
    PucksRebukeNear = 15704, // Helper->self, 5.0s cast, range 5 circle
    PucksRebuke = 15705, // Helper->self, 5.0s cast, range 60 circle knockback
    DivinationRune = 15707, // Boss->player, 4.0s cast, range 60 cone tankbuster
    BrightSabbath = 15708, // Boss->self, 4.0s cast, range 60 circle
    PhantomRuneIn = 15709, // Boss->self, 5.0s cast, range 10 circle
    PhantomRuneOut = 15710, // Boss->self, 5.0s cast, range 5-60 donut
    WarAndPease = 15789, // Helper->player, 5.0s cast, range 10 circle stack
    LeafstormRepeat = 15875, // Helper->self, 5.0s cast, range 50 20-degree cone
    GentleBreeze = 16259, // Puck->self, 2.5s cast, range 60 width 4 rect
    Uplift = 16927, // Helper->player, 5.0s cast, range 6 circle spread
    FlameHammer = 17267, // SpiritOfFlame->self, no cast, range 6 circle
}

class FrostRuneAOE(BossModule module) : Components.StandardAOEs(module, AID.FrostRuneAOE, 10);
class BeingMortalAOE(BossModule module) : Components.RaidwideCast(module, AID.BeingMortalAOE);
class Pease(BossModule module) : Components.SpreadFromCastTargets(module, AID.Pease, 6);
class HardSwipe(BossModule module) : Components.SingleTargetCast(module, AID.HardSwipe);
class Pummel(BossModule module) : Components.SingleTargetCast(module, AID.Pummel);
class Leafstorm(BossModule module) : Components.StandardAOEs(module, AID.Leafstorm, new AOEShapeCone(50, 10.Degrees()));
class LeafstormRepeat(BossModule module) : Components.StandardAOEs(module, AID.LeafstormRepeat, new AOEShapeCone(50, 10.Degrees()));
class PucksCaprice(BossModule module) : Components.RaidwideCast(module, AID.PucksCaprice);
class PucksBreath(BossModule module) : Components.StackWithCastTargets(module, AID.PucksBreath, 6);
class PucksRebukeNear(BossModule module) : Components.StandardAOEs(module, AID.PucksRebukeNear, 5);
class PucksRebuke(BossModule module) : Components.KnockbackFromCastTarget(module, AID.PucksRebuke, 10);
class DivinationRune(BossModule module) : Components.BaitAwayCast(module, AID.DivinationRune, new AOEShapeCone(60, 45.Degrees()));
class BrightSabbath(BossModule module) : Components.RaidwideCast(module, AID.BrightSabbath);
class PhantomRuneIn(BossModule module) : Components.StandardAOEs(module, AID.PhantomRuneIn, 10);
class PhantomRuneOut(BossModule module) : Components.StandardAOEs(module, AID.PhantomRuneOut, new AOEShapeDonut(5, 60));
class WarAndPease(BossModule module) : Components.StackWithCastTargets(module, AID.WarAndPease, 10);
class GentleBreeze(BossModule module) : Components.StandardAOEs(module, AID.GentleBreeze, new AOEShapeRect(60, 2));
class Uplift(BossModule module) : Components.SpreadFromCastTargets(module, AID.Uplift, 6);
class TitaniaAdds(BossModule module) : Components.AddsMulti(module, [(uint)OID.Mustardseed, (uint)OID.Peaseblossom, (uint)OID.Puck, (uint)OID.MustardseedGiant, (uint)OID.PeaseblossomGiant, (uint)OID.PuckGiant], 1);

class T01TitaniaStates : StateMachineBuilder
{
    public T01TitaniaStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<FrostRuneAOE>()
            .ActivateOnEnter<BeingMortalAOE>()
            .ActivateOnEnter<Pease>()
            .ActivateOnEnter<HardSwipe>()
            .ActivateOnEnter<Pummel>()
            .ActivateOnEnter<Leafstorm>()
            .ActivateOnEnter<LeafstormRepeat>()
            .ActivateOnEnter<PucksCaprice>()
            .ActivateOnEnter<PucksBreath>()
            .ActivateOnEnter<PucksRebukeNear>()
            .ActivateOnEnter<PucksRebuke>()
            .ActivateOnEnter<DivinationRune>()
            .ActivateOnEnter<BrightSabbath>()
            .ActivateOnEnter<PhantomRuneIn>()
            .ActivateOnEnter<PhantomRuneOut>()
            .ActivateOnEnter<WarAndPease>()
            .ActivateOnEnter<GentleBreeze>()
            .ActivateOnEnter<Uplift>()
            .ActivateOnEnter<TitaniaAdds>();
    }
}

[ModuleInfo(Contributors = "Kagekazu", Incomplete = true, GroupType = BossModuleInfo.GroupType.CFC, GroupID = 657, NameID = 8361)]
public class T01Titania(WorldState ws, Actor primary) : BossModule(ws, primary, new(100, 100), new ArenaBoundsCircle(20));
