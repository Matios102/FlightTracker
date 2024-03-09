using System;
namespace FlightProject
{
    public class PassengerPlane : BaseObject
    {
        public String Serial {get; set;}
        public String ISO {get; set;}
        public String Model {get; set;}
        public UInt16 FirstClassSize {get; set;}
        public UInt16 BusinessClassSize {get; set;}
        public UInt16 EconomyClassSize {get; set;}
    }
}