using System;
using System.Linq;
using BepInEx;
using HarmonyLib;

namespace VampireTweaks
{
    internal static class ModInfo
    {
        internal const string Guid = "omegaplatinum.elin.vampiretweaks";
        internal const string Name = "Vampire Tweaks";
        internal const string Version = "1.0.1";
        internal const string ModOptionsGuid = "evilmask.elinplugins.modoptions";
        internal const string ModOptionsAssemblyName = "ModOptions";
    }

    [BepInPlugin(GUID: ModInfo.Guid, Name: ModInfo.Name, Version: ModInfo.Version)]
    internal class VampireTweaks : BaseUnityPlugin
    {
        internal static VampireTweaks Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            VampireTweaksConfig.LoadConfig(config: Config);
            Harmony.CreateAndPatchAll(type: typeof(Patcher), harmonyInstanceId: ModInfo.Guid);

            if (IsModOptionsInstalled())
            {
                try
                {
                    UIController.RegisterUI();
                }
                catch (Exception ex)
                {
                    Log(payload: $"An error occurred during UI registration: {ex.Message}");
                }
            }
            else
            {
                Log(payload: "Mod Options is not installed. Skipping UI registration.");
            }
        }

        internal static void Log(object payload)
        {
            Instance?.Logger.LogInfo(data: payload);
        }

        private bool IsModOptionsInstalled()
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .Any(predicate: assembly => assembly.GetName().Name == ModInfo.ModOptionsAssemblyName);
        }
    }
}