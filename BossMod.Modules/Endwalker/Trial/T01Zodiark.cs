namespace BossMod.Endwalker.Trial.T01Zodiark;

public enum OID : uint
{
    Boss = 0x324C, // Teamcraft index
    Helper = 0x233C, // Helper type
}

public enum AID : uint
{
    Kokytos1 = 27687, // Zodiark, cactbot ~11.1s
    Exoterikos1 = 27702, // Zodiark, cactbot ~20.2s
    EsotericSect = 26572, // Arcane Sigil, cactbot ~27.5s
    Ania1 = 26581, // Zodiark, cactbot ~31.5s
    EsotericDyad = 26571, // Arcane Sigil, cactbot ~48.9s
    Paradeigma1 = 26568, // Zodiark, cactbot ~58.3s
    MeteorosEidolon = 26566, // Behemoth, cactbot ~71.0s
    OpheosEidolon = 26567, // Python/Behemoth/Zodiark, cactbot ~88.3s
    Phlegethon1 = 26576, // Zodiark, cactbot ~94.9s
    Styx1 = 26587, // Zodiark, cactbot ~103.1s
    CompleteControl = 26420, // Zodiark, cactbot ~149.6s
    Paradeigma2 = 26559, // Zodiark, cactbot ~179.4s
    AstralFlow1 = 28361, // Zodiark, cactbot ~194.5s
    Adikia = 26584, // Zodiark, cactbot ~215.0s
    Phlegethon2 = 26575, // Zodiark, cactbot ~226.2s
    TripleEsotericRay = 26564, // Zodiark, cactbot ~235.3s
    EsotericRay1 = 26570, // Arcane Sigil, cactbot ~235.4s
    AstralFlow2 = 26160, // Zodiark, cactbot ~268.7s
    AstralEclipse = 26563, // Zodiark, cactbot ~285.9s
    Explosion1 = 26573, // Zodiark, cactbot ~303.0s
    Styx2 = 26586, // Zodiark, cactbot ~318.1s
    Kokytos2 = 27743, // Zodiark, cactbot ~333.2s
    AstralFlow3 = 26159, // Zodiark, cactbot ~353.4s
    Exoterikos2 = 26561, // Zodiark, cactbot ~378.7s
    TrimorphosExoterikos = 26562, // Zodiark, cactbot ~391.0s
    Ania2 = 26580, // Zodiark, cactbot ~472.8s
    Ania3 = 27490, // cast, cactbot trigger
    AlgedonNW = 26578, // cast, cactbot trigger
    AlgedonNE = 26577, // cast, cactbot trigger
}

class T01ZodiarkStates : StateMachineBuilder
{
    public T01ZodiarkStates(BossModule module) : base(module)
    {
        // TODO: stub from cactbot IDs + Teamcraft OIDs; components and states from the first replay
        TrivialPhase();
    }
}

[ModuleInfo(Incomplete = true, Contributors = "Kagekazu", GroupType = BossModuleInfo.GroupType.CFC, GroupID = 802, NameID = 10456)]
public class T01Zodiark(ModuleInit init) : BossModule(init, new(100, 100), new ArenaBoundsSquare(20));
