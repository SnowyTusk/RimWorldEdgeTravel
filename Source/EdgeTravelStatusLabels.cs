namespace SnowyTusk.EdgeTravel
{
    public static class EdgeTravelStatusLabels
    {
        public static string For(EdgeTravelStatus status)
        {
            switch (status)
            {
                case EdgeTravelStatus.Active:
                    return "активна";
                case EdgeTravelStatus.Archived:
                    return "архивирована";
                case EdgeTravelStatus.Failed:
                    return "ошибка";
                default:
                    return "неизвестно";
            }
        }
    }
}
