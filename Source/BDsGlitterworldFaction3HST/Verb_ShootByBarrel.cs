using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Verse;
using BillDoorsFramework;

namespace BDsGlitterworldFaction3HST
{
    public class Verb_ShootByBarrel : Verb_Shoot
    {
        BillDoorsFramework.ModExtension_Verb_Shotgun extension => EquipmentSource.def.GetModExtension<BillDoorsFramework.ModExtension_Verb_Shotgun>();

        public override bool Available()
        {
            if(!base.Available())
            {
                return false;
            }
            if (CasterPawn?.equipment?.Primary?.GetComp<CompChangeBarrel>() is CompChangeBarrel comp)
            {
                if (comp.Durability == 0)
                {
                    return false;
                }
            }
            return true;
        }
        protected override bool TryCastShot()
        {
            bool result =base.TryCastShot();
            if(extension != null && extension.ShotgunPellets > 1)
            {
                for(int i=1; i< extension.ShotgunPellets; i++)
                {
                    base.TryCastShot();
                }
            }
            if(result)
            {
                if (CasterPawn?.equipment?.Primary?.GetComp<CompChangeBarrel>() is CompChangeBarrel comp)
                {
                    comp.Durability -= 1;
                    if(comp.Durability == 0)
                    {
                        Messages.Message(string.Format("BDGW_BarrelWornMessage".Translate(), CasterPawn.LabelCap), CasterPawn, MessageTypeDefOf.RejectInput, false);
                    }
                }
            }
            return result;
        }
    }

    public class Verb_ShootNotUnderRoofByBarrel : Verb_ShootByBarrel
    {
        ModExtension_VerbNotUnderRoof extension => EquipmentSource.def.GetModExtension<ModExtension_VerbNotUnderRoof>();
        CompSecondaryVerb compSecondaryVerb => EquipmentSource.TryGetComp<CompSecondaryVerb>();
        public override bool Available()
        {
            if (Caster.Position.Roofed(Caster.Map)
                && (compSecondaryVerb == null || extension == null || (compSecondaryVerb.IsSecondaryVerbSelected && extension.appliesInSecondaryMode) || (!compSecondaryVerb.IsSecondaryVerbSelected && extension.appliesInPrimaryMode)))
            {
                return false;
            }
            return base.Available();
        }
    }
}
