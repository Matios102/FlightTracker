
using System.Collections.Generic;


namespace FlightProject
{
    public class FlightAdapter : FlightsGUIData
    {
        public List<Flight> flightData = new List<Flight>();

        public override ulong GetID(int index)
        {
            return flightData[index].ID;
        }

        public override int GetFlightsCount()
        {
            return flightData.Count;
        }

        public override WorldPosition GetPosition(int index)
        {
            return new WorldPosition(flightData[index].Latitude, flightData[index].Longitude);
        }

        public override double GetRotation(int index)
        {
            return flightData[index].MapCoordRotation;
        }

    }
}
