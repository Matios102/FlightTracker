using System;
using FlightProject.FlightObjects;

// Purpose: This class is responsible for creating the string field wraper
namespace FlightProject.FunctionalObjects.Commands.Wrapers
{
    public class stringFieldWraper : IFiledWraper
    {
        public delegate string Getter(BaseObject obj);

        public delegate void Setter(BaseObject obj, string value);

        public string FieldName { get; }
        public Getter Get;
        public Setter Set;

        public stringFieldWraper(string fieldName, Getter get, Setter set)
        {
            FieldName = fieldName;
            Get = get;
            Set = set;
        }

        public string GetValue(BaseObject obj)
        {
            return Get(obj);
        }

        public void SetValue(BaseObject obj, string value)
        {
            Set(obj, value);
        }

        public bool Compare(BaseObject obj, string properyName, string value, string op)
        {
            string property = Get(obj);
            switch (op)
            {
                case "=":
                    return property == value;
                case "!=":
                    return property != value;
                case ">":
                    return property.CompareTo(value) > 0;
                case "<":
                    return property.CompareTo(value) < 0;
                case ">=":
                    return property.CompareTo(value) >= 0;
                case "<=":
                    return property.CompareTo(value) <= 0;
                default:
                    throw new Exception("Unknown operator");
            }
        }
    }
}
