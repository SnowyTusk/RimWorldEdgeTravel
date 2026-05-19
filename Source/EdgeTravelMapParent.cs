using System.Collections.Generic;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace SnowyTusk.EdgeTravel
{
    public sealed class EdgeTravelMapParent : MapParent
    {
        private string nameInt;
        private bool hasArchivedMap;

        public string Name
        {
            get { return nameInt; }
            set { nameInt = value; }
        }

        public bool HasArchivedMap
        {
            get { return hasArchivedMap && System.IO.File.Exists(EdgeTravelArchivePaths.PathFor(Tile)); }
        }

        public void Notify_ArchivedMapAvailable()
        {
            hasArchivedMap = true;
            EdgeTravelRegistry.Current.MarkArchived(Tile, EdgeTravelArchivePaths.PathFor(Tile));
        }

        public void Notify_ArchivedMapLoaded()
        {
            hasArchivedMap = false;
            EdgeTravelRegistry.Current.MarkActive(Tile);
        }

        public override string Label
        {
            get
            {
                if (!string.IsNullOrEmpty(nameInt))
                {
                    return nameInt;
                }

                return EdgeTravelConstants.DefaultChunkNamePrefix + Tile;
            }
        }

        public override string LabelShort
        {
            get { return Label; }
        }

        public override string LabelShortCap
        {
            get { return Label.CapitalizeFirst(); }
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref nameInt, "name");
            Scribe_Values.Look(ref hasArchivedMap, "hasArchivedMap", false);
        }

        public override bool ShouldRemoveMapNow(out bool alsoRemoveWorldObject)
        {
            alsoRemoveWorldObject = false;
            return false;
        }

        public override string GetInspectString()
        {
            if (!Prefs.DevMode)
            {
                return "Соседняя область, открытая через Edge Travel.";
            }

            return EdgeTravelMapInspect.For(this);
        }

        public override IEnumerable<Gizmo> GetGizmos()
        {
            foreach (Gizmo gizmo in base.GetGizmos())
            {
                yield return gizmo;
            }

            if (!Prefs.DevMode)
            {
                yield break;
            }

            foreach (Gizmo gizmo in EdgeTravelMapParentGizmos.DevGizmos(this))
            {
                yield return gizmo;
            }
        }
    }
}
