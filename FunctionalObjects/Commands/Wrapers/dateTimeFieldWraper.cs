using System;
using FlightProject.FlightObjects;

// Purpose: This class is responsible for creating the date time field wraper
namespace FlightProject.FunctionalObjects.Commands.Wrapers
{
    public class dateTimeFieldWraper : IFiledWraper
    {
        public string FieldName { get; }

        public delegate string Getter(BaseObject obj);
        public delegate void Setter(BaseObject obj, string value);
        private Getter Get;
        private Setter Set;

        public dateTimeFieldWraper(string fieldName, Getter get, Setter set)
        {
            FieldName = fieldName;
            Get = get;
            Set = set;
        }

        public bool Compare(BaseObject obj, string properyName, string value, string op)
        {
            string property_string = Get(obj);
            DateTime property = DateTime.Parse(property_string);
            DateTime v = DateTime.Parse(value);
            switch (op)
            {
                case "=":
                    return property == v;
                case "!=":
                    return property != v;
                case ">":
                    return property.CompareTo(v) > 0;
                case "<":
                    return property.CompareTo(v) < 0;
                case ">=":
                    return property.CompareTo(v) >= 0;
                case "<=":
                    return property.CompareTo(v) <= 0;
                default:
                    throw new Exception("Unknown operator");
            }
        }

        public string GetValue(BaseObject obj)
        {
            return Get(obj);
        }

        public void SetValue(BaseObject obj, string value)
        {
            Set(obj,value);
        }
    }
}
