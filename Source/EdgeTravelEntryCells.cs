using UnityEngine;
using Verse;

namespace SnowyTusk.EdgeTravel
{
    public static class EdgeTravelEntryCells
    {
        public static IntVec3 GetEntryCell(IntVec3 sourceCell, IntVec3 targetSize, EdgeDirection edgeDirection)
        {
            int x = Mathf.Clamp(sourceCell.x, EdgeTravelConstants.EntryInset, targetSize.x - EdgeTravelConstants.EntryInset - 1);
            int z = Mathf.Clamp(sourceCell.z, EdgeTravelConstants.EntryInset, targetSize.z - EdgeTravelConstants.EntryInset - 1);

            switch (edgeDirection)
            {
                case EdgeDirection.North:
                    z = EdgeTravelConstants.EntryInset;
                    break;
                case EdgeDirection.East:
                    x = EdgeTravelConstants.EntryInset;
                    break;
                case EdgeDirection.South:
                    z = targetSize.z - EdgeTravelConstants.EntryInset - 1;
                    break;
                case EdgeDirection.West:
                    x = targetSize.x - EdgeTravelConstants.EntryInset - 1;
                    break;
            }

            return new IntVec3(x, 0, z);
        }
    }
}
