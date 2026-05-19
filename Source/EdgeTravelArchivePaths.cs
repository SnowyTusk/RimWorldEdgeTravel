using System.IO;
using RimWorld.Planet;
using Verse;

namespace SnowyTusk.EdgeTravel
{
    public static class EdgeTravelArchivePaths
    {
        public static string PathFor(PlanetTile tile)
        {
            string folder = Path.Combine(GenFilePaths.SaveDataFolderPath, EdgeTravelConstants.ArchiveFolderName, CurrentGameKey());
            return Path.Combine(folder, "tile_" + tile.tileId + ".xml");
        }

        private static string CurrentGameKey()
        {
            if (Current.Game != null && Current.Game.Info != null && !string.IsNullOrEmpty(Current.Game.Info.permadeathModeUniqueName))
            {
                return MakeSafeFileName(Current.Game.Info.permadeathModeUniqueName);
            }

            return "default";
        }

        private static string MakeSafeFileName(string value)
        {
            char[] invalid = Path.GetInvalidFileNameChars();
            for (int i = 0; i < invalid.Length; i++)
            {
                value = value.Replace(invalid[i], '_');
            }

            return value;
        }
    }
}
