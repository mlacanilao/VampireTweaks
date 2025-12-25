namespace VampireTweaks
{
    internal static class ConVampirePatch
    {
        internal static void TickPostfix()
        {
            bool enableNightVisionAtNight = VampireTweaksConfig.EnableNightVisionAtNight?.Value ?? false;

            if (enableNightVisionAtNight == false)
            {
                return;
            }
            
            if (EClass.world?.date?.IsNight == true)
            {
                bool hasConNightVision = EClass.pc.HasCondition<ConNightVision>();

                if (hasConNightVision == false)
                {
                    EClass.pc?.AddCondition<ConNightVision>(p: 100, force: false);
                }
            }
        }
    }
}