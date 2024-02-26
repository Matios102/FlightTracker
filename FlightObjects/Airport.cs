using System;
namespace FlightProject
{
    public class Airport : BaseObject
    {
        public String Name {get; set;}
        public String Code {get; set;}
        public Single Longitude {get; set;}
        public Single Latitude {get; set;}
        public Single AMSL {get; set;}
        public String ISO {get; set;}
    
    }
}