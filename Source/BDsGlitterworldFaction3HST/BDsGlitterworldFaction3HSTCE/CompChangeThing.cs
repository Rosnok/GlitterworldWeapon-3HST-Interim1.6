using CombatExtended;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Verse;

namespace BDsGlitterworldFaction3HST
{
    public class CompChangeThing : CompRangedGizmoGiver
    {
        public CompProperties_ChangeThing Props => props as CompProperties_ChangeThing;
        public Pawn Caster
        {
            get
            {
                if (base.ParentHolder is Pawn_EquipmentTracker pawn_EquipmentTracker)
                {
                    return pawn_EquipmentTracker.pawn;
                }
                return null;
            }
        }
        public override IEnumerable<Gizmo> CompGetGizmosExtra()
        {
            yield return new Command_Action
            {
                defaultLabel = Props.gizmoLabel,
                defaultDesc = Props.gizmoDesc,
                icon = ContentFinder<Texture2D>.Get(Props.icon, false),
                action = delegate ()
                {
                    ThingWithComps thing = ThingMaker.MakeThing(Props.thingDef) as ThingWithComps;
                    thing.HitPoints = parent.HitPoints;
                    thing.stackCount = parent.stackCount;
                    if (Caster == null)
                    {
                        IntVec3 pos = parent.Position;
                        Map map = parent.Map;
                        parent.Destroy();
                        GenSpawn.Spawn(thing, pos, map);
                        Find.Selector.Select(thing, false, true);
                    }
                    else
                    {
                        Pawn pawn = Caster;
                        pawn.equipment.DestroyEquipment(parent);
                        pawn.equipment.AddEquipment(thing);
                    }
                }
            };
        }
    }
}
