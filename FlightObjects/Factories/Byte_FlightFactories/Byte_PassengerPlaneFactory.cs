using System;
using System.Text;

// Factory pattern for creating PassengerPlane objects from byte values
namespace FlightProject.FlightObjects.Factories.Byte_FlightFactories
{
    public class Byte_PassengerPlaneFactory : Byte_Factory
    {
        public override BaseObject Create(byte[] values)
        {
            int index = 0;
            string identifier = Encoding.ASCII.GetString(values, index, 3);
            if (identifier != "NPP")
            {
                throw new ArgumentException("invalid values");
            }
            index += 3;

            uint followingMessageLength = BitConverter.ToUInt32(values, index);
            index += 4;

            if (followingMessageLength < 29)
            {
                throw new ArgumentException("invalid values");
            }

            UInt64 id = BitConverter.ToUInt64(values, index);
            index += 8;

            string serial = Encoding.ASCII.GetString(values, index, 6);
            index += 10; //MessageFormat has 10 bytes for serial when it should be 6

            string iso = Encoding.ASCII.GetString(values, index, 3);
            index += 3;

            UInt16 modelLength = BitConverter.ToUInt16(values, index);
            index += 2;

            string model = Encoding.ASCII.GetString(values, index, modelLength);
            index += modelLength;

            UInt16 firstClassSize = BitConverter.ToUInt16(values, index);
            index += 2;

            UInt16 businessClassSize = BitConverter.ToUInt16(values, index);
            index += 2;

            UInt16 economyClassSize = BitConverter.ToUInt16(values, index);
            index += 2;

            PassengerPlane passengerPlane = new PassengerPlane
            {
                ID = id,
                Serial = serial,
                ISO = iso,
                Model = model,
                FirstClassSize = firstClassSize,
                BusinessClassSize = businessClassSize,
                EconomyClassSize = economyClassSize
            };

            return passengerPlane;
        }
    }
}
