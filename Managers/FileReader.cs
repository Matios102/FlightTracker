using System;
using System.Collections.Generic;
using System.IO;
using FlightProject.FileManager;

namespace FlightProject
{
    public class FileReader : baseFileManager
    {

        public static List<BaseObject> ReadFTRFile(string filePath)
        {
            List<BaseObject> objectList = new List<BaseObject>();

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                foreach (string line in lines)
                {
                    string[] values = line.Split(',');

                    if (values.Length <= 0)
                    {
                        throw new ArgumentException($"Invalid line: {values}");
                    }

                    if (!FactoryParser.TryGetValue(values[0], out var factory))
                    {
                        throw new ArgumentException($"Unknown type: {values[0]}");
                    }

                    BaseObject parsedObject = factory.Create(values);

                    if (parsedObject is Flight)
                    {
                        FlightReference reference = new FlightReference(objectList);
                        Flight flight = parsedObject as Flight;
                        flight.Origin = reference.FindAirportByID(flight.Origin.ID);
                        flight.Target = reference.FindAirportByID(flight.Target.ID);
                        flight.CrewList = reference.FindCrewListByID(flight.CrewIDs);
                        flight.LoadList = reference.FindLoadListByID(flight.LoadIDs);
                    }
                    if (parsedObject != null)
                    {
                        objectList.Add(parsedObject);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading file: {ex.Message}");
            }

            return objectList;
        }
    }
}
