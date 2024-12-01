using System;
using FlightProject.FunctionalObjects;
namespace FlightProject.FlightObjects
{
    // Purpose: Represents a cargo plane.
    public class CargoPlane : BasePlane, IReportable
    {
        public Single MaxLoad {get; set;}

        // Purpose: viitor pattern accept method.
        public void accept(INewsVisitor visitor)
        {
            visitor.visitCargoPlane(this);
        }
    }
}
