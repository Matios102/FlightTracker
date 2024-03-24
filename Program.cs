
// using System;
// using System.Collections.Generic;
// using System.IO;
// using System.Linq;
// using System.Threading;
// using System.Threading.Tasks;
// using Avalonia.Remote.Protocol;
// using FlightTrackerGUI;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FlightTrackerGUI;




namespace FlightProject
{
    public class Program
    {
        static void Main()
        {
            string fileName = "example_data.ftr.txt";
            string jsonFileName = "FlightObjects.json";
            string AssentsFolderName = "Data";
            string dataFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AssentsFolderName);
            string filePath = Path.Combine(dataFolderPath, fileName);
            List<BaseObject> objectList = FileReader.ReadFTRFile(filePath);
            FlightReference reference = new FlightReference(objectList);
            Flight_FlightsGUIData_Adapter adapter = new Flight_FlightsGUIData_Adapter();


            Serializer.JSONSerializer(objectList, jsonFileName);


            var networkSource = new NetworkSourceSimulator.NetworkSourceSimulator(filePath, 0, 0);
            Snapshot snapshot = new Snapshot();

            networkSource.OnNewDataReady += (sender, e) => Snapshot.snapshotManager(sender, e, networkSource);

            var networkTask = new Task(networkSource.Run);
            var listenTask = new Task(snapshot.ListenForCommands);
            var guiUpdateTask = new Task(adapter.Update);
            var guiDisplay = new Task(Runner.Run);

            networkTask.Start();
            listenTask.Start();
            guiUpdateTask.Start();
            guiDisplay.Start();

            Runner.Run();

            networkTask.Wait(100);
            listenTask.Wait(100);
            guiUpdateTask.Wait(100);
        }

    }
}

