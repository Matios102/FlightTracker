
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
            string filePath = "./example_data.ftr.txt";
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
