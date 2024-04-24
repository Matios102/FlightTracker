using NetworkSourceSimulator;

namespace FlightProject.FunctionalObjects.DaraSourceEvents
{
    public interface IUpdateContact
    {
        public void Update(object sender, ContactInfoUpdateArgs args);
    }
}
