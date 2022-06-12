﻿using System.Numerics;

namespace BossMod.Endwalker.P2S
{
    using static BossModule;

    // state related to sewage deluge mechanic
    class SewageDeluge : Component
    {
        public enum Corner { None, NW, NE, SW, SE };

        private Corner _blockedCorner = Corner.None;

        private static float _offsetCorner = 9.5f; // not sure
        private static float _cornerHalfSize = 4; // not sure
        private static float _connectHalfWidth = 2; // not sure
        private static float _cornerInner = _offsetCorner - _cornerHalfSize;
        private static float _cornerOuter = _offsetCorner + _cornerHalfSize;
        private static float _connectInner = _offsetCorner - _connectHalfWidth;
        private static float _connectOuter = _offsetCorner + _connectHalfWidth;

        private static WDir[] _corners = { new(), new(-1, -1), new(1, -1), new(-1, 1), new(1, 1) };

        public override void DrawArenaBackground(BossModule module, int pcSlot, Actor pc, MiniArena arena)
        {
            if (_blockedCorner == Corner.None)
                return;

            // central area + H additionals
            arena.ZoneQuad(arena.WorldCenter, new WDir( 1, 0), _connectInner, _connectInner, _cornerInner, arena.ColorAOE);
            // central V additionals
            arena.ZoneQuad(arena.WorldCenter, new WDir(0,  1), _connectInner, -_cornerInner, _cornerInner, arena.ColorAOE);
            arena.ZoneQuad(arena.WorldCenter, new WDir(0, -1), _connectInner, -_cornerInner, _cornerInner, arena.ColorAOE);
            // outer additionals
            arena.ZoneQuad(arena.WorldCenter, new WDir( 1, 0), _cornerOuter, -_connectOuter, _cornerInner, arena.ColorAOE);
            arena.ZoneQuad(arena.WorldCenter, new WDir(-1, 0), _cornerOuter, -_connectOuter, _cornerInner, arena.ColorAOE);
            arena.ZoneQuad(arena.WorldCenter, new WDir(0,  1), _cornerOuter, -_connectOuter, _cornerInner, arena.ColorAOE);
            arena.ZoneQuad(arena.WorldCenter, new WDir(0, -1), _cornerOuter, -_connectOuter, _cornerInner, arena.ColorAOE);
            // outer area
            arena.ZoneQuad(arena.WorldCenter, new WDir( 1, 0), arena.WorldHalfSize, -_cornerOuter, arena.WorldHalfSize, arena.ColorAOE);
            arena.ZoneQuad(arena.WorldCenter, new WDir(-1, 0), arena.WorldHalfSize, -_cornerOuter, arena.WorldHalfSize, arena.ColorAOE);
            arena.ZoneQuad(arena.WorldCenter, new WDir(0,  1), arena.WorldHalfSize, -_cornerOuter, _cornerOuter, arena.ColorAOE);
            arena.ZoneQuad(arena.WorldCenter, new WDir(0, -1), arena.WorldHalfSize, -_cornerOuter, _cornerOuter, arena.ColorAOE);

            var corner = arena.WorldCenter + _corners[(int)_blockedCorner] * _offsetCorner;
            arena.ZoneQuad(corner, new WDir(1, 0), _cornerHalfSize, _cornerHalfSize, _cornerHalfSize, arena.ColorAOE);
        }

        public override void DrawArenaForeground(BossModule module, int pcSlot, Actor pc, MiniArena arena)
        {
            // inner border
            arena.PathLineTo(arena.WorldCenter + new WDir(-_cornerInner, -_cornerInner));
            arena.PathLineTo(arena.WorldCenter + new WDir(-_cornerInner, -_connectInner));
            arena.PathLineTo(arena.WorldCenter + new WDir(+_cornerInner, -_connectInner));
            arena.PathLineTo(arena.WorldCenter + new WDir(+_cornerInner, -_cornerInner));
            arena.PathLineTo(arena.WorldCenter + new WDir(+_connectInner, -_cornerInner));
            arena.PathLineTo(arena.WorldCenter + new WDir(+_connectInner, +_cornerInner));
            arena.PathLineTo(arena.WorldCenter + new WDir(+_cornerInner, +_cornerInner));
            arena.PathLineTo(arena.WorldCenter + new WDir(+_cornerInner, +_connectInner));
            arena.PathLineTo(arena.WorldCenter + new WDir(-_cornerInner, +_connectInner));
            arena.PathLineTo(arena.WorldCenter + new WDir(-_cornerInner, +_cornerInner));
            arena.PathLineTo(arena.WorldCenter + new WDir(-_connectInner, +_cornerInner));
            arena.PathLineTo(arena.WorldCenter + new WDir(-_connectInner, -_cornerInner));
            arena.PathStroke(true, arena.ColorBorder);

            // outer border
            arena.PathLineTo(arena.WorldCenter + new WDir(-_cornerOuter, -_cornerOuter));
            arena.PathLineTo(arena.WorldCenter + new WDir(-_cornerInner, -_cornerOuter));
            arena.PathLineTo(arena.WorldCenter + new WDir(-_cornerInner, -_connectOuter));
            arena.PathLineTo(arena.WorldCenter + new WDir(+_cornerInner, -_connectOuter));
            arena.PathLineTo(arena.WorldCenter + new WDir(+_cornerInner, -_cornerOuter));
            arena.PathLineTo(arena.WorldCenter + new WDir(+_cornerOuter, -_cornerOuter));
            arena.PathLineTo(arena.WorldCenter + new WDir(+_cornerOuter, -_cornerInner));
            arena.PathLineTo(arena.WorldCenter + new WDir(+_connectOuter, -_cornerInner));
            arena.PathLineTo(arena.WorldCenter + new WDir(+_connectOuter, +_cornerInner));
            arena.PathLineTo(arena.WorldCenter + new WDir(+_cornerOuter, +_cornerInner));
            arena.PathLineTo(arena.WorldCenter + new WDir(+_cornerOuter, +_cornerOuter));
            arena.PathLineTo(arena.WorldCenter + new WDir(+_cornerInner, +_cornerOuter));
            arena.PathLineTo(arena.WorldCenter + new WDir(+_cornerInner, +_connectOuter));
            arena.PathLineTo(arena.WorldCenter + new WDir(-_cornerInner, +_connectOuter));
            arena.PathLineTo(arena.WorldCenter + new WDir(-_cornerInner, +_cornerOuter));
            arena.PathLineTo(arena.WorldCenter + new WDir(-_cornerOuter, +_cornerOuter));
            arena.PathLineTo(arena.WorldCenter + new WDir(-_cornerOuter, +_cornerInner));
            arena.PathLineTo(arena.WorldCenter + new WDir(-_connectOuter, +_cornerInner));
            arena.PathLineTo(arena.WorldCenter + new WDir(-_connectOuter, -_cornerInner));
            arena.PathLineTo(arena.WorldCenter + new WDir(-_cornerOuter, -_cornerInner));
            arena.PathStroke(true, arena.ColorBorder);
        }

        public override void OnEventEnvControl(BossModule module, uint featureID, byte index, uint state)
        {
            // 800375A2: we typically get two events for index=0 (global) and index=N (corner)
            // state 00200010 - "prepare" (show aoe that is still harmless)
            // state 00020001 - "active" (dot in center/borders, oneshot in corner)
            // state 00080004 - "finish" (reset)
            if (featureID == 0x800375A2 && index > 0 && index < 5)
            {
                if (state == 0x00200010)
                    _blockedCorner = (Corner)index;
                else if (state == 0x00080004)
                    _blockedCorner = Corner.None;
            }
        }
    }
}
