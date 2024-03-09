using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightProject.FileManager
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