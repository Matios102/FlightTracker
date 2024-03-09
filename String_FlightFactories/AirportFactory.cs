using System;
using System.Text;


namespace FlightProject
{
    public class AirportFactory : Factory
    {
        public override Airport Create(string[] values)
        {

            if (values.Length < 8)
            {
                throw new ArgumentException("invalid values");
            }

            Airport airport = new Airport
            {
                ID = UInt64.Parse(values[1]),
                Name = values[2],
                Code = values[3],
                Longitude = Single.Parse(values[4]),
                Latitude = Single.Parse(values[5]),
                AMSL = Single.Parse(values[6]),
                ISO = values[7]                
            };
            return airport;
        }

    }
}