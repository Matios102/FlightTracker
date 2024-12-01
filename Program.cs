using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FlightProject.FlightObjects;
using FlightProject.FunctionalObjects;
using FlightProject.FunctionalObjects.DaraSourceEvents;
using FlightTrackerGUI;

namespace FlightProject
{
    public class Program
    {
        static void Main()
        {
            const string AssentsFolderName = "Data";

            //Data sorces initialization
            string FTRfileName = "example_data.ftr.txt"; //File name for the objects data
            string FTREfileName = "example.ftre.txt"; // File name for the uptdates events data
            string jsonFileName = "FlightObjects.json"; // File name for the json serialization
            string dataFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, AssentsFolderName);
            string FTRfilePath = Path.Combine(dataFolderPath, FTRfileName);
            string FTREfilePath = Path.Combine(dataFolderPath, FTREfileName);
            var networkSource = new NetworkSourceSimulator.NetworkSourceSimulator(FTREfilePath, 200, 500); // Network source simulator

            //Functional objects initialization
            List<BaseObject> objectList = FileReader.ReadFTRFile(FTRfilePath);
            Snapshot snapshot = new Snapshot(objectList);
            Snapshot.FlightReference();

            // Initialize listiner to listen for commands
            Listener listener = new Listener(objectList);

            // Initilize logger
            Logger logger = new Logger();

            // Publisher design pattern to listen for network events and notify subscribers
            Publisher publisher = new Publisher(networkSource);

            // Managers to manage the objects
            idManager idManager = new idManager(objectList, logger);
            positionManager positionManager = new positionManager(objectList, logger);
            contactManager contactManager = new contactManager(objectList, logger);
            FlightUpdate flightUpdate = new FlightUpdate(logger, positionManager);

            // Subscribe the managers to the publisher
            publisher.subscribe(idManager);
            publisher.subscribe(positionManager);
            publisher.subscribe(contactManager);

            // Serialize the objects to json file
            Serializer.JSONSerializer(objectList, jsonFileName);

            // Initialize and start the tasks for the network source, listener and the gui update
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

