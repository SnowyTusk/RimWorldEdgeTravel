using System.Diagnostics;
using RimWorld.Planet;
using Verse;

namespace SnowyTusk.EdgeTravel
{
    public static class EdgeTravelDiagnostics
    {
        public static Stopwatch StartTimer()
        {
            return Stopwatch.StartNew();
        }

        public static void LogTransitionPrepared(Stopwatch stopwatch)
        {
            stopwatch.Stop();
            Log.Message(EdgeTravelConstants.ModPrefix + " Подготовка перехода заняла " + stopwatch.ElapsedMilliseconds + " мс.");
        }

        public static void LogPrepareFailed(PlanetTile targetTile)
        {
            Log.Warning(EdgeTravelConstants.ModPrefix + " Не удалось создать целевую карту для тайла " + targetTile + ".");
        }

        public static void LogTransitionFailed(System.Exception exception)
        {
            Log.Error(EdgeTravelConstants.ModPrefix + " Ошибка при подготовке перехода: " + exception);
        }

        public static void LogOccupiedTile(PlanetTile targetTile, string label)
        {
            Log.Message(EdgeTravelConstants.ModPrefix + " Целевой тайл " + targetTile + " уже занят объектом " + label + ". Переход пропущен.");
        }

        public static void LogTransitionComplete(string pawnLabel, PlanetTile sourceTile, PlanetTile targetTile)
        {
            Log.Message(EdgeTravelConstants.ModPrefix + " " + pawnLabel + " перемещен с тайла " + sourceTile + " на тайл " + targetTile + ".");
        }
    }
}
