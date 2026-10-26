namespace BossMod.Endwalker.Raid.P2NHippokampos;

public enum OID : uint
{
    Boss = 0x3599, // Teamcraft index
    Hippokampos2 = 0x359A, // Teamcraft index
    Helper = 0x233C, // Helper type
}

public enum AID : uint
{
    MurkyDepths = 26639, // Hippokampos, cast, cactbot ~14.3s
    DoubledImpact = 26638, // Hippokampos, cast, cactbot ~26.6s
    SpokenCataract1 = 26615, // Hippokampos, cast, cactbot ~43.6s
    SpokenCataract2 = 26616, // Hippokampos, cast, cactbot ~43.6s
    SpokenCataract3 = 26617, // Hippokampos, cast, cactbot ~43.6s
    SpokenCataract4 = 26621, // Hippokampos, cactbot ~43.6s
    SewageDeluge = 26614, // Hippokampos, cast, cactbot ~84.6s
    TaintedFlood = 26633, // Hippokampos, cast, cactbot ~111.0s
    PredatorySight1 = 26635, // Hippokampos, cactbot ~126.9s
    Shockwave = 26631, // Hippokampos, cast, cactbot ~137.3s
    Dissociation = 26628, // Hippokampos, cactbot ~148.6s
    DissociationDive = 26630, // Hippokampos, cast, cactbot ~165.8s
    CoherenceFlare = 26624, // Hippokampos, cactbot ~178.2s
    CoherenceLine = 26626, // Hippokampos, cactbot ~180.7s
    PredatorySight2 = 26634, // cast, cactbot trigger
}

class P2NHippokamposStates : StateMachineBuilder
{
    public P2NHippokamposStates(BossModule module) : base(module)
    {
        // TODO: stub from cactbot IDs + Teamcraft OIDs; components and states from the first replay
        TrivialPhase();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 810, NameID = 10348)]
public class P2NHippokampos(ModuleInit init) : BossModule(init, new(100, 100), new ArenaBoundsSquare(20));
