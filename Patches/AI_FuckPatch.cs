using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;

namespace VampireTweaks
{
    internal static class AI_FuckPatch
    {
        internal static bool TryWitnessCrimeWrapper(
            Point pos,
            Chara criminal,
            Chara target,
            int radius,
            Func<Chara, bool> funcWitness)
        {
            bool enableNoBloodSuckCrimeWitness = VampireTweaksConfig.EnableNoBloodSuckCrimeWitness?.Value ?? false;
            
            if (enableNoBloodSuckCrimeWitness == true &&
                criminal?.ai is AI_Fuck ai &&
                ai.variation == AI_Fuck.Variation.Bloodsuck)
            {
                return false;
            }

            return pos.TryWitnessCrime(
                criminal: criminal,
                target: target,
                radius: radius,
                funcWitness: funcWitness);
        }
        
        internal static IEnumerable<CodeInstruction> RunTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            var codeMatcher = new CodeMatcher(instructions: instructions);
            
            var tryWitnessCrimeMethod = AccessTools.Method(
                type: typeof(Point),
                name: nameof(Point.TryWitnessCrime),
                parameters: new[]
                {
                    typeof(Chara),
                    typeof(Chara),
                    typeof(int),
                    typeof(Func<Chara, bool>)
                });
            
            var tryWitnessCrimeWrapperMethod = AccessTools.Method(
                type: typeof(AI_FuckPatch),
                name: nameof(TryWitnessCrimeWrapper));
            
            codeMatcher.MatchStartForward(matches: new[]
            {
                new CodeMatch(
                    opcode: OpCodes.Callvirt,
                    operand: tryWitnessCrimeMethod)
            });
            
            if (codeMatcher.IsValid)
            {
                VampireTweaks.Log(payload: "TryWitnessCrime call found, redirecting to wrapper");

                codeMatcher.SetInstruction(
                    instruction: new CodeInstruction(
                        opcode: OpCodes.Call,
                        operand: tryWitnessCrimeWrapperMethod)
                        .WithLabels(labels: codeMatcher.Instruction.labels)
                        .WithBlocks(blocks: codeMatcher.Instruction.blocks));
            }
            else
            {
                VampireTweaks.Log(payload: "TryWitnessCrime call not found in AI_Fuck.Run");
            }
            
            return codeMatcher.Instructions();
        }
    }
}