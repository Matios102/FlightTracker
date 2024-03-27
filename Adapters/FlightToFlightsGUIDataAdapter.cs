using System;
using System.Collections.Generic;
using System.Timers;
using FlightProject;
using FlightTrackerGUI;

public class FlightToFlightsGUIDataAdapter
{
    public FlightsGUIData FlightData;
    private Timer updateTimer;
    private GUIManager gUIManager = new GUIManager();

    public FlightToFlightsGUIDataAdapter()
    {
        updateTimer = new Timer(1000);
        updateTimer.Elapsed += onTimerEventUpdate;
    }

    public void Update()
    {
        updateTimer.Start();
        while (true)
        {
            Runner.UpdateGUI(FlightData);
        }
    }

    private void onTimerEventUpdate(object source, ElapsedEventArgs e)
    {
        AddFlights(Snapshot.objectList);
        FlightData = ConvertData(gUIManager.flightsList);
        gUIManager.MovePlanes();
    }


    private void AddFlights(List<BaseObject> objectList)
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            if (objectList[i] is Flight)
            {
                Flight flight = objectList[i] as Flight;

                if (gUIManager.flightsList.Contains(flight))
                {
                    continue;
                }

                Console.WriteLine("Adding new flight, ID: " + flight.ID);
                gUIManager.flightsList.Add(flight);
                gUIManager.InitializeParamiters(flight);
            }
        }
    }


    private FlightsGUIData ConvertData(List<Flight> flights)
    {
        List<FlightGUI> list = new List<FlightGUI>();
        for (int i = 0; i < flights.Count; i++)
        {
            if (!gUIManager.toDisplay[i])
            {
                continue;
            }

            list.Add(new FlightGUI
            {
                ID = flights[i].ID,
                WorldPosition = new WorldPosition(flights[i].Latitude, flights[i].Longitude),
                MapCoordRotation = gUIManager.CalcRotation(flights[i])
            });
        }
        return new FlightsGUIData(list);
    }

}
