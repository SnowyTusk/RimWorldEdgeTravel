using System.IO;
using Verse;

namespace SnowyTusk.EdgeTravel
{
    public static class EdgeTravelMapArchiveStorage
    {
        public static void Save(string path, Map map)
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
            Scribe.saver.InitSaving(path, "edgeTravelMap");
            Map mapToSave = map;
            Scribe_Deep.Look(ref mapToSave, "map");
            Scribe.saver.FinalizeSaving();
        }

        public static Map Load(string path)
        {
            Map loadedMap = null;
            Scribe.loader.InitLoading(path);
            Scribe_Deep.Look(ref loadedMap, "map");
            Scribe.loader.FinalizeLoading();
            return loadedMap;
        }
    }
}
