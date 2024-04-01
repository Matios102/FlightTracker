


using FlightProject.FlightObjects;

namespace FlightProject.FunctionalObjects.NewsProviders
{
    public class Television : INewsVisitor
    {
        private string name { get; set; }
        private string report;

        public Television(string name)
        {
            this.name = name;
        }

        public string Report()
        {
            return report;
        }

        public void visitAirport(Airport airport)
        {
            report = $"<An image of {airport.Name} airport>";
        }

        public void visitCargoPlane(CargoPlane cargoPlane)
        {
            report = $"<An image of {cargoPlane.Model} cargo plane>";
        }

        public void visitPassengerPlane(PassengerPlane passengerPlane)
        {
            report = $"<An image of {passengerPlane.Model} passenger plane>";
        }
    }
}
