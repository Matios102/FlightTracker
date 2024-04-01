using FlightProject.FlightObjects;

namespace FlightProject.FunctionalObjects
{
    public interface INewsVisitor
    {
        string Report();
        void visitAirport(Airport airport);
        void visitCargoPlane(CargoPlane cargoPlane);
        void visitPassengerPlane(PassengerPlane passengerPlane);
    }
}
