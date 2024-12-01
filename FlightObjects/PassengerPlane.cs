using System;
using FlightProject.FunctionalObjects;
namespace FlightProject.FlightObjects
{
    // Purpose: Represents a passenger plane.
    public class PassengerPlane : BasePlane, IReportable
    {
        public UInt16 FirstClassSize {get; set;}
        public UInt16 BusinessClassSize {get; set;}
        public UInt16 EconomyClassSize {get; set;}

        // Purpose: viitor pattern accept method.
        public void accept(INewsVisitor visitor)
        {
            visitor.visitPassengerPlane(this);
        }
    }
}
