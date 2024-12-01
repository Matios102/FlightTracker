using System;
using FlightProject.FunctionalObjects;
namespace FlightProject.FlightObjects
{
    // Purpose: Represents an airport.
    public class Airport : BaseObject, IReportable
    {
        public String Name {get; set;}
        public String Code {get; set;}
        public Single Longitude {get; set;}
        public Single Latitude {get; set;}
        public Single AMSL {get; set;}
        public String ISO {get; set;}

        // Purpose: viitor pattern accept method.
        public void accept(INewsVisitor visitor)
        {
            visitor.visitAirport(this);
        }
    }
}
