using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace BDsGlitterworldFaction3HST
{
    public class CompProperties_ChangeBarrel : CompProperties
    {
        public CompProperties_ChangeBarrel()
        {
            compClass = typeof(CompChangeBarrel);
        }
        public int durability = 60;
        public int loadingAmount = 60;
        [MustTranslate]
        public string label;
    }
}
