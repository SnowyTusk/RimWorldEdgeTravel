using Verse;

namespace SnowyTusk.EdgeTravel
{
    public static class EdgeTravelLongEvents
    {
        public static void QueueMapGeneration(System.Action action, System.Action<System.Exception> onException)
        {
            LongEventHandler.QueueLongEvent(action, "GeneratingMap", false, onException);
        }
    }
}
