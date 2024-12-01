using System;
using System.Collections.Generic;
using System.Linq;
using FlightProject.FlightObjects;
using FlightProject.FlightObjects.Builders;

namespace FlightProject.FunctionalObjects.Commands.Executors
{
    public class CargoExecutor : Executor
    {
        public CargoExecutor()
        {
            wrapper = new ClassWraper("Cargo");
        }
        public override void Add(List<BaseObject> objects, Dictionary<string, string> key_value)
        {
            if(wrapper == null)
            {
                throw new ArgumentNullException();
            }
            CargoBuilder cargoBuilder = new CargoBuilder();
            foreach (var key in key_value.Keys)
            {
                cargoBuilder.SetProperty(key, key_value[key]);
            }
            objects.Add(cargoBuilder.Build());
        }

        public override void Display(List<BaseObject> objects, List<string> properties, List<Condition> conditions, List<string> logicalOperators)
        {
            if(wrapper == null)
            {
                throw new ArgumentNullException();
            }
            IEnumerable<BaseObject> cargos = objects.OfType<Cargo>().ToList();
            if (conditions != null)
            {
                cargos = applyConditions(cargos, conditions, logicalOperators);
            }
            if (properties.Count == 1 && properties[0] == "*")
            {
                properties = new List<string> { "ID", "Weight", "Code", "Description"};
            }
            PrintTable(cargos, properties);
        }

        public override void Update(List<BaseObject> objects, Dictionary<string, string> key_value, List<Condition> conditions, List<string> logicalOperators)
        {
            if(wrapper == null)
            {
                throw new ArgumentNullException();
            }
            IEnumerable<BaseObject> cargos = objects.OfType<Cargo>().ToList();
            if (conditions != null)
            {
                cargos = applyConditions(cargos, conditions, logicalOperators);
            }
            foreach (var cargo in cargos)
            {
                foreach (var key in key_value.Keys)
                {
                    wrapper.SetProperty(cargo, key, key_value[key]);
                }
            }
        }

        public override void Delete(List<BaseObject> objects, List<Condition> conditions, List<string> logicalOperators)
        {
            if(wrapper == null)
            {
                throw new ArgumentNullException();
            }
            List<BaseObject> toDelete = objects.OfType<Cargo>().Cast<BaseObject>().ToList();
            if (conditions.Count != 0)
            {
                toDelete = applyConditions(toDelete, conditions, logicalOperators);
            }
            foreach (var cargo in toDelete)
            {
                objects.Remove(cargo);
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
