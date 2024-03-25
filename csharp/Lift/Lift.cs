using System;
using System.Collections.Generic;

namespace Lift
{
    public class Lift
    {
        public string Id { get; }
        public int Floor { get; private set; }
        public List<int> Requests { get; }
        public bool DoorsOpen { get; private set; }

        public Lift(string id, int floor, List<int> requests, bool doorsOpen = false)
        {
            Id = id;
            Floor = floor;
            Requests = requests;
            DoorsOpen = doorsOpen;
        }

        public Lift(string id, int floor, bool doorsOpen = false) : this(id, floor, new List<int>(), doorsOpen)
        {
        }

        public bool HasRequestForFloor(int floor)
        {
            return Requests.Contains(floor);
        }

        public void MoveTo(int newFloor)
        {
            Floor = newFloor;
        }

        public void OpenDoors()
        {
            DoorsOpen = true;
        }
    }
}
