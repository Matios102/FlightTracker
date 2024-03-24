using System;
using System.Collections.Generic;
using System.Timers;
using FlightProject;
using FlightTrackerGUI;
using Mapsui.Projections;

public class Flight_FlightsGUIData_Adapter
{
    public FlightsGUIData FlightData;
    private Timer timer;
    private List<Flight> flightsList = new List<Flight>();
    private List<double> speeds = new List<double>();
    private List<double> distances = new List<double>();
    private List<double> angles = new List<double>();

    public Flight_FlightsGUIData_Adapter()
    {
        timer = new Timer(0.001);
        timer.Elapsed += onTimerEvent;
    }

    private void onTimerEvent(object source, ElapsedEventArgs e)
    {
        AddFlights(Snapshot.objectList);
        FlightData = ConvertData(flightsList);
        MovePlanes();
    }
    private void AddFlights(List<BaseObject> objectList)
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            if (objectList[i] is Flight)
            {
                Flight flight = objectList[i] as Flight;
                if(flightsList.Contains(flight))
                {
                    continue;
                }
                Console.WriteLine("Adding new flight, ID: " + flight.ID);
                flightsList.Add(flight);
                InitializeParamiters(flight);
            }
        }
    }
    public void Update()
    {
        timer.Start();
        while (true)
        {
            Runner.UpdateGUI(FlightData);
        }
    }

    private FlightsGUIData ConvertData(List<Flight> flights)
    {
        List<FlightGUI> list = new List<FlightGUI>();
        foreach (Flight flight in flights)
        {
            list.Add(new FlightGUI
            {
                ID = flight.ID,
                WorldPosition = new WorldPosition(flight.Latitude, flight.Longitude),
                MapCoordRotation = CalcRotation(flight)
            });
        }
        return new FlightsGUIData(list);
    }

    private double CalcRotation(Flight f)
    {
        double originX, originY, destX, destY;
        (originX, originY) = SphericalMercator.FromLonLat(f.Longitude, f.Latitude);
        (destX, destY) = SphericalMercator.FromLonLat(f.Target.Longitude, f.Target.Latitude);
        double num = Math.Atan2(destY - originY, originX - destX) - Math.PI / 2.0;

        return num;
    }


    private void MovePlanes()
    {
        for (int i = 0; i < flightsList.Count; i++)
        {
            double currentX, currentY;

            (currentX, currentY) = SphericalMercator.FromLonLat((double)flightsList[i].Longitude, (double)flightsList[i].Latitude);
            double speed = speeds[i];

            double speedY = speed * Math.Cos(angles[i]);
            double speedX = speed * Math.Sin(angles[i]);

            currentX += speedX;
            currentY += speedY;

            double newLon, newLat;
            (newLon, newLat) = SphericalMercator.ToLonLat(currentX, currentY);

            if (isAtDestination(flightsList[i], currentX, currentY))
            {
                continue;
            }

            flightsList[i].Longitude = (float)newLon;
            flightsList[i].Latitude = (float)newLat;
        }
    }



    private  double CalculateDurationInSeconds(Flight flight)
    {
        DateTime takeOffDateTime = DateTime.ParseExact(flight.TakeOffTime, "HH:mm", null);
        DateTime landingDateTime = DateTime.ParseExact(flight.LandingTime, "HH:mm", null);

        double durationInSeconds = Math.Abs((landingDateTime - takeOffDateTime).TotalSeconds);
        return durationInSeconds;
    }

    private void InitializeParamiters(Flight flight)
    {
        double speed, distance, angle, durationInSeconds;
        distance = CalculateDistance(flight);
        distances.Add(distance);
        angle = CalcRotation(flight);
        angles.Add(angle);
        durationInSeconds = CalculateDurationInSeconds(flight);
        speed = distance/durationInSeconds;
        speeds.Add(speed);
        adjustBasedOnTime(flight, speed, distance, angle);
    }
    private double CalculateDistance(Flight flight)
    {
        double originX, originY, destX, destY;
        (originX, originY) = SphericalMercator.FromLonLat(flight.Origin.Longitude, flight.Origin.Latitude);
        (destX, destY) = SphericalMercator.FromLonLat(flight.Target.Longitude, flight.Target.Latitude);

        return Math.Sqrt((destX - originX) * (destX - originX) + (destY - originY) * (destY - originY));
    }

    private bool isAtDestination(Flight flight, double currentX, double currentY)
    {
        double destX, destY;
        (destX, destY) = SphericalMercator.FromLonLat(flight.Target.Longitude, flight.Target.Latitude);

        double distanceToDestination = Math.Sqrt((destX - currentX) * (destX - currentX) + (destY - currentY) * (destY - currentY));
        return distanceToDestination < 100000;
    }

    private void adjustBasedOnTime(Flight flight, double speed, double distance, double angle)
    {
        DateTime currentTime = DateTime.Now;
        DateTime takeOffDateTime = DateTime.ParseExact(flight.TakeOffTime, "HH:mm", null);
        DateTime landingDateTime = DateTime.ParseExact(flight.LandingTime, "HH:mm", null);

        if(currentTime > landingDateTime)
        {
            flight.Latitude = flight.Target.Latitude;
            flight.Longitude = flight.Target.Longitude;
            return;
        }

        if(currentTime < takeOffDateTime)
        {
            flight.Latitude = flight.Origin.Latitude;
            flight.Longitude = flight.Origin.Longitude;
            return;
        }


        double elapsedSeconds = (currentTime - DateTime.ParseExact(flight.TakeOffTime, "HH:mm", null)).TotalSeconds;

        double distanceTravelled = elapsedSeconds * speed;

        if (distanceTravelled > distance)
        {
            flight.Latitude = flight.Target.Latitude;
            flight.Longitude = flight.Target.Longitude;
            return;
        }

        double originX, originY, newLon, newLat;
        (originX, originY) = SphericalMercator.FromLonLat(flight.Origin.Longitude, flight.Origin.Latitude);

        double newX = originX + Math.Sin(angle) * distanceTravelled;
        double newY = originY + Math.Cos(angle) * distanceTravelled;

        (newLon, newLat) = SphericalMercator.ToLonLat(newX, newY);

        flight.Latitude = (float)newLat;
        flight.Longitude = (float)newLon;
    }
}
