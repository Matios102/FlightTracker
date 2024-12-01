using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FlightProject.FlightObjects;
using FlightProject.FunctionalObjects.Commands.Executors;

// Purpose: This class is responsible for processing the query
namespace FlightProject.FunctionalObjects.Commands
{
    public class QueryProcessor
    {
        public static Dictionary<string, Executor> executors = new Dictionary<string, Executor>()
        {
            {"Flight", new FlightExecutor()},
            {"Passenger", new PassengerExecutor()},
            {"Crew", new CrewExecutor()},
            {"Airport", new AirportExecutor()},
            {"PassengerPlane", new PassengerPlaneExecutor()},
            {"CargoPlane", new CargoPlaneExecutor()},
            {"Cargo", new CargoExecutor()},
        };
        public static void ProcessQuery(List<BaseObject> data, string query)
        {
            string pattern = "(display|update|delete|add){1}.*";
            Regex re = new Regex(pattern);
            var m = re.Match(query);
            Command parsedQuery = null;
            if (!m.Success)
            {
                Console.WriteLine("Not matched");
                return;
            }
            try
            {
                switch (m.Groups[1].Value)
                {
                    case "display":
                        parsedQuery = QueryParser.Display(query);
                        break;
                    case "update":
                        parsedQuery = QueryParser.Update(query);
                        break;
                    case "add":
                        parsedQuery = QueryParser.Add(query);
                        break;
                    case "delete":
                        parsedQuery = QueryParser.Delete(query);
                        break;
                };
                Executor executor = executors[parsedQuery.object_class];
                executor.Execute(data, parsedQuery);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return;
            }
        }
    }
}
