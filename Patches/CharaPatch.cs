namespace VampireTweaks
{
    internal static class CharaPatch
    {
        internal static bool AddConditionPrefix(Chara __instance, Condition c, bool force, ref Condition __result)
        {
            bool enableNoBloodSuckVictimDebuffs = VampireTweaksConfig.EnableNoBloodSuckVictimDebuffs?.Value ?? false;
            
            if (enableNoBloodSuckVictimDebuffs == false)
            {
                return true;
            }
            
            AI_Fuck ai = EClass.player?.chara?.ai as AI_Fuck;

            if (ai is null)
            {
                return true;
            }

            AI_Fuck.Variation variation = ai.variation;

            if (variation != AI_Fuck.Variation.Bloodsuck)
            {
                return true;
            }

            Chara owner = ai.owner;
            Chara target = ai.target;

            if (__instance != target)
            {
                return true;
            }
            
            if (c is ConConfuse ||
                c is ConDim ||
                c is ConParalyze ||
                c is ConInsane ||
                c is ConBleed)
            {
                __result = null;
                return false;
            }

            return true;
        }
        
        internal static void RefreshPostfix(Chara __instance)
        {
            bool enableNoWeakToSunlight = VampireTweaksConfig.EnableNoWeakToSunlight?.Value ?? false;

            if (enableNoWeakToSunlight == false ||
                (__instance.GetRootCard().HasElement(ele: 1251, req: 1) == false && __instance.GetRootCard().HasElement(ele: 431, req: 1) == true))
            {
                return;
            }
            
            __instance.isWeakToSunlight = false;
        }
        
        internal static bool AddCooldownPrefix(int idEle, int turns)
        {
            bool enableNoBatTransformCooldown = VampireTweaksConfig.EnableNoBatTransformCooldown?.Value ?? false;

            if (enableNoBatTransformCooldown == false ||
                idEle != 8793)
            {
                return true;
            }

            return false;
        }
    }
}