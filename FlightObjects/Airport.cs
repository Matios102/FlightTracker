using System;
using FlightProject.FunctionalObjects;
namespace FlightProject.FlightObjects
{
    public class Airport : BaseObject, IReportable
    {
        public String Name {get; set;}
        public String Code {get; set;}
        public Single Longitude {get; set;}
        public Single Latitude {get; set;}
        public Single AMSL {get; set;}
        public String ISO {get; set;}

        public void accept(INewsVisitor visitor)
        {
            visitor.visitAirport(this);
        }
    }
}
