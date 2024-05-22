using System.Collections.Generic;
using FlightProject.FlightObjects;

namespace FlightProject.FunctionalObjects.Commands.Wrapers.Factories
{
    public class FlightWraperFactory : baseFactory
    {
        public override Dictionary<string, IFiledWraper> Create()
        {
            Dictionary<string, IFiledWraper> flightWraper = new Dictionary<string, IFiledWraper>();
            flightWraper.Add("ID", new uLongFieldWraper("ID", (x) => x.ID, (BaseObject x, ulong v) => x.ID = v));
            flightWraper.Add("Origin", new flightComponentWraper("Origin", (x) => ((Flight)x).Origin, (BaseObject x, BaseObject v) => ((Flight)x).Origin = (Airport)v));
            flightWraper.Add("Target", new flightComponentWraper("Target", (x) => ((Flight)x).Target, (BaseObject x, BaseObject v) => ((Flight)x).Target = (Airport)v));
            flightWraper.Add("TakeOffTime", new dateTimeFieldWraper("TakeOffTime", (x) => ((Flight)x).TakeOffTime, (BaseObject x, string v) => ((Flight)x).TakeOffTime = v));
            flightWraper.Add("LandingTime", new dateTimeFieldWraper("LandingTime", (x) => ((Flight)x).LandingTime, (BaseObject x, string v) => ((Flight)x).LandingTime = v));
            flightWraper.Add("Longitude", new floatFieldWraper("Longitude", (x) => ((Flight)x).Longitude, (BaseObject x, float v) => ((Flight)x).Longitude = v));
            flightWraper.Add("Latitude", new floatFieldWraper("Latitude", (x) => ((Flight)x).Latitude, (BaseObject x, float v) => ((Flight)x).Latitude = v));
            flightWraper.Add("AMSL", new floatFieldWraper("AMSL", (x) => ((Flight)x).AMSL, (BaseObject x, float v) => ((Flight)x).AMSL = v));
            flightWraper.Add("Plane", new flightComponentWraper("Plane", (x) => ((Flight)x).Plane, (BaseObject x, BaseObject v) => ((Flight)x).Plane = (BasePlane)v));

            return flightWraper;
        }
    }
}
