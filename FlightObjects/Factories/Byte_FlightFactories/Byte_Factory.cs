
// Purpose: Abstract class that is used to create a factory for creating objects from byte arrays.
namespace FlightProject.FlightObjects.Factories.Byte_FlightFactories
{
    public abstract class Byte_Factory
    {
           public abstract BaseObject Create(byte[] values);
    }
}
