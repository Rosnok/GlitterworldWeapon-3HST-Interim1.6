using CombatExtended;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;
using Verse.AI;

namespace BDsGlitterworldFaction3HST
{
    public class CompChangeBarrel : CompRangedGizmoGiver
    {
        public CompProperties_ChangeBarrel Props => props as CompProperties_ChangeBarrel;
        public int Durability
        {
            get
            {
                if (durability == -1)
                {
                    durability = Props.durability;
                }
                return durability;
            }
            set
            {
                durability = value;
                if (durability >= Props.durability)
                {
                    durability = Props.durability;
                }
            }
        }

        public override void Initialize(CompProperties props)
        {
            base.Initialize(props);
            targetDurability = (int)(Props.durability * 0.2);
        }

        public int targetDurability;

        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            if (Find.Selector.NumSelected == 1)
            {
                yield return new Gizmo_ChangeBarrel
                {
                    compChange = this
                };
            }

            Command_SetTargetFuelLevelForRailgun command_SetTargetFuelLevel = new Command_SetTargetFuelLevelForRailgun();
            command_SetTargetFuelLevel.refuelable = this;
            command_SetTargetFuelLevel.defaultLabel = "CommandSetTargetFuelLevel".Translate();
            command_SetTargetFuelLevel.defaultDesc = "CommandSetTargetFuelLevelDesc".Translate();
            command_SetTargetFuelLevel.icon = ContentFinder<Texture2D>.Get("UI/Commands/SetTargetFuelLevel");
            yield return command_SetTargetFuelLevel;

            if (parent.TryGetComp<CompEquippable>() is CompEquippable equippable && equippable.PrimaryVerb.CasterPawn != null)
            {
                Pawn pawn = equippable.PrimaryVerb.CasterPawn;
                Thing rail = null;
                foreach (Thing thing in pawn.inventory.innerContainer)
                {
                    if (thing.def == ThingDefOf.Resource_3HST_RailComponent)
                    {
                        rail = thing;
                    }
                }
                if (rail != null)
                {
                    Command_Action command_Action = new Command_Action();
                    command_Action.defaultLabel = "BDRG_RearmRail".Translate();
                    command_Action.icon = ThingDefOf.Resource_3HST_RailComponent.IconTexture();
                    command_Action.action = delegate
                    {
                        pawn.inventory.innerContainer.TryDrop(rail, ThingPlaceMode.Direct, 1, out Thing lastResultingThing);
                        Job job = JobMaker.MakeJob(JobDefOf.Job_3HSTRailRearm, lastResultingThing);
                        pawn.jobs.TryTakeOrderedJob(job, 0, true);
                    };
                    yield return command_Action;
                }
            }
        }
        public override void PostExposeData()
        {
            base.PostExposeData();
            Scribe_Values.Look(ref durability, "durability");
            Scribe_Values.Look(ref targetDurability, "targetDurability");
        }
        private int durability = -1;
    }
}
