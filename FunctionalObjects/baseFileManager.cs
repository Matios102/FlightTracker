using System.Collections.Generic;
using FlightProject.FlightObjects.Factories.String_FlightFactories;


namespace FlightProject.FunctionalObjects
{
    public class baseFileManager
    {
        public static readonly Dictionary<string, Factory> FactoryParser = new Dictionary<string, Factory>
        {
            { "C", new CrewFactory() },
            { "P", new PassengerFactory() },
            { "CA", new CargoFactory() },
            { "CP", new CargoPlaneFactory() },
            { "PP", new PassengerPlaneFactory() },
            { "AI", new AirportFactory() },
            { "FL", new FlightFactory() }
        };
    }
}
