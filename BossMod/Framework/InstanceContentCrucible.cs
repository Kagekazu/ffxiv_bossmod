using System.Runtime.InteropServices;

namespace FFXIVClientStructs.FFXIV.Client.Game.InstanceContent;

// Local until FFXIVClientStructs publishes InstanceContentCrucible.
// Client::Game::InstanceContent::InstanceContentCrucible : InstanceContentDirector
[StructLayout(LayoutKind.Explicit, Size = 0x25A0)]
public unsafe struct InstanceContentCrucible
{
    public const int InventoryCount = 10;

    // InstanceContentDirector.InstanceContentType
    [FieldOffset(0xDCE)] public InstanceContentType InstanceContentType;

    [FieldOffset(0x2380)] public InventorySlot Inventory;

    [StructLayout(LayoutKind.Explicit, Size = 12)]
    public struct InventorySlot
    {
        [FieldOffset(4)] public ushort ItemId;
    }
}
