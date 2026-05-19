using Verse;

namespace SnowyTusk.EdgeTravel
{
    public sealed class EdgeTravelRecord : IExposable
    {
        private int tileId;
        private EdgeTravelStatus status;
        private string archivePath;
        private string lastError;
        private int archiveFormatVersion = 1;

        public EdgeTravelRecord()
        {
        }

        public EdgeTravelRecord(int tileId)
        {
            this.tileId = tileId;
            status = EdgeTravelStatus.Active;
        }

        public int TileId
        {
            get { return tileId; }
        }

        public EdgeTravelStatus Status
        {
            get { return status; }
            set { status = value; }
        }

        public string ArchivePath
        {
            get { return archivePath; }
            set { archivePath = value; }
        }

        public string LastError
        {
            get { return lastError; }
            set { lastError = value; }
        }

        public void ExposeData()
        {
            Scribe_Values.Look(ref tileId, "tileId");
            Scribe_Values.Look(ref status, "status", EdgeTravelStatus.Unknown);
            Scribe_Values.Look(ref archivePath, "archivePath");
            Scribe_Values.Look(ref lastError, "lastError");
            Scribe_Values.Look(ref archiveFormatVersion, "archiveFormatVersion", 1);
        }
    }
}
