using CombatExtended;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using BillDoorsFramework;

namespace BDsGlitterworldFaction3HST
{
    public class Verb_ShootByBarrel : Verb_ShootCE
    {
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
        public override bool TryCastShot()
        {
            bool reslut =base.TryCastShot();
            if(reslut)
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
            return reslut;
        }
    }

    public class Verb_ShootNotUnderRoofByBarrel : Verb_ShootByBarrel
    {
        ModExtension_VerbNotUnderRoof extension => EquipmentSource.def.GetModExtension<ModExtension_VerbNotUnderRoof>();

        CompSecondaryVerbCE compSecondaryVerb => EquipmentSource.TryGetComp<CompSecondaryVerbCE>();

        CompSecondaryAmmo compSecondaryAmmo => EquipmentSource.TryGetComp<CompSecondaryAmmo>();
        public override bool Available()
        {
            if (Caster.Position.Roofed(Caster.Map))
            {
                if (extension == null || (compSecondaryVerb != null && ((compSecondaryVerb.IsSecondaryVerbSelected && extension.appliesInSecondaryMode) || (!compSecondaryVerb.IsSecondaryVerbSelected && extension.appliesInPrimaryMode))) || (compSecondaryAmmo != null && ((compSecondaryAmmo.IsSecondaryAmmoSelected && extension.appliesInSecondaryMode) || (!compSecondaryAmmo.IsSecondaryAmmoSelected && extension.appliesInPrimaryMode))))
                {
                    return false;
                }
            }
            return base.Available();
        }
    }

    public class Verb_ShootLGISCatapult : Verb_ShootMortarCE
    {
        public override bool Available()
        {
            if (Caster.Position.Roofed(Caster.Map))
            {
                return false;
            }
            return base.Available();
        }
    }
}
