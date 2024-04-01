

namespace FlightProject.FunctionalObjects
{
    public interface IReportable
    {
        public void accept(INewsVisitor visitor);
    }
}
