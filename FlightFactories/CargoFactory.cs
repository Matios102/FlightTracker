using System;

namespace FlightProject
{
    public class CargoFactory : Factory
    {
        public override BaseObject Create(string[] values)
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