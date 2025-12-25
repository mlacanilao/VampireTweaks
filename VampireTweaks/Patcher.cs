using System;
using System.Collections.Generic;
using HarmonyLib;

namespace VampireTweaks
{
    internal class Patcher
    {
        [HarmonyPostfix]
        [HarmonyPatch(declaringType: typeof(ActBloodsuck), methodName: nameof(ActBloodsuck.IsHostileAct), methodType: MethodType.Getter)]
        internal static void ActBloodsuckIsHostileAct(ref bool __result)
        {
            ActBloodsuckPatch.IsHostileActPostfix(__result: ref __result);
        }
        
        [HarmonyPostfix]
        [HarmonyPatch(declaringType: typeof(ActBloodsuck), methodName: nameof(ActBloodsuck.CanPerform))]
        internal static void ActBloodsuckCanPerform(ref bool __result)
        {
            ActBloodsuckPatch.CanPerformPostfix(__result: ref __result);
        }
        
        [HarmonyTranspiler]
        [HarmonyPatch(declaringType: typeof(AI_Fuck), methodName: nameof(AI_Fuck.Run), methodType: MethodType.Enumerator)]
        internal static IEnumerable<CodeInstruction> AI_FuckRun(IEnumerable<CodeInstruction> instructions)
        {
            return AI_FuckPatch.RunTranspiler(instructions: instructions);
        }
        
        [HarmonyPrefix]
        [HarmonyPatch(declaringType: typeof(Chara), methodName: nameof(Chara.AddCondition), argumentTypes: new[] { typeof(Condition), typeof(bool) })]
        internal static bool CharaAddCondition(Chara __instance, Condition c, bool force, ref Condition __result)
        {
            return CharaPatch.AddConditionPrefix(__instance: __instance, c: c, force: force, __result: ref __result);
        }
        
        [HarmonyPostfix]
        [HarmonyPatch(declaringType: typeof(Chara), methodName: nameof(Chara.Refresh))]
        internal static void CharaRefresh(Chara __instance)
        {
            CharaPatch.RefreshPostfix(__instance: __instance);
        }
        
        [HarmonyPrefix]
        [HarmonyPatch(declaringType: typeof(Chara), methodName: nameof(Chara.AddCooldown))]
        internal static bool CharaAddCooldown(int idEle, int turns)
        {
            return CharaPatch.AddCooldownPrefix(idEle: idEle, turns: turns);
        }
        
        [HarmonyPostfix]
        [HarmonyPatch(declaringType: typeof(ConVampire), methodName: nameof(ConVampire.Tick))]
        internal static void ConVampireTick()
        {
            ConVampirePatch.TickPostfix();
        }
    }
}