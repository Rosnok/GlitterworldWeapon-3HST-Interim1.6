using HarmonyLib;
using JetBrains.Annotations;
using RimWorld;
using System;
using System.Collections.Generic;
using UnityEngine;
using Verse;    

namespace BDsGlitterworldFaction3HST.Patch
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
                if (request.KindDef is PawnKindWithHediffsDef def)
                {
                    if (def.isSlaveTagged)
                    {
                        List<BodyPartRecord> bodyParts = __result.def.race.body.AllParts;
                        foreach(BodyPartRecord x in bodyParts)
                        {
                            if(x.def == BodyPartDefOf.Neck)
                            {
                                Hediff hediff = HediffMaker.MakeHediff(HediffDefOf.Hediff_3HSTSlaveTag, __result, x);
                                __result.health.hediffSet.AddDirect(hediff, null, null);
                            }
                        }
                    }
                }
            }
        }
    }
}
