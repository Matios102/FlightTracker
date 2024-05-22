using System.Collections.Generic;
using FlightProject.FlightObjects;

namespace FlightProject.FunctionalObjects.Commands.Wrapers.Factories
{
    public class AirportWraperFactory : baseFactory
    {
        public override Dictionary<string, IFiledWraper> Create()
        {
            Dictionary<string, IFiledWraper> airportWraper = new Dictionary<string, IFiledWraper>();
            airportWraper.Add("ID", new uLongFieldWraper("ID", (x) => x.ID, (BaseObject x, ulong v) => x.ID = v));
            airportWraper.Add("Name", new stringFieldWraper("Name", (x) => ((Airport)x).Name, (BaseObject x, string v) => ((Airport)x).Name = v));
            airportWraper.Add("Code", new stringFieldWraper("Code", (x) => ((Airport)x).Code, (BaseObject x, string v) => ((Airport)x).Code = v));
            airportWraper.Add("Longitude", new floatFieldWraper("Longitude", (x) => ((Airport)x).Longitude, (BaseObject x, float v) => ((Airport)x).Longitude = v));
            airportWraper.Add("Latitude", new floatFieldWraper("Latitude", (x) => ((Airport)x).Latitude, (BaseObject x, float v) => ((Airport)x).Latitude = v));
            airportWraper.Add("AMSL", new floatFieldWraper("AMSL", (x) => ((Airport)x).AMSL, (BaseObject x, float v) => ((Airport)x).AMSL = v));
            airportWraper.Add("ISO", new stringFieldWraper("Country", (x) => ((Airport)x).ISO, (BaseObject x, string v) => ((Airport)x).ISO = v));

            return airportWraper;
        }
    }
}
