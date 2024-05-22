using System.Collections.Generic;
using FlightProject.FlightObjects;

namespace FlightProject.FunctionalObjects.Commands.Wrapers.Factories
{
    public class CargoPlaneWraperFactory : baseFactory
    {
        public override Dictionary<string, IFiledWraper> Create()
        {
            Dictionary<string, IFiledWraper> cargoPlaneWraper = new Dictionary<string, IFiledWraper>();
            cargoPlaneWraper.Add("ID", new uLongFieldWraper("ID", (x) => x.ID, (BaseObject x, ulong v) => x.ID = v));
            cargoPlaneWraper.Add("Serial", new stringFieldWraper("Serial", (x) => ((CargoPlane)x).Serial, (BaseObject x, string v) => ((CargoPlane)x).Serial = v));
            cargoPlaneWraper.Add("Model", new stringFieldWraper("Model", (x) => ((CargoPlane)x).Model, (BaseObject x, string v) => ((CargoPlane)x).Model = v));
            cargoPlaneWraper.Add("ISO", new stringFieldWraper("ISO", (x) => ((CargoPlane)x).ISO, (BaseObject x, string v) => ((CargoPlane)x).ISO = v));
            cargoPlaneWraper.Add("MaxLoad", new floatFieldWraper("MaxLoad", (x) => ((CargoPlane)x).MaxLoad, (BaseObject x, float v) => ((CargoPlane)x).MaxLoad = v));

            return cargoPlaneWraper;
        }
    }
}
