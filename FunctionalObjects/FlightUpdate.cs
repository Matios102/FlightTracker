using System;
using System.Collections.Generic;
using System.Timers;
using FlightProject;
using FlightTrackerGUI;
using Mapsui.Projections;

public class FlightUpdate
{
    private List<Flight> allFlights = new List<Flight>();
    private List<Flight> inAirFlights = new List<Flight>();


    private Timer updateTimer;
    private List<double> speeds = new List<double>();
    private List<double> distances = new List<double>();

    public FlightUpdate()
    {
        updateTimer = new Timer(1);
        updateTimer.Elapsed += onTimerEventUpdate;
        Snapshot.newFlightReady += AddFlight;
    }

    public void Update()
    {
        updateTimer.Start();
        while (true)
        {
            Runner.UpdateGUI(new FlightAdapter(inAirFlights));
        }
    }

    private void onTimerEventUpdate(object source, ElapsedEventArgs e)
    {
        Console.WriteLine(inAirFlights.Count);
        MovePlanes();
    }

    public void InitializeParamiters(Flight flight)
    {
        double speed, distance, durationInSeconds;
        distance = CalculateDistance(flight);
        distances.Add(distance);
        flight.MapCoordRotation = CalcRotation(flight);
        durationInSeconds = CalculateDurationInSeconds(flight);
        speed = distance / durationInSeconds;
        speeds.Add(speed);
        adjustBasedOnTime(flight, speed, distance);
    }

    public void MovePlanes()
    {
        DateTime currentTime = DateTime.Now;
        for (int i = 0; i < allFlights.Count; i++)
        {
            DateTime takeOffDateTime = DateTime.ParseExact(allFlights[i].TakeOffTime, "HH:mm", null);
            if (currentTime < takeOffDateTime)
            {
                continue;
            }


            double currentX, currentY;

            (currentX, currentY) = SphericalMercator.FromLonLat((double)allFlights[i].Longitude, (double)allFlights[i].Latitude);
            double speed = speeds[i];

            double speedY = speed * Math.Cos(allFlights[i].MapCoordRotation);
            double speedX = speed * Math.Sin(allFlights[i].MapCoordRotation);

            currentX += speedX;
            currentY += speedY;

            double newLon, newLat;
            (newLon, newLat) = SphericalMercator.ToLonLat(currentX, currentY);

            if (isAtDestination(allFlights[i], currentX, currentY))
            {
                allFlights[i].Longitude = allFlights[i].Target.Longitude;
                allFlights[i].Latitude = allFlights[i].Target.Latitude;
                allFlights[i].MapCoordRotation = Math.PI /2;

                inAirFlights.Remove(allFlights[i]);
                continue;
            }

            allFlights[i].Longitude = (float)newLon;
            allFlights[i].Latitude = (float)newLat;

            if(!inAirFlights.Contains(allFlights[i]))
                inAirFlights.Add(allFlights[i]);

        }
    }
    public double CalcRotation(Flight f)
    {
        double originX, originY, destX, destY;
        (originX, originY) = SphericalMercator.FromLonLat(f.Longitude, f.Latitude);
        (destX, destY) = SphericalMercator.FromLonLat(f.Target.Longitude, f.Target.Latitude);
        double num = Math.Atan2(destY - originY, originX - destX) - Math.PI / 2.0;

        return num;
    }

    private double CalculateDurationInSeconds(Flight flight)
    {
        DateTime takeOffDateTime = DateTime.ParseExact(flight.TakeOffTime, "HH:mm", null);
        DateTime landingDateTime = DateTime.ParseExact(flight.LandingTime, "HH:mm", null);

        double durationInSeconds = Math.Abs((landingDateTime - takeOffDateTime).TotalSeconds);
        return durationInSeconds;
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

    private void adjustBasedOnTime(Flight flight, double speed, double distance)
    {
        DateTime currentTime = DateTime.Now;
        DateTime takeOffDateTime = DateTime.ParseExact(flight.TakeOffTime, "HH:mm", null);
        DateTime landingDateTime = DateTime.ParseExact(flight.LandingTime, "HH:mm", null);

        if (currentTime > landingDateTime && takeOffDateTime < landingDateTime)
        {
            flight.Latitude = flight.Target.Latitude;
            flight.Longitude = flight.Target.Longitude;
            return;
        }

        if (currentTime < takeOffDateTime)
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

        double newX = originX + Math.Sin(flight.MapCoordRotation) * distanceTravelled;
        double newY = originY + Math.Cos(flight.MapCoordRotation) * distanceTravelled;

        (newLon, newLat) = SphericalMercator.ToLonLat(newX, newY);

        flight.Latitude = (float)newLat;
        flight.Longitude = (float)newLon;

        return;
    }

    private void AddFlight(Flight flight)
    {
        Console.WriteLine("Adding new flight, ID: " + flight.ID);
        InitializeParamiters(flight);
        allFlights.Add(flight);
    }
}
