using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;
using FlightProject.FlightObjects;
using System.Linq;

// Serializer class for JSON serialization
namespace FlightProject.FunctionalObjects
{
    public class Serializer
    {
        public static void JSONSerializer(List<BaseObject> objectList, string fileName)
        {
            List<Dictionary<string, object>> typedObjectList = new List<Dictionary<string, object>>();
            // Create a typed object list to match required JSON format
            foreach (var obj in objectList)
            {
                if(obj is Flight)
                {
                    Flight flight = obj as Flight;
                    var crewIds = flight.CrewList.Select(crew => crew.ID).ToList();
                    var loadIds = flight.LoadList.Select(load => load.ID).ToList();

                    var typedObject = new Dictionary<string, object>
                    {
                        { "type", flight.GetType().Name },
                        { "OriginID", flight.Origin.ID },
                        { "TargetID", flight.Target.ID },
                        { "TakeOffTime", flight.TakeOffTime },
                        { "LandingTime", flight.LandingTime },
                        { "Longitude", flight.Longitude },
                        { "Latitude", flight.Latitude },
                        { "AMSL", flight.AMSL },
                        { "PlaneID", flight.PlaneID },
                        { "CrewIDs", crewIds },
                        { "LoadIDs", loadIds },
                        {"ID", flight.ID}
                    };
                    typedObjectList.Add(typedObject);
                }
                else
                {
                    var typedObject = new Dictionary<string, object>
                    {
                        { "type", obj.GetType().Name},
                        { "data", obj }
                    };
                    typedObjectList.Add(typedObject);
                }

            }
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string jsonContent = JsonSerializer.Serialize(typedObjectList, jsonSerializerOptions);
            File.WriteAllText(fileName, jsonContent);
            Console.WriteLine($"JSON file {fileName} created successfully.");
        }
    }
}
