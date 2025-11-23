using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;

namespace VampireTweaks
{
    internal static class CraftUtilPatch
    {
        internal static int GetMappedOrOriginalFeederUid(Chara feeder)
        {
            return VampireTweaksUtils.GetMappedOrOriginalUid(
                originalUid: feeder.uid
            );
        }
        
        internal static IEnumerable<CodeInstruction> MakeBloodMealTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            var codeMatcher = new CodeMatcher(instructions: instructions);
            
            var getUidMethod = AccessTools.PropertyGetter(
                type: typeof(Card),
                name: "uid"
            );

            var getMappedOrOriginalFeederUidMethod = AccessTools.Method(
                type: typeof(CraftUtilPatch),
                name: nameof(GetMappedOrOriginalFeederUid)
            );

            codeMatcher.MatchStartForward(matches: new[]
            {
                new CodeMatch(
                    opcode: OpCodes.Callvirt,
                    operand: getUidMethod)
            });

            while (codeMatcher.IsValid)
            {
                codeMatcher.SetInstruction(
                    instruction: new CodeInstruction(
                            opcode: OpCodes.Call,
                            operand: getMappedOrOriginalFeederUidMethod
                        ).WithLabels(labels: codeMatcher.Instruction.labels)
                        .WithBlocks(blocks: codeMatcher.Instruction.blocks)
                );

                codeMatcher.MatchForward(
                    useEnd: false,
                    matches: new[]
                    {
                        new CodeMatch(
                            opcode: OpCodes.Callvirt,
                            operand: getUidMethod)
                    }
                );
            }
            
            return codeMatcher.Instructions();
        }
    }
}