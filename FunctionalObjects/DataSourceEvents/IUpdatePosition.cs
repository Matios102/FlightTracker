using NetworkSourceSimulator;

namespace FlightProject.FunctionalObjects.DaraSourceEvents
{
    public interface IUpdatePosition
    {
        public void Update(object sender, PositionUpdateArgs args);
    }
}
