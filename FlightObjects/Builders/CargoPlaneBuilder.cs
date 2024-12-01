using System;

// Builder pattern for CargoPlane
namespace FlightProject.FlightObjects.Builders
{
    public class CargoPlaneBuilder
    {
        public UInt64 ID { get; set; }
        public String Model { get; set; }
        public String Serial { get; set; }
        public String ISO { get; set; }
        public Single MaxLoad { get; set; }

        public CargoPlaneBuilder()
        {
            ID = 0;
            Model = "";
            Serial = "";
            ISO = "";
            MaxLoad = 0;
        }

        public void SetProperty(string property, string value)
        {
            switch (property)
            {
                case "ID":
                    ID = UInt64.Parse(value);
                    break;
                case "Model":
                    Model = value;
                    break;
                case "Serial":
                    Serial = value;
                    break;
                case "ISO":
                    ISO = value;
                    break;
                case "MaxLoad":
                    MaxLoad = Single.Parse(value);
                    break;
            }
        }

        public CargoPlane Build()
        {
            return new CargoPlane
            {
                ID = ID,
                Model = Model,
                Serial = Serial,
                ISO = ISO,
                MaxLoad = MaxLoad
            };
        }
    }
}
