using System;
using System.Collections.Generic;
using FlightProject.FlightObjects;
using FlightProject.FunctionalObjects.Commands;
using FlightProject.FunctionalObjects.NewsProviders;

// This class is responsible for listening to user input and executing commands
namespace FlightProject.FunctionalObjects
{
    public class Listener
    {
        private List<BaseObject> FTRobjectList;
        public Listener(List<BaseObject> objects)
        {
            FTRobjectList = objects;
        }
        public void ListenForCommands()
        {
            while (true)
            {
                string command = Console.ReadLine();
                switch (command)
                {
                    case "print":
                        printSnapshot(); // Serialize the current state of the objects
                        break;
                    case "exit":
                        Snapshot.objectList.Clear(); // Clear the snapshot and exit
                        Console.WriteLine("Exiting");
                        return;
                    case "report":
                        GenerateReport(); // Generate a report
                        break;

                    default:
                        Query(command); // Process the command
                        break;

                }
            }
        }

        public void Query(string input)
        {
            QueryProcessor.ProcessQuery(FTRobjectList, input);
        }

        // Serialize the current state of the objects
        public void printSnapshot()
        {
            string timestamp = DateTime.Now.ToString("HH_mm_ss");
            string fileName = $"snapshot_{timestamp}.json";
            Serializer.JSONSerializer(Snapshot.objectList, fileName);
        }

        // Generate a report
        public void GenerateReport()
        {
            List<IReportable> reportables = new List<IReportable>();
            foreach (var obj in FTRobjectList)
            {
                if (obj is IReportable)
                    reportables.Add(obj as IReportable);
            }
            Television tv1 = new Television("Abelian Television");
            Television tv2 = new Television("Channel TV-Tensor");

            Radio r1 = new Radio("Quantifier radio");
            Radio r2 = new Radio("Shmem radio");

            Newspaper n1 = new Newspaper("Categories Journal");
            Newspaper n2 = new Newspaper("Polytechnical Gazette");

            List<INewsVisitor> providers = new List<INewsVisitor> { tv1, tv2, r1, r2, n1, n2 };

            NewsGenerator newsGenerator = new NewsGenerator(providers, reportables);

            foreach (var news in newsGenerator)
            {
                Console.WriteLine(news);
            }
        }
    }
}
