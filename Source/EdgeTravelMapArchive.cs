using System.IO;
using RimWorld;
using Verse;

namespace SnowyTusk.EdgeTravel
{
    public static class EdgeTravelMapArchive
    {
        public static bool TrySave(Map map, EdgeTravelMapParent parent)
        {
            string path = EdgeTravelArchivePaths.PathFor(parent.Tile);
            try
            {
                EdgeTravelMapArchiveStorage.Save(path, map);
                parent.Notify_ArchivedMapAvailable();
                return true;
            }
            catch (System.Exception exception)
            {
                EdgeTravelRegistry.Current.MarkFailed(parent.Tile, exception.Message);
                Log.Error(EdgeTravelConstants.ModPrefix + " Не удалось архивировать карту на тайле " + parent.Tile + ": " + exception);
                return false;
            }
        }

        public static Map TryLoad(EdgeTravelMapParent parent)
        {
            string path = EdgeTravelArchivePaths.PathFor(parent.Tile);
            if (!File.Exists(path))
            {
                return null;
            }

            Map loadedMap = null;
            bool addedToGame = false;
            try
            {
                loadedMap = EdgeTravelMapArchiveStorage.Load(path);
                if (loadedMap == null)
                {
                    Log.Error(EdgeTravelConstants.ModPrefix + " Архив вернул пустую карту для тайла " + parent.Tile + ".");
                    return null;
                }

                loadedMap.info.parent = parent;
                Current.Game.AddMap(loadedMap);
                addedToGame = true;
                loadedMap.FinalizeLoading();
                loadedMap.FinalizeInit();
                parent.Notify_ArchivedMapLoaded();
                Log.Message(EdgeTravelConstants.ModPrefix + " Восстановлена архивированная карта на тайле " + parent.Tile + ".");
                return loadedMap;
            }
            catch (System.Exception exception)
            {
                if (addedToGame && loadedMap != null)
                {
                    Current.Game.Maps.Remove(loadedMap);
                }

                parent.Notify_ArchivedMapAvailable();
                EdgeTravelRegistry.Current.MarkFailed(parent.Tile, exception.Message);
                Log.Error(EdgeTravelConstants.ModPrefix + " Не удалось восстановить архивированную карту на тайле " + parent.Tile + ": " + exception);
                return null;
            }
        }
    }
}
