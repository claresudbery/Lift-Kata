using System.Collections.Generic;
using System.Linq;

namespace Lift
{
    public class LiftSystem
    {
        public List<int> Floors { get; }
        public List<Lift> Lifts { get; }
        public List<Call> Calls { get; }

        public LiftSystem(List<int> floors, List<Lift> lifts, List<Call> calls)
        {
            Floors = floors;
            Lifts = lifts;
            Calls = calls;
        }

        public List<int> FloorsInDescendingOrder()
        {
            var copy = new List<int>(Floors);
            copy.Reverse();
            return copy;
        }

        public IEnumerable<Call> CallsForFloor(int floor)
        {
            return Calls.Where(c => c.Floor == floor);
        }

        public void Tick()
        {
            foreach (var lift in Lifts)
            {
                lift.FulFilRequestsOrCloseDoors();
                RespondToCalls(lift);
            }
        }

        private void RespondToCalls(Lift lift)
        {
            if (Calls.Count > 0)
            {
                if (lift.Floor != Calls[0].Floor)
                {
                    lift.MoveTo(Calls[0].Floor);
                }
            }
        }
    }
}