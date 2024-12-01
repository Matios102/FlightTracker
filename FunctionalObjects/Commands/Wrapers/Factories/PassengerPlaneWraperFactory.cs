using System.Collections.Generic;
using FlightProject.FlightObjects;

// Purpose: This class is responsible for creating the passenger plane wraper containing the fields and their wrapers
namespace FlightProject.FunctionalObjects.Commands.Wrapers.Factories
{
    public class PassengerPlaneWraperFactory : baseFactory
    {
        public override Dictionary<string, IFiledWraper> Create()
        {
            Dictionary<string, IFiledWraper> passengerPlaneWraper = new Dictionary<string, IFiledWraper>();
            passengerPlaneWraper.Add("ID", new uLongFieldWraper("ID", (x) => x.ID, (BaseObject x, ulong v) => x.ID = v));
            passengerPlaneWraper.Add("Serial", new stringFieldWraper("Serial", (x) => ((PassengerPlane)x).Serial, (BaseObject x, string v) => ((PassengerPlane)x).Serial = v));
            passengerPlaneWraper.Add("Model", new stringFieldWraper("Model", (x) => ((PassengerPlane)x).Model, (BaseObject x, string v) => ((PassengerPlane)x).Model = v));
            passengerPlaneWraper.Add("ISO", new stringFieldWraper("ISO", (x) => ((PassengerPlane)x).ISO, (BaseObject x, string v) => ((PassengerPlane)x).ISO = v));
            passengerPlaneWraper.Add("FirstClassSize", new uShortFieldWraper("FirstClassSize", (x) => ((PassengerPlane)x).FirstClassSize, (BaseObject x, ushort v) => ((PassengerPlane)x).FirstClassSize = v));
            passengerPlaneWraper.Add("BusinessClassSize", new uShortFieldWraper("BusinessClassSize", (x) => ((PassengerPlane)x).BusinessClassSize, (BaseObject x, ushort v) => ((PassengerPlane)x).BusinessClassSize = v));
            passengerPlaneWraper.Add("EconomyClassSize", new uShortFieldWraper("EconomyClassSize", (x) => ((PassengerPlane)x).EconomyClassSize, (BaseObject x, ushort v) => ((PassengerPlane)x).EconomyClassSize = v));

            return passengerPlaneWraper;
        }
    }
}
