using System;

namespace FlightProject.FlightObjects
{
    // Purpose: Represents a crew.
    public class Crew : BaseObject
    {
        public String Name { get; set; }
        public UInt64 Age { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public UInt16 Practice { get; set; }
        public String Role { get; set; }


    }
}
