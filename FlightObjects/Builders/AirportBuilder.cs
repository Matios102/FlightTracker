using System;

// Builder pattern for Airport
namespace FlightProject.FlightObjects.Builders
{
    public class AirportBuilder
    {
        public UInt64 ID { get; set; }
        public String Name { get; set; }
        public String Code { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single AMSL { get; set; }
        public String ISO { get; set; }

        public AirportBuilder()
        {
            ID = 0;
            Name = "";
            Code = "";
            Longitude = 0;
            Latitude = 0;
            AMSL = 0;
            ISO = "";
        }

        public void SetProperty(string property, string value)
        {
            switch (property)
            {
                case "ID":
                    ID = UInt64.Parse(value);
                    break;
                case "Name":
                    Name = value;
                    break;
                case "Code":
                    Code = value;
                    break;
                case "Longitude":
                    Longitude = Single.Parse(value);
                    break;
                case "Latitude":
                    Latitude = Single.Parse(value);
                    break;
                case "AMSL":
                    AMSL = Single.Parse(value);
                    break;
                case "ISO":
                    ISO = value;
                    break;
            }
        }

        public Airport Build()
        {
            return new Airport
            {
                ID = ID,
                Name = Name,
                Code = Code,
                Longitude = Longitude,
                Latitude = Latitude,
                AMSL = AMSL,
                ISO = ISO
            };
        }
    }
}
