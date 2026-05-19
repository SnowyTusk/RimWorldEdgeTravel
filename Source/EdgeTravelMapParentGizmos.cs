using System.Collections.Generic;
using RimWorld;
using Verse;

namespace SnowyTusk.EdgeTravel
{
    public static class EdgeTravelMapParentGizmos
    {
        public static IEnumerable<Gizmo> DevGizmos(EdgeTravelMapParent parent)
        {
            yield return Info(parent);
            yield return ClearFailed(parent);
        }

        private static Gizmo Info(EdgeTravelMapParent parent)
        {
            return new Command_Action
            {
                defaultLabel = "Информация об области",
                defaultDesc = EdgeTravelRegistry.Current.GetSummary() + "\n\n" + EdgeTravelConstants.ArchivePathLabel + "\n" + EdgeTravelArchivePaths.PathFor(parent.Tile),
                action = delegate
                {
                    Messages.Message(EdgeTravelConstants.ModPrefix + " " + EdgeTravelRegistry.Current.GetSummary(), MessageTypeDefOf.NeutralEvent, false);
                }
            };
        }

        private static Gizmo ClearFailed(EdgeTravelMapParent parent)
        {
            EdgeTravelRecord record = EdgeTravelRegistry.Current.GetRecord(parent.Tile);
            Command_Action clearFailed = new Command_Action
            {
                defaultLabel = "Сбросить ошибку",
                defaultDesc = "Сбрасывает ошибочный статус этой области. Архивы не удаляются, карты не пересоздаются.",
                action = delegate
                {
                    EdgeTravelRegistry.Current.ClearFailed(parent.Tile);
                    Messages.Message(EdgeTravelConstants.ModPrefix + " Ошибочный статус сброшен для тайла " + parent.Tile + ".", MessageTypeDefOf.NeutralEvent, false);
                }
            };

            if (record == null || record.Status != EdgeTravelStatus.Failed)
            {
                clearFailed.Disabled = true;
                clearFailed.disabledReason = "Эта область не отмечена как ошибочная.";
            }

            return clearFailed;
        }
    }
}
