using RimWorld;
using RimWorld.Planet;
using Verse;

namespace SnowyTusk.EdgeTravel
{
    public static class EdgeTravelTransitionWorld
    {
        public static MapParent TryCreateParent(PlanetTile targetTile)
        {
            MapParent targetParent = Find.WorldObjects.MapParentAt(targetTile);
            if (targetParent != null && !(targetParent is EdgeTravelMapParent))
            {
                EdgeTravelDiagnostics.LogOccupiedTile(targetTile, targetParent.Label);
                return null;
            }

            if (targetParent == null)
            {
                targetParent = EdgeTravelMapFactory.CreateTravelMapParent(targetTile);
            }

            return targetParent;
        }

        public static Map PrepareTargetMap(PlanetTile targetTile, MapParent targetParent, IntVec3 sourceMapSize)
        {
            Map targetMap = Current.Game.FindMap(targetTile);
            if (targetMap != null)
            {
                return targetMap;
            }

            EdgeTravelMapParent archivedParent = targetParent as EdgeTravelMapParent;
            if (archivedParent != null && archivedParent.HasArchivedMap)
            {
                targetMap = EdgeTravelMapArchive.TryLoad(archivedParent);
            }

            if (targetMap == null)
            {
                targetMap = EdgeTravelMapFactory.GenerateTravelMap(targetParent, sourceMapSize);
            }

            return targetMap;
        }
    }
}
