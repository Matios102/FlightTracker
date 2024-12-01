
// Purpose: Abstract class that is used to create a BaseObject from a string array.
namespace FlightProject.FlightObjects.Factories.String_FlightFactories
{
    public abstract class Factory
    {
           public abstract BaseObject Create(string[] values);
    }
}
