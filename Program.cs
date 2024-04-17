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
            string FTRfileName = "example_data.ftr.txt";
            string FTREfileName = "example.ftre.txt";
            string jsonFileName = "FlightObjects.json";
            string AssentsFolderName = "Data";
            string dataFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AssentsFolderName);
            string FTRfilePath = Path.Combine(dataFolderPath, FTRfileName);
            string FTREfilePath = Path.Combine(dataFolderPath, FTREfileName);
            var networkSource = new NetworkSourceSimulator.NetworkSourceSimulator(FTREfilePath, 200, 300);

            List<BaseObject> objectList = FileReader.ReadFTRFile(FTRfilePath);
            Snapshot snapshot = new Snapshot(objectList);
            Listener listener = new Listener(objectList, networkSource);
            FlightUpdate flightUpdate = new FlightUpdate(listener);



            Serializer.JSONSerializer(objectList, jsonFileName);


            //var networkSource = new NetworkSourceSimulator.NetworkSourceSimulator(FTRfilePath, 0, 0);
            //Snapshot snapshot = new Snapshot();


            //networkSource.OnNewDataReady += (sender, e) => Snapshot.snapshotManager(sender, e, networkSource);

            var networkTask = new Task(networkSource.Run);
            var listenTask = new Task(listener.ListenForCommands);
            var guiUpdateTask = new Task(flightUpdate.Update);

            networkTask.Start();
            listenTask.Start();
            guiUpdateTask.Start();

            Runner.Run();

            //networkTask.Wait(100);
            listenTask.Wait(100);
            guiUpdateTask.Wait(100);

        }

    }
}

