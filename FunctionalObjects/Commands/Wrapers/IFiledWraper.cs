using FlightProject.FlightObjects;

namespace FlightProject.FunctionalObjects.Commands.Wrapers
{
    public interface IFiledWraper
    {
        public string FieldName { get; }
        public string GetValue(BaseObject obj);
        public void SetValue(BaseObject obj, string value);

        public bool Compare(BaseObject obj, string properyName, string value, string op);
    }
}
