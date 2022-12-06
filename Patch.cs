using System.Linq;
using UnityEngine;

namespace ShyGuyIsPISSED
{
    using NorthwoodLib.Pools;
    using Mirror;
    using System.Collections.Generic;
    using System.Reflection;
    using Exiled.API.Features;
    using System.Reflection.Emit;
    using HarmonyLib;
    using static HarmonyLib.AccessTools;

    [HarmonyPatch(typeof(PlayableScps.Scp096), nameof(PlayableScps.Scp096.Windup))]
    public static class ShyGuyTranspiler
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            Log.Warn("Transpiling...");
            Log.Info($"Is null: {Method(typeof(PlayableScps.Scp096), nameof(PlayableScps.Scp096.Enrage)) is null}");
            var instructionList = ListPool<CodeInstruction>.Shared.Rent(instructions);
            int index = instructionList.Count - 1;

            Log.Debug($"Adding instructions at index: {index}...");
            instructionList.InsertRange(index, new []
            {
                new CodeInstruction(OpCodes.Ldstr, "HE IS FUCKING"),
                new CodeInstruction(OpCodes.Ldc_I4_1),
                new CodeInstruction(OpCodes.Call, Method(typeof(Log), nameof(Log.Debug), new []{typeof(string), typeof(bool)})),
                new CodeInstruction(OpCodes.Ldarg_0),
                new CodeInstruction(OpCodes.Ldfld, Field(typeof(PlayableScps.Scp096), nameof(PlayableScps.Scp096.Hub))), 
                new CodeInstruction(OpCodes.Call, Method(typeof(Player), nameof(Player.Get), new []{typeof(ReferenceHub)})),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Player), nameof(Player.CurrentRoom))),
                new CodeInstruction(OpCodes.Callvirt, PropertyGetter(typeof(Room), nameof(Room.FlickerableLightController))),
                new CodeInstruction(OpCodes.Ldc_R4, 6f),
                new CodeInstruction(OpCodes.Callvirt, Method(typeof(FlickerableLightController), nameof(FlickerableLightController.ServerFlickerLights), new []{typeof(float)}))
            });

        
            // Return evey instruction
            for (int i = 0; i < instructionList.Count; i++)
            {
                yield return instructionList[i];
            }
            
            ListPool<CodeInstruction>.Shared.Return(instructionList);
        }
    }
}