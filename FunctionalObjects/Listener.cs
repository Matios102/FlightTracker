using System;
using System.Collections.Generic;
using System.IO;
using FlightProject.FlightObjects;
using FlightProject.FunctionalObjects.NewsProviders;
using Mapsui.Projections;
using NetworkSourceSimulator;


namespace FlightProject.FunctionalObjects
{
    public class Listener
    {

        private List<BaseObject> FTRobjectList;
        private string logPath;

        public event Action<Listener, PositionUpdateArgs, Flight> FlightUpdateEvent;


        public Listener(List<BaseObject> objects, NetworkSourceSimulator.NetworkSourceSimulator networkSource)
        {
            FTRobjectList = objects;
            networkSource.OnIDUpdate += IDUpdateHendler;
            networkSource.OnPositionUpdate += PositionUpdateHendler;
            networkSource.OnContactInfoUpdate += ContactInfoUpdateHendler;

            logPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            if (!Directory.Exists(logPath))
                Directory.CreateDirectory(logPath);


            DateTime now = DateTime.Now;
            string timestamp = now.ToString("dd_MM_yyyy");
            string logFileName = $"log_{timestamp}.txt";
            logPath = Path.Combine(logPath, logFileName);
            string logMessage = $"LOGG {now.ToLongTimeString()}";
            LogChange(Environment.NewLine + logMessage + Environment.NewLine);
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

        private void IDUpdateHendler(object sender, IDUpdateArgs args)
        {
            BaseObject obj = FTRobjectList.Find(x => x.ID == args.ObjectID);
            if(obj == null)
            {
                LogChange($"Error: Object with ID {args.ObjectID} not found");
                return;
            }
            obj.ID = args.NewObjectID;
            LogChange($"ID updated from {args.ObjectID} to {args.NewObjectID}");
        }

        private void ContactInfoUpdateHendler(object sender, ContactInfoUpdateArgs args)
        {
            BaseObject obj = FTRobjectList.Find(x => x.ID == args.ObjectID);
            if(obj == null)
            {
                LogChange($"Error: Object with ID {args.ObjectID} not found");
                return;
            }
            string oldPhone = "";
            string oldEmail = "";
            if (obj is Passenger)
            {
                Passenger passenger = obj as Passenger;
                oldPhone = passenger.Phone;
                oldEmail = passenger.Email;
                passenger.Phone = args.PhoneNumber;
                passenger.Email = args.EmailAddress;
            }
            else if (obj is Crew)
            {
                Crew crew = obj as Crew;
                oldPhone = crew.Phone;
                oldEmail = crew.Email;
                crew.Phone = args.PhoneNumber;
                crew.Email = args.EmailAddress;
            }
            else
            {
                LogChange($"Error: {obj} is not a passenger or crew");
                return;
            }

            LogChange($"Contact info of [{args.ObjectID}] changed from {oldPhone}, {oldEmail} to {args.PhoneNumber} and {args.EmailAddress}");
        }

        private void PositionUpdateHendler(object sender, PositionUpdateArgs args)
        {
            BaseObject obj = FTRobjectList.Find(x => x.ID == args.ObjectID);
            if(obj == null)
            {
                LogChange($"Error: Object with ID {args.ObjectID} not found");
                return;
            }
            if (obj is Flight)
            {
                Flight flight = obj as Flight;
                FlightUpdateEvent.Invoke(this, args, flight);
            }
            else
            {
                LogChange($"Error: {obj} is not a flight cannot update position");
            }
        }

        public void LogChange(string message)
        {
            File.AppendAllText(logPath, message + Environment.NewLine);
        }

    }
}
