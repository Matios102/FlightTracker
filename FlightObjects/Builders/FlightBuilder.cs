using System;

namespace FlightProject.FlightObjects.Builders
{
    public class FlightBuilder
    {
        public UInt64 ID { get; set; }
        public Airport Origin { get; set; }
        public Airport Target { get; set; }
        public String TakeOffTime { get; set; }
        public String LandingTime { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single AMSL { get; set; }
        public UInt64 PlaneID { get; set; }

        public FlightBuilder()
        {
            ID = 0;
            TakeOffTime = "";
            LandingTime = "";
            Longitude = 0;
            Latitude = 0;
            AMSL = 0;
            PlaneID = 0;
            Origin = new Airport();
            Target = new Airport();
        }

        public void SetProperty(string property, string value)
        {
            switch (property)
            {
                case "ID":
                    ID = UInt64.Parse(value);
                    break;
                case "OriginID":
                    Origin.ID = UInt64.Parse(value);
                    break;
                case "TargetID":
                    Target.ID = UInt64.Parse(value);
                    break;
                case "TakeOffTime":
                    TakeOffTime = value;
                    break;
                case "LandingTime":
                    LandingTime = value;
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
                case "PlaneID":
                    PlaneID = UInt64.Parse(value);
                    break;
            }
        }

        public Flight Build()
        {
            return new Flight()
            {
                ID = ID,
                Origin = Origin,
                Target = Target,
                TakeOffTime = TakeOffTime,
                LandingTime = LandingTime,
                Longitude = Longitude,
                Latitude = Latitude,
                AMSL = AMSL,
                PlaneID = PlaneID,
            };
        }
    }
}
