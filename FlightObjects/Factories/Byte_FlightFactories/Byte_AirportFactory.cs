using System;
using System.Text;


namespace FlightProject
{
    public class Byte_AirportFactory : Byte_Factory
    {
        
        public override Airport Create(byte[] values)
        {
            int index = 0;
            string identifier = Encoding.ASCII.GetString(values, index, 3);
            if(identifier != "NAI")
            {
                throw new ArgumentException("invalid values");
            }
            index += 3;

            uint followingMessageLength = BitConverter.ToUInt32(values, index);
            index += 4;

            if(followingMessageLength < 28)
            {
                throw new ArgumentException("invalid values");
            }

            UInt64 id = BitConverter.ToUInt64(values, index);
            index += 8;

            UInt16 nameLength = BitConverter.ToUInt16(values, index);
            index += 2;

            string name = Encoding.ASCII.GetString(values, index, nameLength);
            index += nameLength;

            string code = Encoding.ASCII.GetString(values, index, 3);
            index += 3;

            Single longitude = BitConverter.ToSingle(values, index);
            index += 4;

            Single latitude = BitConverter.ToSingle(values, index);
            index += 4;

            Single amsl = BitConverter.ToSingle(values, index);
            index += 4;

            string isoCountryCode = Encoding.ASCII.GetString(values, index, 3);
            index += 3;

            Airport airport = new Airport
            {
                ID = id,
                Name = name,
                Code = code,
                Longitude = longitude,
                Latitude = latitude,
                AMSL = amsl,
                ISO = isoCountryCode
            };
            return airport;
        }

    }
}