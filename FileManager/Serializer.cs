using System;
using System.Collections.Generic;
using System.Text.Json;
using System.IO;

namespace FlightProject
{
    public class Serializer
    {
        public static void JSONSerializer(List<BaseObject> objectList, string fileName)
        {
            List<Dictionary<string, object>> typedObjectList = new List<Dictionary<string, object>>();
            foreach (var obj in objectList)
            {
                var typedObject = new Dictionary<string, object>
                {
                    { "type", obj.GetType().Name},
                    { "data", obj }
                };
                typedObjectList.Add(typedObject);
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