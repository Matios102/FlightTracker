using System;
namespace FlightProject
{
    public class Flight : BaseObject
    {
        public UInt64 Origin {get; set;}
        public UInt64 Target {get; set;}
        public String TakeOffTime {get; set;}
        public String LandingTime {get; set;}
        public Single Longitude {get; set;}
        public Single Latitude {get; set;}
        public Single AMSL {get; set;}
        public UInt64 PlaneID {get; set;}
        public UInt64[] CrewIDs {get; set;}
        public UInt64[] LoadIDs {get; set;}
    }
}
