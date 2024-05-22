using System.Collections.Generic;
using FlightProject.FlightObjects;
using FlightProject.FunctionalObjects.Commands.Wrapers;
using FlightProject.FunctionalObjects.Commands.Wrapers.Factories;

namespace FlightProject.FunctionalObjects.Commands
{

    public class ClassWraper
    {
        private Dictionary<string, baseFactory> factories = new Dictionary<string, baseFactory>
        {
            { "Flight", new FlightWraperFactory()},
            { "Airport", new AirportWraperFactory() },
            { "PassengerPlane", new PassengerPlaneWraperFactory() },
            { "CargoPlane", new CargoPlaneWraperFactory() },
            { "Crew", new CrewWraperFactory() },
            { "Passenger", new PassengerWraperFactory() },
            { "Cargo", new CargoWraperFactory()}
        };
        public string className { get; set; }
        public Dictionary<string, IFiledWraper> Properties { get; set; }
        public ClassWraper(string className)
        {
            this.className = className;
            Properties = new Dictionary<string, IFiledWraper>();
            if (factories.ContainsKey(className))
            {
                Properties = factories[className].Create();
            }
        }



        public string GetPropertyValue(BaseObject obj, string propertyName)
        {
            propertyName = propertyName.Replace(" ", "");
            return Properties[propertyName].GetValue(obj);
        }

        public void SetProperty(BaseObject obj, string propertyName, string value)
        {
            Properties[propertyName].SetValue(obj, value);
        }

        public bool CompareProperty(BaseObject obj, string propertyName, string value, string op)
        {
            return Properties[propertyName].Compare(obj, propertyName, value, op);
        }
    }
}
