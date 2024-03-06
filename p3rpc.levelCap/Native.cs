using Reloaded.Hooks.ReloadedII.Interfaces;
using System.Runtime.InteropServices;

namespace p3rpc.levelCap;
internal unsafe class Native
{
    private static GetUGlobalWorkDelegate _getUGlobalWork;
    internal static GetAllyUnitStatusDelegate GetAllyUnitStatus;
    internal static UGlobalWork* GlobalWork => _getUGlobalWork();

    internal static void Initialise(IReloadedHooks hooks)
    {
        Utils.SigScan("40 53 48 83 EC 20 0F B7 D9 E8 ?? ?? ?? ?? 48 85 C0 75 ?? 48 83 C4 20 5B C3 0F B7 D3 48 89 C1", "GetAllyUnitStatus", address =>
        {
            GetAllyUnitStatus = hooks.CreateWrapper<GetAllyUnitStatusDelegate>(address, out _);
        });

        Utils.SigScan("48 89 5C 24 ?? 57 48 83 EC 20 48 8B 0D ?? ?? ?? ?? 33 DB", "GetUGlobalWork", address =>
        {
            _getUGlobalWork = hooks.CreateWrapper<GetUGlobalWorkDelegate>(address, out _);
        });
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct UGlobalWork
    {
        [FieldOffset(0xa378)]
        internal int Date;
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct ABtlPhaseResult
    {
        [FieldOffset(0x2c6)]
        internal short NumInParty;

        [FieldOffset(0x2b2)]
        internal fixed short Party[10];

        [FieldOffset(0x2cc)]
        internal fixed int EarnedExp[11];
    }

    [StructLayout(LayoutKind.Explicit)]
    internal struct AllyUnitStatus
    {
        [FieldOffset(4)]
        internal byte Level;

        [FieldOffset(8)]
        internal uint Exp;
    }

    internal enum PartyMember
    {
        None,
        Hero,
        Yukari,
        Junpei,
        Akihiko,
        Mitsuru,
        Fuuka,
        Aigis,
        Ken,
        Koromaru,
        Shinjiro
    }

    internal delegate void SetupPartyExpDelegate(ABtlPhaseResult* result);
    internal delegate AllyUnitStatus* GetAllyUnitStatusDelegate(PartyMember member);
    internal delegate UGlobalWork* GetUGlobalWorkDelegate();
}
