namespace BossMod.Stormblood.Trial.T01Susano;

public enum OID : uint
{
    Boss = 0x1AF7,
    Helper = 0x233C,
    AmaNoIwato = 0x1BA1, // R0.500, x?, mixed types
    SusanoBig = 0x1AF8, // R0.000, x?
    _Gen_Actor1e8536 = 0x1E8536, // R2.000, x?, EventObj type
    _Gen_Actor1ea478 = 0x1EA478, // R2.000, x?, EventObj type
    BladesShadow = 0x1EA479, // R2.000, x?, EventObj type
    _Gen_Exit = 0x1E850B, // R0.500, x?, EventObj type
    DarkCloud = 0x1F53, // R3.000, x?
    _Gen_2 = 0x1BA2, // R1.000, x? : Crystal flower item that starts the clash possibly.
    AmeNoMurakumo = 0x1C84, // R8.000, x?, Part type
    DarkLevin = 0x1B9B, // R1.000, x? : Lightning orbs
    AmaNoIwato1 = 0x1C20, // R1.800, x? Stones
}

public enum AID : uint
{
    _AutoAttack_Attack = 870, // 1AF7->player, no cast, single-target
    Assail = 8220, // 1AF7->player, no cast, single-target
    RasenKaikyo = 8222, // 1BA1->self, 3.0s cast, range 6 circle
    RasenKaikyoVisual = 8221, // 1AF7->self, 3.0s cast, single-target
    YataNoKagami = 8223, // 1AF7->player, no cast, single-target
    Brightstorm = 8224, // 1AF7->players, no cast, range 6 circle
    YasakaniNoMagatamaVisual = 9633, // 1AF7->self, no cast, single-target : Summons the giant Susano
    ThePartingClouds = 9631, // 1F53->self, 3.5s cast, range 50+R width 10 rect
    SeasplitterVisual = 9661, // 1BA1->self, 2.9s cast, single-target
    Seasplitter = 8232, // 1BA1->self, 3.0s cast, range 21+R width 40 rect
    Seasplitter1 = 8233, // 1BA1->self, no cast, range 7+R width 40 rect
    Seasplitter2 = 8234, // 1BA1->self, no cast, range 7+R width 40 rect
    Seasplitter3 = 8235, // 1BA1->self, no cast, range 7+R width 40 rect
    _Weaponskill_ = 8646, // 1AF8->self, no cast, single-target
    SheerForce = 8225, // 1BA1->self, no cast, range 40+R circle
    Shock = 8259, // 1B9B->self, no cast, range 6 circle
    AmeNoMurakumoRaidwide = 8226, // 1AF8->self, no cast, range 40+R circle
    AmeNoMurakumoRectAOE = 8588, // 1BA1->self, 4.0s cast, range 40+R width 6 rect
    Stormsplitter = 8227, // 1AF7->self/player, 5.0s cast, range 20+R width 4 rect
    _Ability_TheHiddenGate = 8228, // 1AF7->self, no cast, single-target
    _Ability_TheAlteredGate = 8333, // 1BA1->1C20, no cast, ???
    TheSealedGate = 8229, // 1C20->self, 15.0s cast, single-target
    Ukehi = 8230, // 1AF7->self, 4.0s cast, range 40+R circle
    _Weaponskill_Ukehi1 = 8231, // 1AF7->self, no cast, range 40+R circle
    AmeNoMurakumoWipe = 9506, // 1C84->self, 24.0s cast, single-target - Enrage cast for not beating big sword
}

public enum IconID : uint
{
    _Gen_Icon_lockon5_t0h = 23, // player->self : Knockback icon?
    _Gen_Icon_com_share0c = 62, // player->self
    StormsplitterIcon = 230, // player->self : Caution Tankbuster: After icon is cast it is followed up with Spell 8227 'Stormsplitter'
    _Gen_Icon_m0372trg_t2j = 112, // AmaNoIwato1->self : 1C20/Ama-no-iwato
}

public enum SID : uint
{
    _Gen_LightningResistanceDown = 898,
    _Gen_Paralysis = 216,
    _Gen_Clashing = 1271,
    _Gen_FleshWound = 624,
    _Gen_Fetters = 292,
}

public enum TetherID : uint
{
    _Gen_Tether_chn_m0372_01j = 66,
}

class RasenKaikyo(BossModule module) : Components.StandardAOEs(module, AID.RasenKaikyo, 6);

class T01SusanoStates : StateMachineBuilder
{
    public T01SusanoStates(BossModule module) : base(module)
    {
        TrivialPhase()
            .ActivateOnEnter<RasenKaikyo>();
    }
}

[ModuleInfo(Contributors = "Kagekazu", Incomplete = true, GroupType = BossModuleInfo.GroupType.CFC, GroupID = 243, NameID = 6221)]
public class T01Susano(WorldState ws, Actor primary) : BossModule(ws, primary, new(0, 0), new ArenaBoundsCircle(20));
