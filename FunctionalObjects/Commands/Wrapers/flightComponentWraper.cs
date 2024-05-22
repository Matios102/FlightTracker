using System;
using FlightProject.FlightObjects;

namespace FlightProject.FunctionalObjects.Commands.Wrapers
{
    public class flightComponentWraper : IFiledWraper
    {
        public delegate BaseObject Getter(BaseObject obj);
        public delegate void Setter(BaseObject obj, BaseObject value);
        public string FieldName { get; }
        private Getter Get;
        private Setter Set;

        public flightComponentWraper(string fieldName, Getter get, Setter set)
        {
            FieldName = fieldName;
            Get = get;
            Set = set;
        }

        public bool Compare(BaseObject obj, string properyName, string value, string op)
        {
            ulong property = Get(obj).ID;
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

        public string GetValue(BaseObject obj)
        {
            var value = Get(obj);
            if (value == null)
            {
                return "null";
            }
            return value.ID.ToString();
        }

        public void SetValue(BaseObject obj, string value)
        {
            throw new NotImplementedException("Setting is not implemeneted for complex types");
        }
    }
}
