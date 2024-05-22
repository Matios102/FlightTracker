using System;
using System.Collections.Generic;
namespace FlightProject.FlightObjects
{
    public class Flight : BaseObject
    {
        public Airport Origin { get; set; }
        public Airport Target { get; set; }
        public String TakeOffTime { get; set; }
        public String LandingTime { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single AMSL { get; set; }
        public UInt64 PlaneID { get; set; }
        public UInt64[] CrewIDs { get; set; }
        public List<Crew> CrewList { get; set; }
        public UInt64[] LoadIDs { get; set; }
        public List<BaseObject> LoadList { get; set; }

        public BasePlane Plane { get; set; }

        public double MapCoordRotation { get; set; }
    }
}
