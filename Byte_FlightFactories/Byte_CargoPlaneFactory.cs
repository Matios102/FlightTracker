using System;
using System.Text;

namespace FlightProject
{
    public class Byte_CargoPlaneFactory : Byte_Factory
    {
        public override CargoPlane Create(byte[] values)
        {
            int index = 0;
            string identifier = Encoding.ASCII.GetString(values, index, 3);
            if (identifier != "NCP")
            {
                throw new ArgumentException("invalid values");
            }
            index += 3;

            uint followingMessageLength = BitConverter.ToUInt32(values, index);
            index += 4;

            if (followingMessageLength < 27)
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

            Single maxLoad = BitConverter.ToSingle(values, index);
            index += 4;

            CargoPlane cargoPlane = new CargoPlane
            {
                ID = id,
                Serial = serial,
                ISO = iso,
                Model = model,
                MaxLoad = maxLoad
            };
            return cargoPlane;
        }
    }
}