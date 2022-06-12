﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace BossMod.Endwalker.P4S2
{
    using static BossModule;

    // state related to hell's sting mechanic (part of curtain call sequence)
    class HellsSting : Component
    {
        public int NumCasts { get; private set; } = 0;

        private AOEShapeCone _cone = new(50, Angle.Radians(MathF.PI / 12));
        private List<Angle> _directions = new();

        public override void AddHints(BossModule module, int slot, Actor actor, TextHints hints, MovementHints? movementHints)
        {
            if (NumCasts >= _directions.Count * 2)
                return;

            if (ConeDirections().Any(x => actor.Position.InCone(module.PrimaryActor.Position, x, _cone.HalfAngle)))
                hints.Add("GTFO from cone!");
        }

        public override void DrawArenaBackground(BossModule module, int pcSlot, Actor pc, MiniArena arena)
        {
            if (NumCasts >= _directions.Count * 2)
                return;

            foreach (var dir in ConeDirections())
                _cone.Draw(arena, module.PrimaryActor.Position, dir);
        }

        public override void OnCastStarted(BossModule module, Actor actor)
        {
            if (actor.CastInfo!.IsSpell(AID.HellsStingAOE1))
                _directions.Add(actor.Rotation);
        }

        public override void OnCastFinished(BossModule module, Actor actor)
        {
            if (actor.CastInfo!.IsSpell(AID.HellsStingAOE1) || actor.CastInfo!.IsSpell(AID.HellsStingAOE2))
                ++NumCasts;
        }

        private IEnumerable<Angle> ConeDirections()
        {
            return NumCasts < _directions.Count ? _directions : _directions.Select(x => x + Angle.Radians(MathF.PI / 8));
        }
    }
}
