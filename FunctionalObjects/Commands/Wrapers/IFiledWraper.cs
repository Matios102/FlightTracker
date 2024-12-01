using FlightProject.FlightObjects;

// Purpose: This interface is responsible for creating the field wraper containing the fields and their wrapers
namespace FlightProject.FunctionalObjects.Commands.Wrapers
{
    public interface IFiledWraper
    {
        public string FieldName { get; } // For displaying the field name
        public string GetValue(BaseObject obj); // For getting the value of the field
        public void SetValue(BaseObject obj, string value); // For setting the value of the field

        public bool Compare(BaseObject obj, string properyName, string value, string op); // For comparing the value of the field
    }
}
