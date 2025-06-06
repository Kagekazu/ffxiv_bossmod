﻿namespace BossMod.Endwalker.Ultimate.DSW2;

// used by two trio mechanics, in p2 and in p5
class DragonsGaze(BossModule module, OID bossOID, float activationDelay) : Components.GenericGaze(module, AID.DragonsGazeAOE)
{
    public bool EnableHints;
    private readonly OID _bossOID = bossOID;
    private Actor? _boss;
    private WPos _eyePosition;
    private DateTime _activation;

    public bool Active => _boss != null;

    public override IEnumerable<Eye> ActiveEyes(int slot, Actor actor)
    {
        if (_boss != null && NumCasts == 0)
        {
            yield return new(_eyePosition, _activation, Range: EnableHints ? 10000 : 0);
            yield return new(_boss.Position, _activation, Range: EnableHints ? 10000 : 0);
        }
    }

    public override void OnEventEnvControl(byte index, uint state)
    {
        // seen indices: 2 = E, 5 = SW, 6 = W => inferring 0=N, 1=NE, ... cw order
        if (state == 0x00020001 && index <= 7)
        {
            if (_activation == default)
                _activation = WorldState.FutureTime(activationDelay);
            _boss = Module.Enemies(_bossOID).FirstOrDefault();
            _eyePosition = Module.Center + 40 * (180 - index * 45).Degrees().ToDirection();
        }
    }
}
