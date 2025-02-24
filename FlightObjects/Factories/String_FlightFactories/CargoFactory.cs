using System;

// Factory pattern for creating Cargo objects from string values
namespace FlightProject.FlightObjects.Factories.String_FlightFactories
{
    public class CargoFactory : Factory
    {
        public override Cargo Create(string[] values)
        {
            if (values.Length <  5)
            {
                throw new ArgumentException("invalid values");
            }

            Cargo cargo = new Cargo
            {
                ID = UInt64.Parse(values[1]),
                Weight = Single.Parse(values[2]),
                Code = values[3],
                Description = values[4],
            };
            return cargo;
        }
    }
}
