using System.Collections.Generic;
using FlightProject.FlightObjects;

// Purpose: This class is responsible for creating the passenger wraper containing the fields and their wrapers
namespace FlightProject.FunctionalObjects.Commands.Wrapers.Factories
{
    public class PassengerWraperFactory : baseFactory
    {
        public override Dictionary<string, IFiledWraper> Create()
        {
            Dictionary<string, IFiledWraper> passengerWraper = new Dictionary<string, IFiledWraper>();
            passengerWraper.Add("ID", new uLongFieldWraper("ID", (x) => x.ID, (BaseObject x, ulong v) => x.ID = v));
            passengerWraper.Add("Name", new stringFieldWraper("Name", (x) => ((Passenger)x).Name, (BaseObject x, string v) => ((Passenger)x).Name = v));
            passengerWraper.Add("Age", new uLongFieldWraper("Age", (x) => ((Passenger)x).Age, (BaseObject x, ulong v) => ((Passenger)x).Age = v));
            passengerWraper.Add("Phone", new stringFieldWraper("Phone", (x) => ((Passenger)x).Phone, (BaseObject x, string v) => ((Passenger)x).Phone = v));
            passengerWraper.Add("Email", new stringFieldWraper("Email", (x) => ((Passenger)x).Email, (BaseObject x, string v) => ((Passenger)x).Email = v));
            passengerWraper.Add("Class", new stringFieldWraper("Class", (x) => ((Passenger)x).Class, (BaseObject x, string v) => ((Passenger)x).Class = v));
            passengerWraper.Add("Miles", new uLongFieldWraper("Miles", (x) => ((Passenger)x).Miles, (BaseObject x, ulong v) => ((Passenger)x).Miles = v));

            return passengerWraper;
        }
    }
}
