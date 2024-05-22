using System;

namespace FlightProject.FlightObjects.Builders
{
    public class PassengerBuilder
    {
        public UInt64 ID { get; set; }
        public String Name { get; set; }
        public UInt64 Age { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public String Class { get; set; }
        public UInt64 Miles { get; set; }

        public PassengerBuilder()
        {
            ID = 0;
            Name = "";
            Age = 0;
            Phone = "";
            Email = "";
            Class = "";
            Miles = 0;
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
                case "Age":
                    Age = UInt64.Parse(value);
                    break;
                case "Phone":
                    Phone = value;
                    break;
                case "Email":
                    Email = value;
                    break;
                case "Class":
                    Class = value;
                    break;
                case "Miles":
                    Miles = UInt64.Parse(value);
                    break;
            }
        }

        public Passenger Build()
        {
            return new Passenger
            {
                ID = ID,
                Name = Name,
                Age = Age,
                Phone = Phone,
                Email = Email,
                Class = Class,
                Miles = Miles
            };
        }
    }
}
