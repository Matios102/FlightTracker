
using FlightProject.FlightObjects;
using FlightProject.FlightObjects.Factories.Byte_FlightFactories;
using NetworkSourceSimulator;
using System;
using System.Collections.Generic;
using System.Text;


namespace FlightProject.FunctionalObjects
{
    // Purpose: Represents a snapshot of the current state of the system.
    public class Snapshot : baseFileManager
    {
        // List of all objects in the system
        public static List<BaseObject> objectList = new List<BaseObject>();

        public Snapshot()
        {
            objectList = new List<BaseObject>();
        }

        public Snapshot(List<BaseObject> objectList)
        {
            Snapshot.objectList = objectList;
        }
        public static readonly new Dictionary<string, Byte_Factory> FactoryParser = new Dictionary<string, Byte_Factory>
        {
            { "NCR", new Byte_CrewFactory() },
            { "NPA", new Byte_PassengerFactory() },
            { "NCA", new Byte_CargoFactory() },
            { "NCP", new Byte_CargoPlaneFactory() },
            { "NPP", new Byte_PassengerPlaneFactory() },
            { "NAI", new Byte_AirportFactory() },
            { "NFL", new Byte_FlightFactory() }
        };

        // Event for adding new flights to the flights lists to be displayed
        public static event Action<Flight> newFlightReady;

        // Delegate for network source simulator
        public delegate void OnNewDataReadyHandler(object sender, NewDataReadyArgs e, NetworkSourceSimulator.NetworkSourceSimulator networkSource);

        // Method to manage the new data
        public static void snapshotManager(object sender, NewDataReadyArgs e, NetworkSourceSimulator.NetworkSourceSimulator networkSource)
        {
            Message message = networkSource.GetMessageAt(e.MessageIndex);
            byte[] byteArray = message.MessageBytes;
            string identifier = Encoding.ASCII.GetString(byteArray, 0, 3);
            Console.WriteLine($"New data ready: {identifier}");

            if (!FactoryParser.TryGetValue(identifier, out var factory))
            {
                throw new ArgumentException($"Unknown type: {identifier}");
            }

            BaseObject parsedObject = factory.Create(byteArray);

            if (parsedObject is Flight)
            {
                Flight flight = parsedObject as Flight;
                newFlightReady?.Invoke(flight);
            }

            if (parsedObject != null)
            {
                objectList.Add(parsedObject);
            }
        }

        // Method to reference the flights to the airports, crew, load and plane
        public static void FlightReference()
        {
            FlightReference reference = new FlightReference(objectList);

            foreach (var obj in objectList)
            {
                if (obj is Flight)
                {
                    Flight flight = obj as Flight;
                    flight.Origin = reference.FindAirportByID(flight.Origin.ID);
                    flight.Target = reference.FindAirportByID(flight.Target.ID);
                    flight.CrewList = reference.FindCrewListByID(flight.CrewIDs);
                    flight.LoadList = reference.FindLoadListByID(flight.LoadIDs);
                    flight.Longitude = flight.Origin.Longitude;
                    flight.Latitude = flight.Origin.Latitude;
                    flight.AMSL = flight.Origin.AMSL;
                    flight.Plane = reference.FindPlaneByID(flight.PlaneID);
                }
            }
        }
    }
}
