namespace BossMod.AI;

[ConfigDisplay(Name = "AI Configuration", Order = 9, Group = "AI & Automation")]
public sealed class AIConfig : ConfigNode
{
    [PropertyDisplay("Enable")]
    public bool Enabled = false;

    // ui settings
    [PropertyDisplay("Show config window")]
    public bool DrawUI = true;

    [PropertyDisplay("Focus target master")]
    public bool FocusTargetMaster = false;

    [PropertyDisplay("Distance to master")]
    [PropertySlider(3, 25)]
    public float DistanceToMaster = 25f;

    [PropertyDisplay("Follow master even if a boss module is active")]
    public bool FollowDuringBoss = false;

    [PropertyDisplay("Disable movement")]
    public bool ForbidMovement = false;

    [PropertyDisplay("Disable auto-target")]
    public bool ForbidActions = false;

    [PropertyDisplay("Move towards current target", since: "7.5.1.21")]
    public bool GoalZoneFallback = true;

    [PropertyDisplay("Print AI status changes to chat", since: "7.5.1.36")]
    public bool ChatConfirmation = false;

    [PropertyDisplay("Extra time to leave dangerous areas (seconds)", tooltip: "Added on top of ping + 0.6s before an AOE goes off.\nOlder versions used a flat 1s instead; raise this (e.g. 0.4) if it gets clipped by AOEs it was trying to dodge.", since: "7.5.7.4")]
    [PropertySlider(0, 1)]
    public float ExtraDodgeTime = 0;

    [PropertyDisplay("Use legacy forbidden zone rasterizer", tooltip: "Debug option: use the pathfinding rasterizer from before the August 2026 rewrite, which checks every grid point instead of skipping by distance. Slower; only meant for comparing AI behaviour.", since: "7.5.7.4")]
    public bool LegacyRasterizer = false;

    // defined here for DtrConfig migration
    public bool ShowDTR = true;
}
