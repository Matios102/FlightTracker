using System;
using System.Collections.Generic;
using System.Linq;
using FlightProject.FlightObjects;
using FlightProject.FlightObjects.Builders;

namespace FlightProject.FunctionalObjects.Commands.Executors
{
    public class AirportExecutor : Executor
    {
        public AirportExecutor()
        {
            wrapper = new ClassWraper("Airport");
        }
        public override void Display(List<BaseObject> objects, List<string> properties, List<Condition> conditions, List<string> logicalOperators)
        {
            IEnumerable<BaseObject> airports = objects.OfType<Airport>().ToList();
            if (conditions != null)
            {
                airports = applyConditions(airports, conditions, logicalOperators);

            }
            if (properties.Count == 1 && properties[0] == "*")
            {
                properties = new List<string> { "ID", "Name", "Code", "Longitude", "Latitude", "AMSL", "ISO"};
            }
            PrintTable(airports, properties);
        }

        public override void Update(List<BaseObject> objects, Dictionary<string, string> key_value, List<Condition> conditions, List<string> logicalOperators)
        {
            if(wrapper == null)
            {
                throw new ArgumentNullException();
            }
            IEnumerable<BaseObject> toUpdate = objects.OfType<Airport>();
            if (conditions != null)
            {
                toUpdate = applyConditions(toUpdate, conditions, logicalOperators);
            }
            foreach (Airport airport in toUpdate)
            {
                foreach (var key in key_value.Keys)
                {
                    wrapper.SetProperty(airport, key, key_value[key]);
                }
            }
        }

        public override void Add(List<BaseObject> objects, Dictionary<string, string> key_value)
        {
            if (wrapper == null)
            {
                throw new ArgumentNullException();
            }
            AirportBuilder builder = new AirportBuilder();
            foreach (var key in key_value.Keys)
            {
                builder.SetProperty(key, key_value[key]);
            }
            objects.Add(builder.Build());

        }

        public override void Delete(List<BaseObject> objects, List<Condition> conditions, List<string> logicalOperators)
        {
            if(wrapper == null)
            {
                throw new ArgumentNullException();
            }
            IEnumerable<BaseObject> toDelete = objects.OfType<Airport>();
            if (conditions != null)
            {
                toDelete = applyConditions(toDelete, conditions, logicalOperators);
            }
            foreach (Airport airport in toDelete)
            {
                objects.Remove(airport);
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
                    throw new NotImplementedException("Invalid command type");
            }
        }
    }
}
