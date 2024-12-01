using System;
using FlightProject.FlightObjects;

// Purpose: This class is responsible for creating the float field wraper
namespace FlightProject.FunctionalObjects.Commands.Wrapers
{
    public class floatFieldWraper : IFiledWraper
    {
        public delegate float Getter(BaseObject obj);

        public delegate void Setter(BaseObject obj, float value);

        public string FieldName { get; }
        public Getter Get;
        public Setter Set;

        public floatFieldWraper(string fieldName, Getter get, Setter set)
        {
            FieldName = fieldName;
            Get = get;
            Set = set;
        }

        public string GetValue(BaseObject obj)
        {
            return Get(obj).ToString();
        }

        public void SetValue(BaseObject obj, string value)
        {
            Set(obj, float.Parse(value));
        }
        public bool Compare(BaseObject obj, string properyName, string value, string op)
        {
            float property = Get(obj);
            float v = float.Parse(value);
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
    }
}
