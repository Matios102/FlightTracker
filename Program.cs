
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;




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
            Serializer.JSONSerializer(objectList, jsonFileName);

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            var networkSource = new NetworkSourceSimulator.NetworkSourceSimulator(filePath, 300, 500);
            Snapshot snapshot = new Snapshot();

            networkSource.OnNewDataReady += (sender, e) => Snapshot.snapshotManager(sender, e, networkSource);

            var networkTask = new Task(networkSource.Run);
            networkTask.Start();

            snapshot.ListenForCommands();
            networkTask.Wait(100);
        }
    }
}

