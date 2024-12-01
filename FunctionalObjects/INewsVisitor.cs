using FlightProject.FlightObjects;

// Purpose: This interface is used to implement the visitor pattern.
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
