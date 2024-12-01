using System;
using FlightProject.FlightObjects;

// Purpose: This class is responsible for creating the ushort field wraper
namespace FlightProject.FunctionalObjects.Commands.Wrapers
{
    public class uShortFieldWraper : IFiledWraper
    {
        public delegate ushort Getter(BaseObject obj);

        public delegate void Setter(BaseObject obj, ushort value);

        public string FieldName { get; }
        public Getter Get;
        public Setter Set;

        public uShortFieldWraper(string fieldName, Getter get, Setter set)
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
            Set(obj, ushort.Parse(value));
        }

        public bool Compare(BaseObject obj, string properyName, string value, string op)
        {
            ushort property = Get(obj);
            ushort v = ushort.Parse(value);
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
