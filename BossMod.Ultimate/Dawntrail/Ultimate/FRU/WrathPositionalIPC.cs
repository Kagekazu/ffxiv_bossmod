using Dalamud.Plugin.Ipc;

namespace BossMod.Dawntrail.Ultimate.FRU;

// WrathCombo upcoming-positional hints (same gates Avarice consumes). Event-driven cache; no per-frame IPC.
static class WrathPositionalIPC
{
    private const string HintGate = "WrathCombo.GetUpcomingPositionalHint";
    private const string HintChangedGate = "OnUpcomingPositionalHint";
    private const int FieldCount = 7;

    private static ICallGateSubscriber<uint[]>? _get;
    private static ICallGateSubscriber<object>? _changed;
    private static bool _initAttempted;
    private static Hint _hint;
    private static readonly Action OnChanged = Refresh;

    public readonly record struct Hint(Positional Pos, uint ActionId, int GcdsUntil, ulong TargetObjectId, long ExpiresAtTick, bool IsSatisfied)
    {
        public bool IsActive(long now) =>
            Pos is Positional.Rear or Positional.Flank && ActionId != 0 && GcdsUntil > 0 && ExpiresAtTick > now;
    }

    public static Hint? Current
    {
        get
        {
            EnsureInit();
            var now = Environment.TickCount64;
            if (!_hint.IsActive(now))
                return null;
            return _hint;
        }
    }

    private static void EnsureInit()
    {
        if (_initAttempted || Service.IsMock || Service.PluginInterface == null)
            return;
        _initAttempted = true;
        try
        {
            _get = Service.PluginInterface.GetIpcSubscriber<uint[]>(HintGate);
            _changed = Service.PluginInterface.GetIpcSubscriber<object>(HintChangedGate);
            _changed.Subscribe(OnChanged);
            Refresh();
        }
        catch (Exception e)
        {
            Service.Log($"[FRUAI] Wrath positional IPC unavailable: {e.Message}");
            _get = null;
            _changed = null;
        }
    }

    private static void Refresh()
    {
        if (_get == null)
            return;
        try
        {
            var wire = _get.InvokeFunc();
            _hint = TryParse(wire, out var h) ? h : default;
        }
        catch
        {
            _hint = default;
        }
    }

    private static bool TryParse(uint[]? wire, out Hint hint)
    {
        hint = default;
        if (wire == null || wire.Length < FieldCount)
            return false;

        var pos = wire[0] switch
        {
            1 => Positional.Rear,
            2 => Positional.Flank,
            _ => Positional.Any
        };
        var expiresInMs = (int)wire[4];
        if (pos == Positional.Any || wire[1] == 0 || wire[2] == 0 || expiresInMs <= 0)
            return false;

        hint = new(pos, wire[1], (int)wire[2], wire[3], Environment.TickCount64 + expiresInMs, wire[6] != 0);
        return true;
    }
}
