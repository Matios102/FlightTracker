using System;
using System.Collections.Generic;
using FlightProject.FlightObjects;
using FlightProject.FunctionalObjects.NewsProviders;


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
                string command = Console.ReadLine()?.ToLowerInvariant();

                switch (command)
                {
                    case "print":
                        printSnapshot();
                        break;
                    case "exit":
                        Snapshot.objectList.Clear();
                        Console.WriteLine("Exiting");
                        return;
                    case "report":
                        GenerateReport();
                        break;

                    default:
                        Console.WriteLine("Unknown command. Available commands: print, report, exit");
                        break;
                }
            }
        }

        public void printSnapshot()
        {
            string timestamp = DateTime.Now.ToString("HH_mm_ss");
            string fileName = $"snapshot_{timestamp}.json";
            Serializer.JSONSerializer(Snapshot.objectList, fileName);
        }

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
