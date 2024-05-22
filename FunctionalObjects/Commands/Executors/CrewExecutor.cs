using System;
using System.Collections.Generic;
using System.Linq;
using FlightProject.FlightObjects;
using FlightProject.FlightObjects.Builders;

namespace FlightProject.FunctionalObjects.Commands.Executors
{
    public class CrewExecutor : Executor
    {
        public CrewExecutor()
        {
            wrapper = new ClassWraper("Crew");
        }
        public override void Display(List<BaseObject> objects, List<string> properties, List<Condition> conditions, List<string> logicalOperators)
        {
            IEnumerable<BaseObject> crews = objects.OfType<Crew>().ToList();
            if (conditions != null)
            {
                crews = applyConditions(crews, conditions, logicalOperators);

            }
            if (properties.Count == 1 && properties[0] == "*")
            {
                properties = new List<string> { "ID", "Name", "Age", "Phone", "Email", "Role", "Practice" };
            }
            PrintTable(crews, properties);
        }

        public override void Update(List<BaseObject> objects, Dictionary<string, string> key_value, List<Condition> conditions, List<string> logicalOperators)
        {
            IEnumerable<BaseObject> crews = objects.OfType<Crew>().ToList();
            if (conditions != null)
            {
                crews = applyConditions(crews, conditions, logicalOperators);
            }
            foreach (var crew in crews)
            {
                foreach (var key in key_value.Keys)
                {
                    wrapper.SetProperty(crew, key, key_value[key]);
                }
            }
        }

        public override void Add(List<BaseObject> objects, Dictionary<string, string> key_value)
        {
            if (wrapper == null)
            {
                throw new ArgumentNullException();
            }
            CrewBuilder builder = new CrewBuilder();
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
            IEnumerable<BaseObject> toDelete = objects.OfType<Crew>().ToList();
            if (conditions != null)
            {
                toDelete = applyConditions(toDelete, conditions, logicalOperators);
            }
            foreach (var crew in toDelete)
            {
                objects.Remove(crew);
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
