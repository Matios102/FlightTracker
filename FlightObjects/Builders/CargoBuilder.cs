using System;

// Builder pattern for Airport
namespace FlightProject.FlightObjects.Builders
{
    public class CargoBuilder
    {
        public UInt64 ID { get; set; }
        public Single Weight { get; set; }
        public String Code { get; set; }
        public String Description { get; set; }

        public CargoBuilder()
        {
            ID = 0;
            Weight = 0;
            Code = "";
            Description = "";
        }

        public void SetProperty(string property, string value)
        {
            switch (property)
            {
                case "ID":
                    ID = UInt64.Parse(value);
                    break;
                case "Weight":
                    Weight = Single.Parse(value);
                    break;
                case "Code":
                    Code = value;
                    break;
                case "Description":
                    Description = value;
                    break;
            }
        }

        public Cargo Build()
        {
            return new Cargo
            {
                ID = ID,
                Weight = Weight,
                Code = Code,
                Description = Description
            };
        }
    }
}
