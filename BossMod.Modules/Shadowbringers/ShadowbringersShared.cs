namespace BossMod.Shadowbringers;

// towers marked by event objects; tracks whichever of those objects currently exist
class ActorTowers(BossModule module, uint oid, float radius, int minSoakers, int maxSoakers) : Components.GenericTowers(module)
{
    public override void Update()
    {
        Towers.Clear();
        foreach (var marker in Module.Enemies(oid))
            if (!marker.IsDestroyed)
                Towers.Add(new(marker.Position, radius, minSoakers, maxSoakers));
    }
}
