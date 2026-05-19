using RimWorld;
using Verse;

namespace SnowyTusk.EdgeTravel
{
    public sealed class EdgeTravelTransitionService
    {
        private readonly Map map;
        private readonly EdgeTravelPawnTransitionState transitionState;

        public EdgeTravelTransitionService(Map map, EdgeTravelPawnTransitionState transitionState)
        {
            this.map = map;
            this.transitionState = transitionState;
        }

        public void Tick()
        {
            foreach (Pawn pawn in map.mapPawns.FreeColonistsSpawned)
            {
                if (!EdgeTravelMod.AutoTravelEnabled || !pawn.Drafted)
                {
                    continue;
                }

                EdgeDirection edgeDirection;
                if (EdgeTravelTransitionRules.TryGetEdgeDirection(pawn.Position, map.Size, EdgeTravelConstants.EdgeBandWidth, out edgeDirection) &&
                    transitionState.CanTransitionNow(pawn))
                {
                    QueueTransitionPawn(pawn, edgeDirection);
                }
            }
        }

        private void QueueTransitionPawn(Pawn pawn, EdgeDirection edgeDirection)
        {
            EdgeTravelTransitionRequest request = new EdgeTravelTransitionRequest(
                map,
                pawn,
                edgeDirection,
                transitionState);
            request.Queue();
        }
    }
}
