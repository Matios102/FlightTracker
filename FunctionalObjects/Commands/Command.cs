using System.Collections.Generic;

namespace FlightProject.FunctionalObjects.Commands
{
    public class Command
    {
        public string type { get; set; } // "display", "add", "delete", "update"
        public string object_class { get; set; } // "Flight", "Passenger" ... etc.
        public Dictionary<string, string> key_value { get; set; } // for add/update command {parameter} = {value}
        public List<string> object_fileds { get; set; } // for display command {property1}, {property2} ... etc.
        public List<Condition> conditions { get; set; } // {property1} {operator1} {value1} {logical_operator1} ... etc.
        public List<string> logical_operators {get; set;} // and/or
    }
}
