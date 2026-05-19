using Verse;

namespace SnowyTusk.EdgeTravel
{
    public static class EdgeTravelTransitionRules
    {
        public static bool TryGetEdgeDirection(IntVec3 cell, IntVec3 mapSize, int edgeBandWidth, out EdgeDirection edgeDirection)
        {
            if (cell.x < edgeBandWidth)
            {
                edgeDirection = EdgeDirection.West;
                return true;
            }

            if (cell.x >= mapSize.x - edgeBandWidth)
            {
                edgeDirection = EdgeDirection.East;
                return true;
            }

            if (cell.z < edgeBandWidth)
            {
                edgeDirection = EdgeDirection.South;
                return true;
            }

            if (cell.z >= mapSize.z - edgeBandWidth)
            {
                edgeDirection = EdgeDirection.North;
                return true;
            }

            edgeDirection = EdgeDirection.None;
            return false;
        }
    }
}
