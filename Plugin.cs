using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;

namespace ExtRaftagant
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        static ManualLogSource logger;
        static ConfigEntry<int> maxSize;

        private void Awake()
        {
            maxSize = Config.Bind("General", "Max Raft size", 20, "Max width/depth of raft (Vanilla: 5)");

            // Plugin startup logic
            logger = Logger;
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded");
            Logger.LogInfo($"Patching...");
            Harmony.CreateAndPatchAll(typeof(Plugin));
            Logger.LogInfo($"Patched");
        }

        [HarmonyPatch(typeof(RaftOrigin), "InitLists")]
        [HarmonyPrefix]
        static void InitLists_prefix(ref int ___raftBasesSize, ref int ___raftBaseFullSize)
        {
            ___raftBasesSize = maxSize.Value * 2;
            ___raftBaseFullSize = maxSize.Value;
        }
    }
}
