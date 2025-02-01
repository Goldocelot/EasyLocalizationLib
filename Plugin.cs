using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace EasyLocalizationLib;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    internal static new ManualLogSource Logger;
    public static Plugin instance;

    private void Awake()
    {
        instance = this;

        Logger = base.Logger;
        Logger.LogInfo($"Loaded!");

        Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
        harmony.PatchAll();

        Logger.LogInfo($"Patch applied!");

        EasyLocalizationManager.GetInstance().AddPluginLocalization(this, LANG.EN);
    }
}