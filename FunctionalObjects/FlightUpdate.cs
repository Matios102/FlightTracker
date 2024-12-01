using System;
using System.Collections.Generic;
using System.Timers;
using FlightProject.FlightObjects;
using FlightTrackerGUI;
using Mapsui.Projections;
using NetworkSourceSimulator;
using FlightProject.FunctionalObjects.DaraSourceEvents;
using NetTopologySuite.Operation.Distance3D;

// Purpose: Update the position of flights on the map.
namespace FlightProject.FunctionalObjects
{
    public class FlightUpdate
    {
        private List<Flight> allFlights = new List<Flight>();
        private Timer updateTimer;

        private FlightAdapter flightAdapter;

        private Logger logger;

        public FlightUpdate(Logger logger, positionManager positionManager)
        {
            positionManager.FlightUpdateEvent += PositionUpdateHendler; // subscribe to the position update event
            updateTimer = new Timer(1000); // update every second
            updateTimer.Elapsed += onTimerEventUpdate;
            Snapshot.newFlightReady += AddFlight; // subscribe to the new flight event to add new flights
            flightAdapter = new FlightAdapter(allFlights);
            this.logger = logger;
        }

        // Purpose: Update the position of flights on the map.
        public void Update()
        {
            // Add all flights from the snapshot to the list of all flights to be displayed
            foreach (var obj in Snapshot.objectList)
            {
                if (obj is Flight)
                {
                    AddFlight(obj as Flight);
                }
            }
            updateTimer.Start();
            while (true)
            {
                Runner.UpdateGUI(flightAdapter); // Update the GUI with the new flight data
            }
        }

        // Purpose: Each sevond move the planes on the map.
        private void onTimerEventUpdate(object source, ElapsedEventArgs e)
        {
            MovePlanes();
        }

        // Purpose: Initialize the parameters of the flight.
        // Each flight has a takeoff time and a landing time, origin and target.
        // The speed and distance are calculated based on those times.
        public void InitializeParameters(Flight flight)
        {
            flight.MapCoordRotation = CalcRotation(flight);
            flight.TotalDistance = CalculateDistance(flight);
            flight.TotalDurationInSeonds = CalculateTotalDurationInSeconds(flight);
            flight.CurrentSpeed = flight.TotalDistance / flight.TotalDurationInSeonds;
        }

        // Purpose: Move the planes on the map.
        public void MovePlanes()
        {
            // Move each plane on the map
            for (int i = 0; i < allFlights.Count; i++)
            {

                DateTime currentTime = DateTime.Now;
                DateTime takeOffDateTime = DateTime.ParseExact(allFlights[i].TakeOffTime, "HH:mm", null);
                DateTime landingDateTime = DateTime.ParseExact(allFlights[i].LandingTime, "HH:mm", null);

                // If the current time is before the takeoff time, set the plane's position to the origin
                if (currentTime < takeOffDateTime)
                {
                    continue;
                }

                if(currentTime > landingDateTime)
                {
                    allFlights[i].Longitude = allFlights[i].Target.Longitude;
                    allFlights[i].Latitude = allFlights[i].Target.Latitude;
                    allFlights[i].MapCoordRotation = Math.PI / 2;
                    allFlights[i].IsMarkedForRemoval = true;
                    continue;
                }

                double currentX, currentY;

                // Get the current position of the plane
                (currentX, currentY) = SphericalMercator.FromLonLat((double)allFlights[i].Longitude, (double)allFlights[i].Latitude);

                // Calculate the speed in the x and y directions
                double speedY = allFlights[i].CurrentSpeed * Math.Cos(allFlights[i].MapCoordRotation);
                double speedX = allFlights[i].CurrentSpeed * Math.Sin(allFlights[i].MapCoordRotation);

                currentX += speedX;
                currentY += speedY;

                double newLon, newLat;
                (newLon, newLat) = SphericalMercator.ToLonLat(currentX, currentY);

                // If the plane is at the destination, remove it from the list
                if (isAtDestination(allFlights[i], currentX, currentY))
                {
                    allFlights[i].Longitude = allFlights[i].Target.Longitude;
                    allFlights[i].Latitude = allFlights[i].Target.Latitude;
                    allFlights[i].MapCoordRotation = Math.PI / 2;

                    Console.WriteLine("Flight " + allFlights[i].ID + " has reached its destination");
                    allFlights[i].IsMarkedForRemoval = true;
                    continue;
                }

                // Update the position of the plane
                allFlights[i].Longitude = (float)newLon;
                allFlights[i].Latitude = (float)newLat;

                // Update the rotation of the plane
                allFlights[i].MapCoordRotation = CalcRotation(allFlights[i]);
            }

            // Remove flights marked for removal after completing the update cycle.
            allFlights.RemoveAll(flight => flight.IsMarkedForRemoval);
        }

        // Purpose: Calculate the rotation of the plane on the map.
        public double CalcRotation(Flight f)
        {
            double currentX, currentY, destX, destY;
            (currentX, currentY) = SphericalMercator.FromLonLat(f.Longitude, f.Latitude);
            (destX, destY) = SphericalMercator.FromLonLat(f.Target.Longitude, f.Target.Latitude);
            double num = Math.Atan2(destY - currentY, currentX - destX) - Math.PI / 2.0;

            return num;
        }

        // Purpose: Calculate the duration of the flight in seconds.
        private double CalculateTotalDurationInSeconds(Flight flight)
        {
            DateTime takeOffDateTime = DateTime.ParseExact(flight.TakeOffTime, "HH:mm", null);
            DateTime landingDateTime = DateTime.ParseExact(flight.LandingTime, "HH:mm", null);

            return Math.Abs((landingDateTime - takeOffDateTime).TotalSeconds);
        }

        // Purpose: Calculate the distance between the origin and destination of the flight.
        private double CalculateDistance(Flight flight)
        {
            double originX, originY, destX, destY;
            (originX, originY) = SphericalMercator.FromLonLat(flight.Longitude, flight.Latitude);
            (destX, destY) = SphericalMercator.FromLonLat(flight.Target.Longitude, flight.Target.Latitude);

            return Math.Sqrt((destX - originX) * (destX - originX) + (destY - originY) * (destY - originY));
        }

        // Purpose: Check if the plane is at the destination.
        private bool isAtDestination(Flight flight, double currentX, double currentY)
        {
            double destX, destY;
            (destX, destY) = SphericalMercator.FromLonLat(flight.Target.Longitude, flight.Target.Latitude);

            double distanceToDestination = Math.Sqrt((destX - currentX) * (destX - currentX) + (destY - currentY) * (destY - currentY));
            return distanceToDestination < 10;
        }

        // Purpose: Adjust the flight position based on the current time.
        private void adjustBasedOnTime(Flight flight, double speed, double distance)
        {
            DateTime currentTime = DateTime.Now;
            DateTime takeOffDateTime = DateTime.ParseExact(flight.TakeOffTime, "HH:mm", null);
            DateTime landingDateTime = DateTime.ParseExact(flight.LandingTime, "HH:mm", null);

            // If the current time is after the landing time, set the plane's position to the destination
            if (currentTime > landingDateTime && takeOffDateTime < landingDateTime)
            {
                flight.Latitude = flight.Target.Latitude;
                flight.Longitude = flight.Target.Longitude;
                return;
            }

            // If the current time is before the takeoff time, set the plane's position to the origin
            if (currentTime < takeOffDateTime)
            {
                flight.Latitude = flight.Origin.Latitude;
                flight.Longitude = flight.Origin.Longitude;
                return;
            }


            double elapsedSeconds = (currentTime - takeOffDateTime).TotalSeconds;

            double distanceTravelled = elapsedSeconds * speed;

            // If the distance travelled is greater than the distance between the origin and destination, set the plane's position to the destination
            if (distanceTravelled > distance)
            {
                flight.Latitude = flight.Target.Latitude;
                flight.Longitude = flight.Target.Longitude;
                return;
            }

            // Calculate the currnet position of the plane
            double originX, originY, newLon, newLat;
            (originX, originY) = SphericalMercator.FromLonLat(flight.Origin.Longitude, flight.Origin.Latitude);

            double newX = originX + Math.Sin(flight.MapCoordRotation) * distanceTravelled;
            double newY = originY + Math.Cos(flight.MapCoordRotation) * distanceTravelled;

            (newLon, newLat) = SphericalMercator.ToLonLat(newX, newY);

            flight.Latitude = (float)newLat;
            flight.Longitude = (float)newLon;

            return;
        }

        // Purpose: Add a new flight to the list of all flights and log it.
        private void AddFlight(Flight flight)
        {
            Console.WriteLine("Adding new flight, ID: " + flight.ID);
            InitializeParameters(flight);
            adjustBasedOnTime(flight, flight.CurrentSpeed, flight.TotalDistance);
            allFlights.Add(flight);
        }


        // Purpose: Update the position of the flight on the map upon position change event.
        private void PositionUpdateHendler(object sender, PositionUpdateArgs args, Flight flight)
        {
            double originalLon = flight.Longitude;
            double originalLat = flight.Latitude;

            double originalRotation = flight.MapCoordRotation;
            double originalSpeed;


            flight.Latitude = args.Latitude;
            flight.Longitude = args.Longitude;
            flight.AMSL = args.AMSL;

            flight.MapCoordRotation = CalcRotation(flight);


            for(int i = 0; i < allFlights.Count; i++)
            {
                if(allFlights[i].ID == flight.ID)
                {
                    originalSpeed = allFlights[i].CurrentSpeed;
                    flight.CurrentSpeed = CalculateDistance(flight) / CalculateTotalDurationInSeconds(flight);
                    logger.LogChange($"Flight [{flight.ID}] updated from longitude: {originalLon}, latitude: {originalLat}, speed: {originalSpeed}, rotation: {originalRotation} to longitude: {flight.Longitude}, latitude: {flight.Latitude}, speed: {flight.CurrentSpeed}, rotation: {flight.MapCoordRotation}");
                    break;
                }
            }
        }
    }
}
