using Verse;

namespace SnowyTusk.EdgeTravel
{
    public sealed class EdgeTravelMapComponent : MapComponent
    {
        private readonly EdgeTravelTransitionService transitionService;

        public EdgeTravelMapComponent(Map map) : base(map)
        {
            transitionService = new EdgeTravelTransitionService(map, new EdgeTravelPawnTransitionState());
        }

        public override void MapComponentTick()
        {
            base.MapComponentTick();

            ShowAlphaWarningIfNeeded();

            if (Find.TickManager.TicksGame % EdgeTravelConstants.CheckIntervalTicks != 0)
            {
                return;
            }

            if (map == null || !map.IsPlayerHome)
            {
                return;
            }

            transitionService.Tick();
        }

        private static void ShowAlphaWarningIfNeeded()
        {
            EdgeTravelSettings settings = EdgeTravelMod.Settings;
            if (settings == null || !settings.ShowAlphaWarning || settings.AlphaWarningShown)
            {
                return;
            }

            settings.AlphaWarningShown = true;
            settings.Write();
            EdgeTravelMessages.AlphaWarning();
        }
    }
}
