using System;
using System.Collections.Generic;
using System.IO;

namespace FlightProject
{
    public class FileReader
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
