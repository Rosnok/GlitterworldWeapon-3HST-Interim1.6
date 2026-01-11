using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;

namespace BDsGlitterworldFaction3HST
{
	[DefOf]
	public static class ThingDefOf
	{
		static ThingDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(ThingDefOf));
		}
		public static ThingDef Resource_3HST_RailComponent;
	}
}

