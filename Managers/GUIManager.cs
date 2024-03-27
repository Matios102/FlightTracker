using System;
using System.Collections.Generic;
using Mapsui.Projections;

namespace FlightProject
{
    public class GUIManager
    {
        public List<Flight> flightsList = new List<Flight>();
        private List<double> speeds = new List<double>();
        private List<double> distances = new List<double>();
        private List<double> angles = new List<double>();
        public List<bool> toDisplay = new List<bool>();



        public void InitializeParamiters(Flight flight)
        {
            double speed, distance, angle, durationInSeconds;
            distance = CalculateDistance(flight);
            distances.Add(distance);
            angle = CalcRotation(flight);
            angles.Add(angle);
            durationInSeconds = CalculateDurationInSeconds(flight);
            speed = distance / durationInSeconds;
            speeds.Add(speed);
            bool display = adjustBasedOnTime(flight, speed, distance, angle);
            toDisplay.Add(display);
        }

        public void MovePlanes()
        {
            DateTime currentTime = DateTime.Now;
            for (int i = 0; i < flightsList.Count; i++)
            {
                toDisplay[i] = true;
                DateTime takeOffDateTime = DateTime.ParseExact(flightsList[i].TakeOffTime, "HH:mm", null);
                if (currentTime < takeOffDateTime)
                {
                    toDisplay[i] = false;
                    continue;
                }


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
                    flightsList[i].Longitude = flightsList[i].Target.Longitude;
                    flightsList[i].Latitude = flightsList[i].Target.Latitude;
                    toDisplay[i] = false;
                    continue;
                }

                flightsList[i].Longitude = (float)newLon;
                flightsList[i].Latitude = (float)newLat;
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

        private bool adjustBasedOnTime(Flight flight, double speed, double distance, double angle)
        {
            DateTime currentTime = DateTime.Now;
            DateTime takeOffDateTime = DateTime.ParseExact(flight.TakeOffTime, "HH:mm", null);
            DateTime landingDateTime = DateTime.ParseExact(flight.LandingTime, "HH:mm", null);

            if (currentTime > landingDateTime && takeOffDateTime < landingDateTime)
            {

                flight.Latitude = flight.Target.Latitude;
                flight.Longitude = flight.Target.Longitude;
                return false;
            }

            if (currentTime < takeOffDateTime)
            {

                flight.Latitude = flight.Origin.Latitude;
                flight.Longitude = flight.Origin.Longitude;
                return false;
            }


            double elapsedSeconds = (currentTime - DateTime.ParseExact(flight.TakeOffTime, "HH:mm", null)).TotalSeconds;

            double distanceTravelled = elapsedSeconds * speed;

            if (distanceTravelled > distance)
            {
                flight.Latitude = flight.Target.Latitude;
                flight.Longitude = flight.Target.Longitude;
                return false;
            }

            double originX, originY, newLon, newLat;
            (originX, originY) = SphericalMercator.FromLonLat(flight.Origin.Longitude, flight.Origin.Latitude);

            double newX = originX + Math.Sin(angle) * distanceTravelled;
            double newY = originY + Math.Cos(angle) * distanceTravelled;

            (newLon, newLat) = SphericalMercator.ToLonLat(newX, newY);

            flight.Latitude = (float)newLat;
            flight.Longitude = (float)newLon;

            return true;
        }
    }
}
