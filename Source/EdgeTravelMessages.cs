using RimWorld;
using Verse;

namespace SnowyTusk.EdgeTravel
{
    public static class EdgeTravelMessages
    {
        public static void NoNeighbor(Pawn pawn)
        {
            Messages.Message("С этого края нельзя перейти в соседнюю область.", pawn, MessageTypeDefOf.RejectInput, false);
        }

        public static void OccupiedEdge(Pawn pawn)
        {
            Messages.Message("Этот край ведет в уже занятую область.", pawn, MessageTypeDefOf.RejectInput, false);
        }

        public static void PreparingArea(Pawn pawn)
        {
            Messages.Message("Edge Travel готовит соседнюю область...", pawn, MessageTypeDefOf.SilentInput, false);
        }

        public static void PrepareFailed(Pawn pawn)
        {
            Messages.Message("Edge Travel не смог подготовить соседнюю область. Подробности есть в логе.", pawn, MessageTypeDefOf.RejectInput, false);
        }

        public static void TransitionComplete(Pawn pawn)
        {
            Messages.Message(pawn.LabelShort + " перешел в соседнюю область.", pawn, MessageTypeDefOf.SilentInput, false);
        }

        public static void AlphaWarning()
        {
            Messages.Message("Edge Travel находится в ранней альфа-версии. Используйте тестовые сохранения, пока не будете уверены в поведении мода.", MessageTypeDefOf.CautionInput, false);
        }
    }
}
