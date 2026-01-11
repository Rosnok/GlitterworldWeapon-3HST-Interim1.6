using HarmonyLib;
using JetBrains.Annotations;
using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;    

namespace BDsGlitterworld.Patches
{
    [StaticConstructorOnStartup]
    public static class PawnKindDef
    {

        [HarmonyPatch(typeof(PawnGenerator), "GeneratePawn", new Type[] { typeof(PawnGenerationRequest) })]
        private static class PawnKindDef_Patch
        {
            [HarmonyPostfix]
            static void Postfix(PawnGenerationRequest request, ref Pawn __result)
            {
                if(request.KindDef is PawnKindWithHediffsDef def)
                {
                    foreach(HediffDef x in def.addHediffDefs)
                    {
                        HealthUtility.AdjustSeverity(__result,x,x.initialSeverity);
                    }
                }
            }
        }
    }
}
