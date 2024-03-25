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

        public void TickFulfilRequestsAndCalls()
        {
            foreach (var lift in Lifts)
            {
                lift.FulFilRequestsOrCloseDoors();
                if (Calls.Count > 0)
                {
                    if (lift.Floor != Calls[0].Floor)
                    {
                        lift.MoveTo(Calls[0].Floor);
                    }
                }
            }
        }

        public void TickOpenDoorsAndClearRequestAndCloseDoors()
        {
            foreach (var lift in Lifts)
            {
                if (lift.Requests.Count > 0)
                {
                    lift.FulfilRequests();
                }
                else
                {
                    lift.CloseDoors();
                }
            }
        }

        public void TickOpenDoorsAndClearRequest()
        {
            foreach (var lift in Lifts)
            {
                if (lift.Requests.Count > 0)
                {
                    var request = lift.Requests[0];
                    if (lift.Floor != request)
                    {
                        lift.MoveTo(request);
                    }
                    else
                    {
                        lift.Requests.RemoveAt(0);
                        lift.OpenDoors();
                    }
                }
            }
        }

        public void TickManyLiftsMoveToFloor()
        {
            foreach (var lift in Lifts)
            {
                if (lift.Requests.Count > 0)
                {
                    var request = lift.Requests[0];
                    lift.MoveTo(request);
                }
            }
        }

        public void TickMoveToFloor()
        {
            var lift = Lifts[0];
            if (lift.Requests.Count > 0)
            {
                var request = lift.Requests[0];
                lift.MoveTo(request);
            }
        }

        public void TickUnimplemented()
        {
            // TODO: implement this method
        }
    }
}