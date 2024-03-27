using System;
namespace FlightProject
{
    public class CrewFactory : Factory
    {
        public override Crew Create(string[] values)
        {
            if (values.Length < 8)
            {
                throw new ArgumentException("invalid values");
            }


            Crew crew = new Crew
            {
                ID = UInt64.Parse(values[1]),
                Name = values[2],
                Age = UInt64.Parse(values[3]),
                Phone = values[4],
                Email = values[5],
                Practice = UInt16.Parse(values[6]),
                Role = values[7]
            };
            return crew;


        }
    }
}
