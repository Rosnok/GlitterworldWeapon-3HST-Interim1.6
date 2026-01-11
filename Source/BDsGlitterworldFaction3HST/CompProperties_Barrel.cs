using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace BDsGlitterworldFaction3HST
{
    public class CompProperties_Barrel : CompProperties_UseEffect
    {
        public CompProperties_Barrel()
        {
            compClass = typeof(CompUseEffect_ChangeBarrel);
        }
        public SoundDef loadSound;
    }
}
