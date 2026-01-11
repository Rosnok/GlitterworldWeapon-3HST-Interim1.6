using System;
using System.Collections.Generic;
using RimWorld;
using UnityEngine;
using Verse;

namespace BDsGlitterworldFaction3HST
{


    [StaticConstructorOnStartup]
    public class Command_SetTargetFuelLevelForRailgun : Command
    {
        public CompChangeBarrel refuelable;

        public override void ProcessInput(Event ev)
        {
            base.ProcessInput(ev);
            int num = refuelable.Props.durability;
            int startingValue = refuelable.targetDurability;

            Func<int, string> textGetter = (int x) => "SetTargetFuelLevel".Translate(x);

            Dialog_Slider window = new Dialog_Slider(textGetter, 0, num, delegate (int value)
            {
                refuelable.targetDurability = value;
            }, startingValue);
            Find.WindowStack.Add(window);
        }
    }
}
