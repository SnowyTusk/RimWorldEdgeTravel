using RimWorld;
using RimWorld.Planet;
using Verse;

namespace SnowyTusk.EdgeTravel
{
    public static class EdgeTravelTransitionRunner
    {
        public static void FinishTransitionPawn(Pawn pawn, Map sourceMap, Map targetMap, EdgeDirection edgeDirection, PlanetTile targetTile)
        {
            IntVec3 entryCell = EdgeTravelEntryCells.GetEntryCell(pawn.Position, targetMap.Size, edgeDirection);
            pawn.jobs.StopAll();
            pawn.pather.StopDead();
            pawn.DeSpawn(DestroyMode.Vanish);
            GenSpawn.Spawn(pawn, entryCell, targetMap, WipeMode.Vanish);
            Current.Game.CurrentMap = targetMap;
            Find.CameraDriver.JumpToCurrentMapLoc(entryCell);
            EdgeTravelInactiveMapArchiver.ArchiveOtherEdgeTravelMaps(targetMap);

            EdgeTravelMessages.TransitionComplete(pawn);
            EdgeTravelDiagnostics.LogTransitionComplete(pawn.LabelShort, sourceMap.Tile, targetTile);
        }
    }
}
