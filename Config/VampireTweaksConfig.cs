using System.Collections.Generic;
using System.IO;
using System.Linq;
using BepInEx.Configuration;

namespace VampireTweaks
{
    internal static class VampireTweaksConfig
    {
        internal static ConfigEntry<bool> EnableNonHostileBloodSuck;
        internal static ConfigEntry<bool> EnableUniversalBloodSuck;
        internal static ConfigEntry<bool> EnableNoBloodSuckCrimeWitness;
        internal static ConfigEntry<bool> EnableNoBloodSuckVictimDebuffs;
        internal static ConfigEntry<bool> EnableNoWeakToSunlight;
        internal static ConfigEntry<bool> EnableNoBatTransformCooldown;

        public static string XmlPath { get; private set; }
        public static string TranslationXlsxPath { get; private set; }

        internal static void LoadConfig(ConfigFile config)
        {
            EnableNonHostileBloodSuck = config.Bind(
                section: ModInfo.Name,
                key: "Enable Non-Hostile Blood Suck",
                defaultValue: false,
                description:
                "Blood Suck will no longer make the target hostile when used.\n" +
                "Useful for friendly feeding without starting combat.\n" +
                "吸血を使用しても相手が敵対しないようにします。\n" +
                "戦闘を始めずに吸血したい場合に便利です。\n" +
                "使用吸血时不会使目标变为敌对。\n" +
                "适合和平吸血或角色扮演使用。"
            );
            
            EnableUniversalBloodSuck = config.Bind(
                section: ModInfo.Name,
                key: "Enable Universal Blood Suck",
                defaultValue: false,
                description:
                "Allows Blood Suck to be used on any target, ignoring vanilla restrictions.\n" +
                "相手に関係なく吸血できるようになります。\n" +
                "可以对任意目标使用吸血技能。"
            );
            
            EnableNoBloodSuckCrimeWitness = config.Bind(
                section: ModInfo.Name,
                key: "Enable No Blood Suck Crime Witness",
                defaultValue: false,
                description:
                "Prevents Blood Suck from being reported as a crime by nearby witnesses.\n" +
                "周囲の目撃者に犯罪として通報されなくなります。\n" +
                "附近目击者将不会把吸血视作犯罪。"
            );
            
            EnableNoBloodSuckVictimDebuffs = config.Bind(
                section: ModInfo.Name,
                key: "Enable No Blood Suck Victim Debuffs",
                defaultValue: false,
                description:
                "Prevents debuffs from being applied by Blood Suck (Bleed, Confusion, Dim, Paralysis, Insanity).\n" +
                "吸血による流血、混乱、朦朧、麻痺、狂気の付与を無効化します。\n" +
                "阻止吸血造成的流血、混乱、视线模糊、麻痹、疯狂效果。"
            );
            
            EnableNoWeakToSunlight = config.Bind(
                section: ModInfo.Name,
                key: "Enable No Weak to Sunlight",
                defaultValue: false,
                description:
                "When enabled, vampires are no longer weakened or harmed by sunlight.\n" +
                "有効にすると、吸血鬼は日光による弱体化やダメージを受けなくなります。\n" +
                "启用后，吸血鬼将不会再受到阳光削弱或伤害。"
            );
            
            EnableNoBatTransformCooldown = config.Bind(
                section: ModInfo.Name,
                key: "Enable No Bat Transform Cooldown",
                defaultValue: false,
                description:
                "Removes the cooldown for Bat Transformation.\n" +
                "蝙蝠変容のクールダウンを無効化します。\n" +
                "移除蝙蝠变形的冷却时间。"
            );
        }
        
        public static void InitializeXmlPath(string xmlPath)
        {
            if (File.Exists(path: xmlPath))
            {
                XmlPath = xmlPath;
            }
            else
            {
                XmlPath = string.Empty;
            }
        }
        
        public static void InitializeTranslationXlsxPath(string xlsxPath)
        {
            if (File.Exists(path: xlsxPath))
            {
                TranslationXlsxPath = xlsxPath;
            }
            else
            {
                TranslationXlsxPath = string.Empty;
            }
        }
    }
}
