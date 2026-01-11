using HarmonyLib;
using JetBrains.Annotations;
using System.Reflection;
using Verse;


namespace Weapon_HarmonyPatch
{
    [UsedImplicitly]
    [StaticConstructorOnStartup]
    public class PatchMain
    {
        static PatchMain()
        {
            var instance = new Harmony("BD_HarmonyPatches");
            instance.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}
