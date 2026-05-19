using RimWorld;
using Verse;

namespace SnowyTusk.EdgeTravel
{
    public sealed class EdgeTravelTransitionRequest
    {
        private readonly Map sourceMap;
        private readonly Pawn pawn;
        private readonly EdgeDirection edgeDirection;
        private readonly EdgeTravelPawnTransitionState transitionState;
        private readonly int pawnId;

        private EdgeTravelTransitionTarget target;

        public EdgeTravelTransitionRequest(
            Map sourceMap,
            Pawn pawn,
            EdgeDirection edgeDirection,
            EdgeTravelPawnTransitionState transitionState)
        {
            this.sourceMap = sourceMap;
            this.pawn = pawn;
            this.edgeDirection = edgeDirection;
            this.transitionState = transitionState;
            pawnId = pawn.thingIDNumber;
        }

        public void Queue()
        {
            if (!transitionState.TryReserve(pawn))
            {
                return;
            }

            if (!EdgeTravelTransitionTargetResolver.TryResolve(sourceMap, edgeDirection, pawn, out target))
            {
                CancelQueue();
                transitionState.StartCooldown(pawn);
                return;
            }

            transitionState.StartCooldown(pawn);
            EdgeTravelMessages.PreparingArea(pawn);
            EdgeTravelLongEvents.QueueMapGeneration(PrepareThenFinish, OnPrepareFailed);
        }

        private void PrepareThenFinish()
        {
            System.Diagnostics.Stopwatch stopwatch = EdgeTravelDiagnostics.StartTimer();
            try
            {
                Map preparedMap = target.ExistingMap;
                if (preparedMap == null)
                {
                    preparedMap = EdgeTravelTransitionWorld.PrepareTargetMap(target.Tile, target.Parent, sourceMap.Size);
                }

                LongEventHandler.ExecuteWhenFinished(delegate
                {
                    Finish(preparedMap);
                });
            }
            finally
            {
                EdgeTravelDiagnostics.LogTransitionPrepared(stopwatch);
            }
        }

        private void Finish(Map preparedMap)
        {
            CancelQueue();

            if (preparedMap == null)
            {
                EdgeTravelMessages.PrepareFailed(pawn);
                EdgeTravelDiagnostics.LogPrepareFailed(target.Tile);
                return;
            }

            if (pawn.Destroyed || !pawn.Spawned || pawn.Map != sourceMap)
            {
                return;
            }

            EdgeTravelTransitionRunner.FinishTransitionPawn(pawn, sourceMap, preparedMap, edgeDirection, target.Tile);
        }

        private void OnPrepareFailed(System.Exception exception)
        {
            CancelQueue();
            EdgeTravelDiagnostics.LogTransitionFailed(exception);
        }

        private void CancelQueue()
        {
            transitionState.Release(pawnId);
        }
    }
}
