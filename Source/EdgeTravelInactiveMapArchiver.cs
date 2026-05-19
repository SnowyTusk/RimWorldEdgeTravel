using System.Collections.Generic;
using RimWorld;
using Verse;

namespace SnowyTusk.EdgeTravel
{
    public static class EdgeTravelInactiveMapArchiver
    {
        public static void ArchiveOtherEdgeTravelMaps(Map currentMap)
        {
            List<Map> mapsToArchive = new List<Map>();
            for (int i = 0; i < Current.Game.Maps.Count; i++)
            {
                Map candidate = Current.Game.Maps[i];
                if (candidate != currentMap && candidate.Parent is EdgeTravelMapParent)
                {
                    mapsToArchive.Add(candidate);
                }
            }

            for (int i = 0; i < mapsToArchive.Count; i++)
            {
                ArchiveAndRemove(mapsToArchive[i]);
            }
        }

        private static void ArchiveAndRemove(Map mapToArchive)
        {
            EdgeTravelMapParent parent = mapToArchive.Parent as EdgeTravelMapParent;
            if (parent == null || !EdgeTravelMapArchive.TrySave(mapToArchive, parent))
            {
                return;
            }

            Log.Message(EdgeTravelConstants.ModPrefix + " Архивирована неактивная карта на тайле " + mapToArchive.Tile + ".");
            Current.Game.DeinitAndRemoveMap(mapToArchive, false);
        }
    }
}
