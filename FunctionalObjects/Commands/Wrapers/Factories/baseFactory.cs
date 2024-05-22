using System.Collections.Generic;

namespace FlightProject.FunctionalObjects.Commands.Wrapers.Factories
{
    public abstract class baseFactory
    {
        public abstract Dictionary<string, IFiledWraper> Create();
    }
}
