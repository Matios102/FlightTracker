using System.Collections.Generic;
using FlightProject.FlightObjects;

namespace FlightProject.FunctionalObjects.Commands.Wrapers.Factories
{
    public class CargoWraperFactory : baseFactory
    {
        public override Dictionary<string, IFiledWraper> Create()
        {
            Dictionary<string, IFiledWraper> cargoWraper = new Dictionary<string, IFiledWraper>();
            cargoWraper.Add("ID", new uLongFieldWraper("ID", (x) => x.ID, (BaseObject x, ulong v) => x.ID = v));
            cargoWraper.Add("Weight", new floatFieldWraper("Weight", (x) => ((Cargo)x).Weight, (BaseObject x, float v) => ((Cargo)x).Weight = v));
            cargoWraper.Add("Code", new stringFieldWraper("Code", (x) => ((Cargo)x).Code, (BaseObject x, string v) => ((Cargo)x).Code = v));
            cargoWraper.Add("Description", new stringFieldWraper("Description", (x) => ((Cargo)x).Description, (BaseObject x, string v) => ((Cargo)x).Description = v));

            return cargoWraper;
        }
    }
}
