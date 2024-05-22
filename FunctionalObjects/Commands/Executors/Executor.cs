using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlightProject.FlightObjects;

namespace FlightProject.FunctionalObjects.Commands
{
    public abstract class Executor
    {
        public ClassWraper wrapper;

        public abstract void Execute(List<BaseObject> objects, Command command);
        public abstract void Display(List<BaseObject> objects, List<string> properties, List<Condition> conditions, List<string> logicalOperators);

        public abstract void Update(List<BaseObject> objects, Dictionary<string, string> key_value, List<Condition> conditions, List<string> logicalOperators);

        public abstract void Add(List<BaseObject> objects, Dictionary<string, string> key_value);

        public abstract void Delete(List<BaseObject> objects, List<Condition> conditions, List<string> logicalOperators);

        public List<BaseObject> Compare(IEnumerable<BaseObject> data, Condition condition)
        {
            List<BaseObject> result = new List<BaseObject>();
            result = data.Where(obj => wrapper.CompareProperty(obj, condition.property, condition.value, condition.oprator)).ToList();
            return result;
        }
        public  List<BaseObject> applyConditions(IEnumerable<BaseObject> data, List<Condition> conditions, List<string> logicalOperators)
        {
            List<BaseObject> result = new List<BaseObject>();
            List<BaseObject> partialResult = new List<BaseObject>();

            for(int i = 0; i < conditions.Count; i++)
            {
                partialResult = Compare(data, conditions[i]);
                if(i == 0)
                {
                    result = partialResult;
                }
                else
                {
                    if(logicalOperators[i - 1] == "and")
                    {
                        result = result.Intersect(partialResult).ToList();
                    }
                    else if (logicalOperators[i - 1] == "or")
                    {
                        result = result.Union(partialResult).ToList();
                    }
                }
            }

            return result;
        }

        protected void PrintTable(IEnumerable<BaseObject> data, List<string> properties)
        {

            if (wrapper == null)
            {
                throw new ArgumentNullException();
            }
            StringBuilder sb = new StringBuilder();
            List<int> columnWidths = new List<int>();
            for (int i = 0; i < properties.Count(); i++)
            {
                columnWidths.Add(properties[i].Length);
            }
            foreach (BaseObject f in data)
            {
                for (int i = 0; i < properties.Count(); i++)
                {
                    int valueLength = wrapper.GetPropertyValue(f, properties[i]).Length;
                    if (valueLength > columnWidths[i])
                    {
                        columnWidths[i] = valueLength;
                    }
                }
            }
            for (int i = 0; i < properties.Count(); i++)
            {
                sb.Append(" " + properties[i].PadRight(columnWidths[i]) + " | ");
            }
            Console.WriteLine(sb.ToString());
            foreach (BaseObject f in data)
            {
                sb.Clear();
                for (int i = 0; i < properties.Count(); i++)
                {
                    sb.Append(" " + wrapper.GetPropertyValue(f, properties[i]).PadLeft(columnWidths[i]) + " | ");
                }
                Console.WriteLine(sb.ToString());
            }
        }

    }
}
