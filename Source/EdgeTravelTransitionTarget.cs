using RimWorld.Planet;
using Verse;

namespace SnowyTusk.EdgeTravel
{
    public sealed class EdgeTravelTransitionTarget
    {
        public EdgeTravelTransitionTarget(PlanetTile tile, Map existingMap, MapParent parent)
        {
            Tile = tile;
            ExistingMap = existingMap;
            Parent = parent;
        }

        public PlanetTile Tile { get; private set; }

        public Map ExistingMap { get; private set; }

        public MapParent Parent { get; private set; }
    }
}
