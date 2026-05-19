using RimWorld.Planet;
using Verse;

namespace SnowyTusk.EdgeTravel
{
    public static class EdgeTravelTransitionTargetResolver
    {
        public static bool TryResolve(Map sourceMap, EdgeDirection edgeDirection, Pawn pawn, out EdgeTravelTransitionTarget target)
        {
            target = null;

            PlanetTile targetTile;
            if (!EdgeTravelNeighborTiles.TryGetNeighborTile(sourceMap.Tile, edgeDirection, out targetTile))
            {
                EdgeTravelMessages.NoNeighbor(pawn);
                return false;
            }

            Map existingMap = Current.Game.FindMap(targetTile);
            if (existingMap != null)
            {
                target = new EdgeTravelTransitionTarget(targetTile, existingMap, null);
                return true;
            }

            MapParent parent = EdgeTravelTransitionWorld.TryCreateParent(targetTile);
            if (parent == null)
            {
                EdgeTravelMessages.OccupiedEdge(pawn);
                return false;
            }

            target = new EdgeTravelTransitionTarget(targetTile, null, parent);
            return true;
        }
    }
}
