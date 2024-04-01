using System;
using FlightProject.FunctionalObjects;
namespace FlightProject.FlightObjects
{
    public class CargoPlane : BasePlane, IReportable
    {
        public Single MaxLoad {get; set;}

        public void accept(INewsVisitor visitor)
        {
            visitor.visitCargoPlane(this);
        }
    }
}
