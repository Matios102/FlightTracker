


using FlightProject.FlightObjects;

namespace FlightProject.FunctionalObjects.NewsProviders
{
    public class Radio : INewsVisitor
    {
        private string name {get; set;}
        private string report;

        public Radio(string name)
        {
            this.name = name;
        }

        public string Report()
        {
            return report;
        }

        public void visitAirport(Airport airport)
        {
            report = $"Reporting for {name}, Ladies and Gentlemen, we are at the {airport.Name} airport.";
        }

        public void visitCargoPlane(CargoPlane cargoPlane)
        {
            report = $"Reporting for {name}, Ladies and Gentlemen, we are seeing the {cargoPlane.Serial} aircraft fly above us.";
        }

        public void visitPassengerPlane(PassengerPlane passengerPlane)
        {
            report = $"Reporting for {name}, Ladies and Gentlemen, we've just witnessed {passengerPlane.Serial} takeoff.";
        }
    }
}
