
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;



namespace FlightProject
{
    public class Program
    {
        static void Main()
        {
            string fileName = "example_data.ftr.txt";
            string AssentsFolderName = "Data";
            string dataFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AssentsFolderName);
            string filePath = Path.Combine(dataFolderPath, fileName);
            
            List<BaseObject> objectList = FileReader.ReadFTRFile(filePath);
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
            File.WriteAllText("FlightObjects.json", jsonContent);
            Console.WriteLine("JSON file created successfully.");
        }
    }

}
