
using System.Collections.Generic;
using FlightProject.FlightObjects;

// Purpose: This class is responsible for adapting the flight data to the GUI
namespace FlightProject.FunctionalObjects
{
    public class FlightAdapter : FlightsGUIData
    {
        public List<Flight> flightData;

        public FlightAdapter(List<Flight> flights)
        {
            flightData = flights;
        }

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
