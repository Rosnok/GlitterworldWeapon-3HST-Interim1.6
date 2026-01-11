using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace BDsGlitterworldFaction3HST
{
    public class CompProperties_ChangeThing : CompProperties
    {
        public CompProperties_ChangeThing()
        {
            compClass = typeof(CompChangeThing);
        }
        public ThingDef thingDef;
        [MustTranslate]
        public string gizmoLabel;
        [MustTranslate]
        public string gizmoDesc;
        [NoTranslate]
        public string icon;
    }
}
