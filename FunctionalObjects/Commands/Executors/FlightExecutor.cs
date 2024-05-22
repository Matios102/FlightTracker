using System;
using System.Collections.Generic;
using System.Linq;
using FlightProject.FlightObjects;
using FlightProject.FlightObjects.Builders;

namespace FlightProject.FunctionalObjects.Commands.Executors
{
    public class FlightExecutor : Executor
    {
        public FlightExecutor()
        {
            wrapper = new ClassWraper("Flight");
        }

        public override void Display(List<BaseObject> objects, List<string> properties, List<Condition> conditions, List<string> logicalOperators)
        {
            IEnumerable<BaseObject> flights = objects.OfType<Flight>().ToList();
            if(conditions != null)
            {
                flights = applyConditions(flights, conditions, logicalOperators);

            }
            if (properties.Count == 1 && properties[0] == "*")
            {
                properties = new List<string> { "ID", "Origin", "Target", "TakeOffTime", "LandingTime", "Longitude", "Latitude", "AMSL", "Plane" };
            }
            PrintTable(flights, properties);
        }

        public override void Update(List<BaseObject> objects, Dictionary<string, string> key_value, List<Condition> conditions, List<string> logicalOperators)
        {
            if (wrapper == null)
            {
                throw new ArgumentNullException();
            }
            IEnumerable<BaseObject> toUpdate = objects.OfType<Flight>();
            if (conditions != null)
            {
                toUpdate = applyConditions(toUpdate, conditions, logicalOperators);
            }
            foreach (Flight flight in toUpdate)
            {
                foreach (var key in key_value.Keys)
                {
                    wrapper.SetProperty(flight, key, key_value[key]);
                }
            }
        }

        public override void Add(List<BaseObject> objects, Dictionary<string, string> key_value)
        {
            if(wrapper == null)
            {
                throw new ArgumentNullException();
            }
            FlightBuilder flightBuilder = new FlightBuilder();
            foreach(var key in key_value.Keys)
            {
                flightBuilder.SetProperty(key, key_value[key]);
            }
            objects.Add(flightBuilder.Build());
        }

        public override void Delete(List<BaseObject> objects, List<Condition> conditions, List<string> logicalOperators)
        {
            if(wrapper == null)
            {
                throw new ArgumentNullException();
            }
            IEnumerable<BaseObject> toDelete = objects.OfType<Flight>().ToList();
            if (conditions != null)
            {
                toDelete = applyConditions(toDelete, conditions, logicalOperators);
            }
            foreach (var flight in toDelete)
            {
                objects.Remove(flight);
            }
        }

        public override void Execute(List<BaseObject> objects, Command command)
        {
            switch (command.type)
            {
                case "display":
                    Display(objects, command.object_fileds, command.conditions, command.logical_operators);
                    break;
                case "update":
                    Update(objects, command.key_value, command.conditions, command.logical_operators);
                    break;
                case "add":
                    Add(objects, command.key_value);
                    break;
                case "delete":
                    Delete(objects, command.conditions, command.logical_operators);
                    break;
                default:
                    throw new ArgumentException("Invalid command type");
            }
        }

    }
}
