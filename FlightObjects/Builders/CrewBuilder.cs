using System;

namespace FlightProject.FlightObjects.Builders
{
    public class CrewBuilder
    {
        public UInt64 ID { get; set; }
        public String Name { get; set; }
        public UInt64 Age { get; set; }
        public String Phone { get; set; }
        public String Email { get; set; }
        public UInt16 Practice { get; set; }
        public String Role { get; set; }

        public CrewBuilder()
        {
            ID = 0;
            Name = "";
            Age = 0;
            Phone = "";
            Email = "";
            Practice = 0;
            Role = "";
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
                case "Practice":
                    Practice = UInt16.Parse(value);
                    break;
                case "Role":
                    Role = value;
                    break;
            }
        }

        public Crew Build()
        {
            return new Crew
            {
                ID = ID,
                Name = Name,
                Age = Age,
                Phone = Phone,
                Email = Email,
                Practice = Practice,
                Role = Role
            };
        }
    }
}
