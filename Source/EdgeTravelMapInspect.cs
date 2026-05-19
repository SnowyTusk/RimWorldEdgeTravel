using System.Text;

namespace SnowyTusk.EdgeTravel
{
    public static class EdgeTravelMapInspect
    {
        public static string For(EdgeTravelMapParent parent)
        {
            EdgeTravelRecord record = EdgeTravelRegistry.Current.GetRecord(parent.Tile);
            StringBuilder inspect = new StringBuilder();
            inspect.AppendLine("Карта Edge Travel.");
            inspect.Append("Прототипный объект, не поселение.");

            if (record != null)
            {
                AppendRecord(inspect, record);
            }

            inspect.AppendLine();
            inspect.Append("Реестр: ");
            inspect.Append(EdgeTravelRegistry.Current.GetSummary());
            return inspect.ToString();
        }

        private static void AppendRecord(StringBuilder inspect, EdgeTravelRecord record)
        {
            inspect.AppendLine();
            inspect.Append("Статус: ");
            inspect.Append(EdgeTravelStatusLabels.For(record.Status));

            if (!string.IsNullOrEmpty(record.ArchivePath))
            {
                inspect.AppendLine();
                inspect.Append("Архив: ");
                inspect.Append(record.ArchivePath);
            }

            if (!string.IsNullOrEmpty(record.LastError))
            {
                inspect.AppendLine();
                inspect.Append("Последняя ошибка: ");
                inspect.Append(record.LastError);
            }
        }
    }
}
