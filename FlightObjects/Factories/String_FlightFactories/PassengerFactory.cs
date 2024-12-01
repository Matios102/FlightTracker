using System;

// Factory pattern for creating Passenger objects from string values
namespace FlightProject.FlightObjects.Factories.String_FlightFactories
{
    public class PassengerFactory : Factory
    {
        public override Passenger Create(string[] values)
        {

            if (values.Length < 8)
            {
                throw new ArgumentException("invalid values");
            }

                Passenger passenger = new Passenger
                {
                    ID = UInt64.Parse(values[1]),
                    Name = values[2],
                    Age = UInt64.Parse(values[3]),
                    Phone = values[4],
                    Email = values[5],
                    Class = values[6],
                    Miles = UInt64.Parse(values[7])
                };

                return passenger;
        }
    }
}
