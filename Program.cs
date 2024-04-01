using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FlightProject.FlightObjects;
using FlightProject.FunctionalObjects;
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
            FlightUpdate flightUpdate = new FlightUpdate();
            Listener listener = new Listener(objectList);


            Serializer.JSONSerializer(objectList, jsonFileName);


            var networkSource = new NetworkSourceSimulator.NetworkSourceSimulator(filePath, 0, 0);
            Snapshot snapshot = new Snapshot();

            networkSource.OnNewDataReady += (sender, e) => Snapshot.snapshotManager(sender, e, networkSource);

            var networkTask = new Task(networkSource.Run);
            var listenTask = new Task(listener.ListenForCommands);
            var guiUpdateTask = new Task(flightUpdate.Update);

            networkTask.Start();
            listenTask.Start();
            guiUpdateTask.Start();

            Runner.Run();

            networkTask.Wait(100);
            listenTask.Wait(100);
            guiUpdateTask.Wait(100);

        }

    }
}

