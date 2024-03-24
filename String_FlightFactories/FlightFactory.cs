using System;
using System.Collections.Generic;
using System.Text;


namespace FlightProject
{
    public class FlightFactory : Factory
    {
        public override Flight Create(string[] values)
        {

            if (values.Length < 12)
            {
                throw new ArgumentException("invalid values");
            }

                values[10] = values[10].Trim('[', ']');
                string[] crewIDs_string = values[10].Split(';');
                List<UInt64> crewIDs_uint64 = new List<UInt64>();

                foreach (var id in crewIDs_string)
                {
                    crewIDs_uint64.Add(UInt64.Parse(id));
                }

                values[11] = values[11].Trim('[', ']');
                string[] loadIDs_string = values[11].Split(';');
                List<UInt64> loadIDs_uint64 = new List<UInt64>();

                foreach (var id in loadIDs_string)
                {
                    loadIDs_uint64.Add(UInt64.Parse(id));
                }

                Flight flight = new Flight
                {
                    ID = UInt64.Parse(values[1]),
                    Origin = new Airport(),
                    Target = new Airport(),
                    TakeOffTime = values[4],
                    LandingTime = values[5],
                    Longitude = Single.Parse(values[6]),
                    Latitude = Single.Parse(values[7]),
                    AMSL = Single.Parse(values[8]),
                    PlaneID = UInt64.Parse(values[9]),
                    CrewIDs = crewIDs_uint64.ToArray(),
                    CrewList = new List<Crew>(),
                    LoadIDs = loadIDs_uint64.ToArray(),
                    LoadList = new List<BaseObject>(),
                };
                flight.Origin.ID = UInt64.Parse(values[2]);
                flight.Target.ID = UInt64.Parse(values[3]);

                return flight;
        }
    }
}
