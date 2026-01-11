using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.AI;

namespace BDsGlitterworldFaction3HST
{
    public class JobGiver_RailRearm : ThinkNode_JobGiver
    {
        public override float GetPriority(Pawn pawn)
        {
            return 6f;
        }
        protected override Job TryGiveJob(Pawn pawn)
        {
            if (pawn.equipment?.Primary?.GetComp<CompChangeBarrel>() is CompChangeBarrel comp)
            {
                if (comp.Durability >= comp.targetDurability)
                {
                    return null;
                }
                if (FindBestRail(pawn) is Thing thing)
                {
                    return MakeReloadJob(thing);
                }
            }
            return null;
        }
        public static Job MakeReloadJob(Thing thing)
        {
            return JobMaker.MakeJob(JobDefOf.Job_3HSTRailRearm, thing);
        }
        private static Thing FindBestRail(Pawn pawn)
        {
            bool validator(Thing x) => !x.IsForbidden(pawn) && pawn.CanReserve(x, 1, -1, null, false);
            return GenClosest.ClosestThingReachable(pawn.Position, pawn.Map, ThingRequest.ForDef(ThingDefOf.Resource_3HST_RailComponent), PathEndMode.ClosestTouch, TraverseParms.For(pawn, Danger.Deadly, TraverseMode.ByPawn, false, false, false), 9999f, validator, null, 0, -1, false, RegionType.Set_Passable, false);
        }
    }
}
