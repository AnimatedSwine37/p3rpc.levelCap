using p3rpc.levelCap.Configuration;
using p3rpc.levelCap.Template;
using Reloaded.Hooks.ReloadedII.Interfaces;
using Reloaded.Mod.Interfaces;
using static p3rpc.levelCap.Utils;
using static p3rpc.levelCap.Native;
using Reloaded.Hooks.Definitions;
using IReloadedHooks = Reloaded.Hooks.ReloadedII.Interfaces.IReloadedHooks;

namespace p3rpc.levelCap;
/// <summary>
/// Your mod logic goes here.
/// </summary>
public unsafe class Mod : ModBase // <= Do not Remove.
{
    /// <summary>
    /// Provides access to the mod loader API.
    /// </summary>
    private readonly IModLoader _modLoader;

    /// <summary>
    /// Provides access to the Reloaded.Hooks API.
    /// </summary>
    /// <remarks>This is null if you remove dependency on Reloaded.SharedLib.Hooks in your mod.</remarks>
    private readonly IReloadedHooks? _hooks;

    /// <summary>
    /// Provides access to the Reloaded logger.
    /// </summary>
    private readonly ILogger _logger;

    /// <summary>
    /// Entry point into the mod, instance that created this class.
    /// </summary>
    private readonly IMod _owner;

    /// <summary>
    /// Provides access to this mod's configuration.
    /// </summary>
    private Config _configuration;

    /// <summary>
    /// The configuration of the currently executing mod.
    /// </summary>
    private readonly IModConfig _modConfig;

    private IHook<SetupPartyExpDelegate> _setupPartyExpHook;

    public Mod(ModContext context)
    {
        _modLoader = context.ModLoader;
        _hooks = context.Hooks;
        _logger = context.Logger;
        _owner = context.Owner;
        _configuration = context.Configuration;
        _modConfig = context.ModConfig;

        Utils.Initialise(_logger, _configuration, _modLoader);
        Native.Initialise(_hooks!);

        SigScan("48 8B C4 53 55 48 81 EC 88 00 00 00", "SetupPartyExp", address =>
        {
            _setupPartyExpHook = _hooks.CreateHook<SetupPartyExpDelegate>(SetupPartyExp, address).Activate();
        });
    }

    private void SetupPartyExp(ABtlPhaseResult* result)
    {
        _setupPartyExpHook.OriginalFunction(result);

        for (int i = 0; i < result->NumInParty; i++)
        {
            var member = (PartyMember)result->Party[i];
            var unit = GetAllyUnitStatus(member);

            // TODO cap based on current date
            var cap = GetCap(GlobalWork->Date);
            if (unit->Level >= cap)
            {
                Log($"Capping exp for {member}");
                result->EarnedExp[(int)member] = 0;
            }
        }
    }

    private int GetCap(int date)
    {
        if (date <= 38) // Priestess (5/9)
            return _configuration.Cap1;
        else if (date <= 68) // Empress + Emperor (6/8)
            return _configuration.Cap2;
        else if (date <= 97) // Hierophant + Lovers (7/7)
            return _configuration.Cap3;
        else if (date <= 127) // Chariot + Justice (8/6)
            return _configuration.Cap4;
        else if (date <= 157) // Hermit (9/5)
            return _configuration.Cap5;
        else if (date <= 186) // Fortune + Strength (10/4)
            return _configuration.Cap6;
        else if (date <= 216) // Hanged Man (11/3)
            return _configuration.Cap7;
        else if (date <= 245) // 12/2
            return _configuration.Cap8;
        else if (date <= 274) // 12/31
            return _configuration.Cap9;
        else if (date <= 305) // Nyx (1/31)
            return _configuration.Cap10;
        else
            return 99;
    }

    #region Standard Overrides
    public override void ConfigurationUpdated(Config configuration)
    {
        // Apply settings from configuration.
        // ... your code here.
        _configuration = configuration;
        _logger.WriteLine($"[{_modConfig.ModId}] Config Updated: Applying");
    }
    #endregion

    #region For Exports, Serialization etc.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public Mod() { }
#pragma warning restore CS8618
    #endregion
}