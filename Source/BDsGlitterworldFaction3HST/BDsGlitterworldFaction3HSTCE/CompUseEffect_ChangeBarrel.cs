using CombatExtended;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verse;
using Verse.Sound;

namespace BDsGlitterworldFaction3HST
{
    public class CompUseEffect_ChangeBarrel : CompUseEffect
    {
        public CompProperties_Barrel Props => props as CompProperties_Barrel;
        public override AcceptanceReport CanBeUsedBy(Pawn p)
        {
            if (p.equipment?.Primary?.GetComp<CompChangeBarrel>() is CompChangeBarrel comp)
            {
                if (comp.Durability == comp.Props.durability)
                {
                    return "BDGW_BarrelFullMessage".Translate();
                }
                return true;
            }
            return "BDGW_NoWeaponMessage".Translate();
        }
        public override void DoEffect(Pawn usedBy)
        {
            if (usedBy.equipment?.Primary?.GetComp<CompChangeBarrel>() is CompChangeBarrel comp)
            {
                comp.Durability += comp.Props.loadingAmount;
                if (Props.loadSound != null)
                {
                    Props.loadSound.PlayOneShot(new TargetInfo(usedBy.DrawPos.ToIntVec3(), usedBy.Map, false));
                }
                if (parent.stackCount == 1)
                {
                    parent.Destroy();
                }
                else
                {
                    parent.stackCount -= 1;
                }
            }
        }
    }
}
