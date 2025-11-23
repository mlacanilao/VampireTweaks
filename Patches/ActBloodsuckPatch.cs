namespace VampireTweaks
{
    internal static class ActBloodsuckPatch
    {
        internal static void IsHostileActPostfix(ref bool __result)
        {
            bool enableNonHostileBloodSuck = VampireTweaksConfig.EnableNonHostileBloodSuck?.Value ?? false;

            if (enableNonHostileBloodSuck == false)
            {
                return;
            }
            
            __result = false;
        }
        
        internal static void CanPerformPostfix(ref bool __result)
        {
            bool enableUniversalBloodSuck = VampireTweaksConfig.EnableUniversalBloodSuck?.Value ?? false;

            if (enableUniversalBloodSuck == false)
            {
                return;
            }
            
            if (Act.TC != null)
            {
                __result = true;
            }
        }
    }
}