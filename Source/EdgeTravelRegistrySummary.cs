namespace SnowyTusk.EdgeTravel
{
    public struct EdgeTravelRegistrySummary
    {
        public EdgeTravelRegistrySummary(int total, int active, int archived, int failed, int unknown) : this()
        {
            Total = total;
            Active = active;
            Archived = archived;
            Failed = failed;
            Unknown = unknown;
        }

        public int Total { get; private set; }

        public int Active { get; private set; }

        public int Archived { get; private set; }

        public int Failed { get; private set; }

        public int Unknown { get; private set; }

        public override string ToString()
        {
            return "Областей всего=" + Total +
                   ", активных=" + Active +
                   ", архивированных=" + Archived +
                   ", ошибочных=" + Failed +
                   ", неизвестных=" + Unknown;
        }
    }
}
