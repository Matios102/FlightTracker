using System;
using FlightProject.FlightObjects;

namespace FlightProject.FunctionalObjects.Commands.Wrapers
{
    public class uLongFieldWraper : IFiledWraper
    {
        public delegate ulong Getter(BaseObject obj);

        public delegate void Setter(BaseObject obj, ulong value);

        public string FieldName { get; }
        public Getter Get;
        public Setter Set;

        public uLongFieldWraper(string fieldName, Getter get, Setter set)
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
            Set(obj, ulong.Parse(value));
        }

        public bool Compare(BaseObject obj, string properyName, string value, string op)
        {
            ulong property = Get(obj);
            ulong v = ulong.Parse(value);
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
