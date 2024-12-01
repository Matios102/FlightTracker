
// Purpose: This interface is used to implement the visitor pattern.
// It is used to allow the visitor to visit the object and perform some operation on it.
namespace FlightProject.FunctionalObjects
{
    public interface IReportable
    {
        public void accept(INewsVisitor visitor);
    }
}
