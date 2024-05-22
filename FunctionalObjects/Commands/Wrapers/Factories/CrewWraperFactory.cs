using System.Collections.Generic;
using FlightProject.FlightObjects;

namespace FlightProject.FunctionalObjects.Commands.Wrapers.Factories
{

    public class CrewWraperFactory : baseFactory
    {
        public override Dictionary<string, IFiledWraper> Create()
        {
            Dictionary<string, IFiledWraper> crewWraper = new Dictionary<string, IFiledWraper>();
            crewWraper.Add("ID", new uLongFieldWraper("ID", (x) => x.ID, (BaseObject x, ulong v) => x.ID = v));
            crewWraper.Add("Name", new stringFieldWraper("Name", (x) => ((Crew)x).Name, (BaseObject x, string v) => ((Crew)x).Name = v));
            crewWraper.Add("Role", new stringFieldWraper("Role", (x) => ((Crew)x).Role, (BaseObject x, string v) => ((Crew)x).Role = v));
            crewWraper.Add("Age", new uLongFieldWraper("Age", (x) => ((Crew)x).Age, (BaseObject x, ulong v) => ((Crew)x).Age = v));
            crewWraper.Add("Phone", new stringFieldWraper("Phone", (x) => ((Crew)x).Phone, (BaseObject x, string v) => ((Crew)x).Phone = v));
            crewWraper.Add("Email", new stringFieldWraper("Email", (x) => ((Crew)x).Email, (BaseObject x, string v) => ((Crew)x).Email = v));
            crewWraper.Add("Practice", new uShortFieldWraper("Practice", (x) => ((Crew)x).Practice, (BaseObject x, ushort v) => ((Crew)x).Practice = v));

            return crewWraper;
        }
    }
}
