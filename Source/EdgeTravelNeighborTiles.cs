using System.Collections.Generic;
using RimWorld.Planet;
using UnityEngine;
using Verse;

namespace SnowyTusk.EdgeTravel
{
    public static class EdgeTravelNeighborTiles
    {
        public static bool TryGetNeighborTile(PlanetTile sourceTile, EdgeDirection edgeDirection, out PlanetTile targetTile)
        {
            List<PlanetTile> neighbors = new List<PlanetTile>();
            Find.WorldGrid.GetTileNeighbors(sourceTile, neighbors);
            if (neighbors.Count == 0)
            {
                targetTile = PlanetTile.Invalid;
                return false;
            }

            Vector3 sourceCenter = Find.WorldGrid.GetTileCenter(sourceTile).normalized;
            Vector3 north = Vector3.ProjectOnPlane(Vector3.up, sourceCenter).normalized;
            if (north.sqrMagnitude < 0.001f)
            {
                north = Vector3.ProjectOnPlane(Vector3.forward, sourceCenter).normalized;
            }

            Vector3 east = Vector3.Cross(north, sourceCenter).normalized;
            Vector3 wanted = WantedDirection(edgeDirection, north, east);

            float bestScore = float.MinValue;
            targetTile = neighbors[0];
            for (int i = 0; i < neighbors.Count; i++)
            {
                Vector3 delta = Vector3.ProjectOnPlane(Find.WorldGrid.GetTileCenter(neighbors[i]) - Find.WorldGrid.GetTileCenter(sourceTile), sourceCenter).normalized;
                float score = Vector3.Dot(delta, wanted);
                if (score > bestScore)
                {
                    bestScore = score;
                    targetTile = neighbors[i];
                }
            }

            return targetTile.Valid;
        }

        private static Vector3 WantedDirection(EdgeDirection edgeDirection, Vector3 north, Vector3 east)
        {
            if (edgeDirection == EdgeDirection.East)
            {
                return east;
            }

            if (edgeDirection == EdgeDirection.South)
            {
                return -north;
            }

            if (edgeDirection == EdgeDirection.West)
            {
                return -east;
            }

            return north;
        }
    }
}
