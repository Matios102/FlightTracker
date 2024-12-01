using System;

// Factory pattern for creating PassengerPlane objects from string values
namespace FlightProject.FlightObjects.Factories.String_FlightFactories
{
    public class PassengerPlaneFactory : Factory
    {
        public override PassengerPlane Create(string[] values)
        {

            if (values.Length < 8)
            {
                throw new ArgumentException("invalid values");
            }

                PassengerPlane passegnerPlane = new PassengerPlane
                {
                    ID = UInt64.Parse(values[1]),
                    Serial = values[2],
                    ISO = values[3],
                    Model = values[4],
                    FirstClassSize = UInt16.Parse(values[5]),
                    BusinessClassSize = UInt16.Parse(values[6]),
                    EconomyClassSize = UInt16.Parse(values[7]),

                };

                return passegnerPlane;
        }
    }
}
