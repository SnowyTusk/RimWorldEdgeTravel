using System.Collections.Generic;
using RimWorld;
using Verse;

namespace SnowyTusk.EdgeTravel
{
    public sealed class EdgeTravelPawnTransitionState
    {
        private readonly Dictionary<int, int> nextAllowedTransitionTickByPawn = new Dictionary<int, int>();
        private readonly HashSet<int> queuedTransitionPawns = new HashSet<int>();

        public bool CanTransitionNow(Pawn pawn)
        {
            int nextAllowedTick;
            return !nextAllowedTransitionTickByPawn.TryGetValue(pawn.thingIDNumber, out nextAllowedTick) ||
                   Find.TickManager.TicksGame >= nextAllowedTick;
        }

        public bool TryReserve(Pawn pawn)
        {
            return queuedTransitionPawns.Add(pawn.thingIDNumber);
        }

        public void Release(int pawnId)
        {
            queuedTransitionPawns.Remove(pawnId);
        }

        public void StartCooldown(Pawn pawn)
        {
            nextAllowedTransitionTickByPawn[pawn.thingIDNumber] =
                Find.TickManager.TicksGame + EdgeTravelConstants.TransitionCooldownTicks;
        }
    }
}
