using RimWorld;
using RimWorld.Planet;
using Verse;

namespace SnowyTusk.EdgeTravel
{
    public static class EdgeTravelMapFactory
    {
        public static MapParent CreateTravelMapParent(PlanetTile targetTile)
        {
            WorldObjectDef chunkDef = DefDatabase<WorldObjectDef>.GetNamed(EdgeTravelConstants.WorldObjectDefName);
            EdgeTravelMapParent chunk = (EdgeTravelMapParent)WorldObjectMaker.MakeWorldObject(chunkDef);
            chunk.SetFaction(Faction.OfPlayer);
            chunk.Tile = targetTile;
            chunk.Name = EdgeTravelConstants.DefaultChunkNamePrefix + targetTile;
            Find.WorldObjects.Add(chunk);
            EdgeTravelRegistry.Current.RegisterDiscovered(targetTile);
            return chunk;
        }

        public static Map GenerateTravelMap(MapParent targetParent, IntVec3 sourceSize)
        {
            return MapGenerator.GenerateMap(sourceSize, targetParent, targetParent.MapGeneratorDef, targetParent.ExtraGenStepDefs, null);
        }
    }
}
