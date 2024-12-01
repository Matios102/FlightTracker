using FlightProject.FlightObjects;

// Purpose: This interface is used to implement the visitor pattern for newspaper provider.
namespace FlightProject.FunctionalObjects.NewsProviders
{
    public class Newspaper : INewsVisitor
    {
        private string name { get; set; }
        private string report;

        public Newspaper(string name)
        {
            this.name = name;
        }
        public string Report()
        {
            return report;
        }

        public void visitAirport(Airport airport)
        {
            report = $"{name} - A report from the {airport.Name} airport, {airport.ISO}.";
        }

        public void visitCargoPlane(CargoPlane cargoPlane)
        {
            report = $"{name} - An interview with the crew of {cargoPlane.Serial}.";
        }

        public void visitPassengerPlane(PassengerPlane passengerPlane)
        {
            report = $"{name} - Breaking news! {passengerPlane.Model} aircraft loses EASA certification after inspection of {passengerPlane.Serial}.";
        }
    }
}
