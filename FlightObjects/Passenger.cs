using System;
namespace FlightProject.FlightObjects
{
    // Purpose: Represents a passenger.
    public class Passenger : BaseObject
    {
        public String Name { get; set; }
        public UInt64 Age { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Class { get; set; }
        public UInt64 Miles { get; set; }
    }
}
