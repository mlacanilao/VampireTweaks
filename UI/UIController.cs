using System.IO;
using System.Reflection;
using BepInEx;
using EvilMask.Elin.ModOptions;
using EvilMask.Elin.ModOptions.UI;

namespace VampireTweaks
{
    public class UIController
    {
        public static void RegisterUI()
        {
            foreach (var obj in ModManager.ListPluginObject)
            {
                if (obj is BaseUnityPlugin plugin && plugin.Info.Metadata.GUID == ModInfo.ModOptionsGuid)
                {
                    var controller = ModOptionController.Register(guid: ModInfo.Guid, tooptipId: "mod.tooltip");
                    
                    var assemblyLocation = Path.GetDirectoryName(path: Assembly.GetExecutingAssembly().Location);
                    var xmlPath = Path.Combine(path1: assemblyLocation, path2: "VampireTweaksConfig.xml");
                    VampireTweaksConfig.InitializeXmlPath(xmlPath: xmlPath);
            
                    var xlsxPath = Path.Combine(path1: assemblyLocation, path2: "translations.xlsx");
                    VampireTweaksConfig.InitializeTranslationXlsxPath(xlsxPath: xlsxPath);
                    
                    if (File.Exists(path: VampireTweaksConfig.XmlPath))
                    {
                        using (StreamReader sr = new StreamReader(path: VampireTweaksConfig.XmlPath))
                            controller.SetPreBuildWithXml(xml: sr.ReadToEnd());
                    }
                    
                    if (File.Exists(path: VampireTweaksConfig.TranslationXlsxPath))
                    {
                        controller.SetTranslationsFromXslx(path: VampireTweaksConfig.TranslationXlsxPath);
                    }
                    
                    RegisterEvents(controller: controller);
                }
            }
        }

        private static void RegisterEvents(ModOptionController controller)
        {
            controller.OnBuildUI += builder =>
            {
                var topic01 = builder.GetPreBuild<OptTopic>(id: "topic01");
                topic01.Text = "general".lang();
                
                var enableNonHostileBloodSuckToggle = builder.GetPreBuild<OptToggle>(id: "enableNonHostileBloodSuckToggle");
                enableNonHostileBloodSuckToggle.Checked = VampireTweaksConfig.EnableNonHostileBloodSuck.Value;
                enableNonHostileBloodSuckToggle.OnValueChanged += isChecked =>
                {
                    VampireTweaksConfig.EnableNonHostileBloodSuck.Value = isChecked;
                };
                
                var enableUniversalBloodSuckToggle = builder.GetPreBuild<OptToggle>(id: "enableUniversalBloodSuckToggle");
                enableUniversalBloodSuckToggle.Checked = VampireTweaksConfig.EnableUniversalBloodSuck.Value;
                enableUniversalBloodSuckToggle.OnValueChanged += isChecked =>
                {
                    VampireTweaksConfig.EnableUniversalBloodSuck.Value = isChecked;
                };
                
                var enableNoBloodSuckCrimeWitnessToggle = builder.GetPreBuild<OptToggle>(id: "enableNoBloodSuckCrimeWitnessToggle");
                enableNoBloodSuckCrimeWitnessToggle.Checked = VampireTweaksConfig.EnableNoBloodSuckCrimeWitness.Value;
                enableNoBloodSuckCrimeWitnessToggle.OnValueChanged += isChecked =>
                {
                    VampireTweaksConfig.EnableNoBloodSuckCrimeWitness.Value = isChecked;
                };
                
                var enableNoBloodSuckVictimDebuffsToggle = builder.GetPreBuild<OptToggle>(id: "enableNoBloodSuckVictimDebuffsToggle");
                enableNoBloodSuckVictimDebuffsToggle.Checked = VampireTweaksConfig.EnableNoBloodSuckVictimDebuffs.Value;
                enableNoBloodSuckVictimDebuffsToggle.OnValueChanged += isChecked =>
                {
                    VampireTweaksConfig.EnableNoBloodSuckVictimDebuffs.Value = isChecked;
                };
                
                var enableNoWeakToSunlightToggle = builder.GetPreBuild<OptToggle>(id: "enableNoWeakToSunlightToggle");
                enableNoWeakToSunlightToggle.Checked = VampireTweaksConfig.EnableNoWeakToSunlight.Value;
                enableNoWeakToSunlightToggle.OnValueChanged += isChecked =>
                {
                    VampireTweaksConfig.EnableNoWeakToSunlight.Value = isChecked;
                };
                
                var enableNoBatTransformCooldownToggle = builder.GetPreBuild<OptToggle>(id: "enableNoBatTransformCooldownToggle");
                enableNoBatTransformCooldownToggle.Checked = VampireTweaksConfig.EnableNoBatTransformCooldown.Value;
                enableNoBatTransformCooldownToggle.OnValueChanged += isChecked =>
                {
                    VampireTweaksConfig.EnableNoBatTransformCooldown.Value = isChecked;
                };
            };
        }
    }
}