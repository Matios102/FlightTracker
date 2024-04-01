using System;
using FlightProject.FunctionalObjects;
namespace FlightProject.FlightObjects
{
    public class PassengerPlane : BasePlane, IReportable
    {
        public UInt16 FirstClassSize {get; set;}
        public UInt16 BusinessClassSize {get; set;}
        public UInt16 EconomyClassSize {get; set;}

        public void accept(INewsVisitor visitor)
        {
            visitor.visitPassengerPlane(this);
        }
    }
}
