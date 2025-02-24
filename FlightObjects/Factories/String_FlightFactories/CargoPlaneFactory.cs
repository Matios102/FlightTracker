using System;


// Factory pattern for creating CargoPlane objects from string values
namespace FlightProject.FlightObjects.Factories.String_FlightFactories
{
    public class CargoPlaneFactory : Factory
    {
        public override CargoPlane Create(string[] values)
        {

            if (values.Length < 6)
            {
                throw new ArgumentException("invalid values");
            }

            CargoPlane cargoPlane = new CargoPlane
            {
                ID = UInt64.Parse(values[1]),
                Serial = values[2],
                ISO = values[3],
                Model = values[4],
                MaxLoad = Single.Parse(values[5])
            };
            return cargoPlane;
        }
    }
}
