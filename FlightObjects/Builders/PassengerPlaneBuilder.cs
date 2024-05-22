using System;

namespace FlightProject.FlightObjects.Builders
{
    public class PassengerPlaneBuilder
    {
        public UInt64 ID { get; set; }
        public String Model { get; set; }
        public String Serial { get; set; }
        public String ISO { get; set; }
        public UInt16 FirstClassSize { get; set; }
        public UInt16 BusinessClassSize { get; set; }
        public UInt16 EconomyClassSize { get; set; }

        public PassengerPlaneBuilder()
        {
            ID = 0;
            Model = "";
            Serial = "";
            ISO = "";
            FirstClassSize = 0;
            BusinessClassSize = 0;
            EconomyClassSize = 0;
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
                case "FirstClassSize":
                    FirstClassSize = UInt16.Parse(value);
                    break;
                case "BusinessClassSize":
                    BusinessClassSize = UInt16.Parse(value);
                    break;
                case "EconomyClassSize":
                    EconomyClassSize = UInt16.Parse(value);
                    break;
            }
        }

        public PassengerPlane Build()
        {
            return new PassengerPlane
            {
                ID = ID,
                Model = Model,
                Serial = Serial,
                ISO = ISO,
                FirstClassSize = FirstClassSize,
                BusinessClassSize = BusinessClassSize,
                EconomyClassSize = EconomyClassSize
            };
        }
    }
}
