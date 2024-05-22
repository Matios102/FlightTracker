using System;
using System.Collections.Generic;
using System.Linq;
using FlightProject.FlightObjects;
using FlightProject.FlightObjects.Builders;

namespace FlightProject.FunctionalObjects.Commands.Executors
{
    public class PassengerPlaneExecutor : Executor
    {
        public PassengerPlaneExecutor()
        {
            wrapper = new ClassWraper("PassengerPlane");
        }
        public override void Display(List<BaseObject> objects, List<string> properties, List<Condition> conditions, List<string> logicalOperators)
        {
            IEnumerable<BaseObject> passengerPlanes = objects.OfType<PassengerPlane>().ToList();
            if (conditions != null)
            {
                passengerPlanes = applyConditions(passengerPlanes, conditions, logicalOperators);

            }
            if (properties.Count == 1 && properties[0] == "*")
            {
                properties = new List<string> { "ID", "Serial", "Model", "ISO", "FirstClassSize", "BusinessClassSize", "EconomyClassSize"};
            }
            PrintTable(passengerPlanes, properties);
        }

        public override void Update(List<BaseObject> objects, Dictionary<string, string> key_value, List<Condition> conditions, List<string> logicalOperators)
        {
            IEnumerable<BaseObject> passengerPlanes = objects.OfType<PassengerPlane>().ToList();
            if (conditions != null)
            {
                passengerPlanes = applyConditions(passengerPlanes, conditions, logicalOperators);
            }
            foreach (var passengerPlane in passengerPlanes)
            {
                foreach (var key in key_value.Keys)
                {
                    wrapper.SetProperty(passengerPlane, key, key_value[key]);
                }
            }
        }

        public override void Add(List<BaseObject> objects, Dictionary<string, string> key_value)
        {
            if (wrapper == null)
            {
                throw new ArgumentNullException();
            }
            PassengerPlaneBuilder builder = new PassengerPlaneBuilder();
            foreach (var key in key_value.Keys)
            {
                builder.SetProperty(key, key_value[key]);
            }
        }

        public override void Delete(List<BaseObject> objects, List<Condition> conditions, List<string> logicalOperators)
        {
            if(wrapper == null)
            {
                throw new ArgumentNullException();
            }
            IEnumerable<BaseObject> toDelete = objects.OfType<PassengerPlane>();
            if (conditions != null)
            {
                toDelete = applyConditions(toDelete, conditions, logicalOperators);
            }
            foreach (var passengerPlane in toDelete)
            {
                objects.Remove(passengerPlane);
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
