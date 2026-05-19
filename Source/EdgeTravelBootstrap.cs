using HarmonyLib;
using RimWorld;
using Verse;

namespace SnowyTusk.EdgeTravel
{
    [StaticConstructorOnStartup]
    public static class EdgeTravelBootstrap
    {
        static EdgeTravelBootstrap()
        {
            Harmony harmony = new Harmony("snowytusk.edgetravel");
            harmony.PatchAll();
            Log.Message(EdgeTravelConstants.ModPrefix + " Загружен. Ожидаем карты игрока.");
        }
    }

    [HarmonyPatch(typeof(Map), "MapPreTick")]
    public static class EdgeTravelMapPreTickPatch
    {
        public static bool Prefix(Map __instance)
        {
            return EdgeTravelHibernation.ShouldTick(__instance);
        }
    }

    [HarmonyPatch(typeof(Map), "MapPostTick")]
    public static class EdgeTravelMapPostTickPatch
    {
        public static bool Prefix(Map __instance)
        {
            return EdgeTravelHibernation.ShouldTick(__instance);
        }
    }

    public static class EdgeTravelHibernation
    {
        public static bool ShouldTick(Map map)
        {
            if (map == null || !(map.Parent is EdgeTravelMapParent))
            {
                return true;
            }

            return Current.Game == null || Current.Game.CurrentMap == map;
        }
    }
}
