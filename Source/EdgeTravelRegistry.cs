using System.Collections.Generic;
using System.IO;
using RimWorld.Planet;
using Verse;

namespace SnowyTusk.EdgeTravel
{
    public sealed class EdgeTravelRegistry : WorldComponent
    {
        private List<EdgeTravelRecord> records = new List<EdgeTravelRecord>();

        public EdgeTravelRegistry(World world) : base(world)
        {
        }

        public static EdgeTravelRegistry Current
        {
            get { return Find.World.GetComponent<EdgeTravelRegistry>(); }
        }

        public void RegisterDiscovered(PlanetTile tile)
        {
            EdgeTravelRecord record = GetOrCreate(tile);
            if (record.Status == EdgeTravelStatus.Unknown)
            {
                record.Status = EdgeTravelStatus.Active;
            }
        }

        public void MarkActive(PlanetTile tile)
        {
            SetStatus(tile, EdgeTravelStatus.Active, null, null);
        }

        public void MarkArchived(PlanetTile tile, string archivePath)
        {
            SetStatus(tile, EdgeTravelStatus.Archived, archivePath, null);
        }

        public void MarkFailed(PlanetTile tile, string error)
        {
            SetStatus(tile, EdgeTravelStatus.Failed, null, error);
        }

        public void ClearFailed(PlanetTile tile)
        {
            EdgeTravelRecord record = GetOrCreate(tile);
            if (record.Status == EdgeTravelStatus.Failed)
            {
                record.Status = File.Exists(record.ArchivePath) ? EdgeTravelStatus.Archived : EdgeTravelStatus.Unknown;
                record.LastError = null;
            }
        }

        public EdgeTravelRecord GetRecord(PlanetTile tile)
        {
            return FindRecord(tile);
        }

        public string GetSummary()
        {
            return GetCounts().ToString();
        }

        public EdgeTravelRegistrySummary GetCounts()
        {
            int active = 0;
            int archived = 0;
            int failed = 0;
            int unknown = 0;

            for (int i = 0; i < records.Count; i++)
            {
                if (records[i].Status == EdgeTravelStatus.Active)
                {
                    active++;
                }
                else if (records[i].Status == EdgeTravelStatus.Archived)
                {
                    archived++;
                }
                else if (records[i].Status == EdgeTravelStatus.Failed)
                {
                    failed++;
                }
                else
                {
                    unknown++;
                }
            }

            return new EdgeTravelRegistrySummary(records.Count, active, archived, failed, unknown);
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Collections.Look(ref records, EdgeTravelConstants.RegistryKey, LookMode.Deep);
            if (Scribe.mode == LoadSaveMode.PostLoadInit && records == null)
            {
                records = new List<EdgeTravelRecord>();
            }
        }

        private EdgeTravelRecord GetOrCreate(PlanetTile tile)
        {
            EdgeTravelRecord record = FindRecord(tile);
            if (record != null)
            {
                return record;
            }

            record = new EdgeTravelRecord(tile.tileId);
            records.Add(record);
            return record;
        }

        private EdgeTravelRecord FindRecord(PlanetTile tile)
        {
            for (int i = 0; i < records.Count; i++)
            {
                if (records[i].TileId == tile.tileId)
                {
                    return records[i];
                }
            }

            return null;
        }

        private void SetStatus(PlanetTile tile, EdgeTravelStatus status, string archivePath, string error)
        {
            EdgeTravelRecord record = GetOrCreate(tile);
            record.Status = status;

            if (archivePath != null)
            {
                record.ArchivePath = archivePath;
            }

            record.LastError = error;
        }
    }
}
