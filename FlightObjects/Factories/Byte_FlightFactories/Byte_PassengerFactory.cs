using System;
using System.Text;

// Factory pattern for creating Passenger objects from byte values
namespace FlightProject.FlightObjects.Factories.Byte_FlightFactories
{
    public class Byte_PassengerFactory : Byte_Factory
    {

        public override Passenger Create(byte[] values)
        {
            int index = 0;
            string identifier = Encoding.ASCII.GetString(values, index, 3);
            if (identifier != "NPA")
            {
                throw new ArgumentException("invalid values");
            }
            index += 3;

            UInt32 followingMessageLength = BitConverter.ToUInt32(values, index);
            index += 4;

            if (followingMessageLength < 35)
            {
                throw new ArgumentException("invalid values");
            }

            UInt64 id = BitConverter.ToUInt64(values, index);
            index += 8;

            UInt16 nameLength = BitConverter.ToUInt16(values, index);
            index += 2;

            string name = Encoding.ASCII.GetString(values, index, nameLength);
            index += nameLength;

            UInt16 age = BitConverter.ToUInt16(values, index);
            index += 2;

            string phone = Encoding.ASCII.GetString(values, index, 12);
            index += 12;

            UInt16 emailLength = BitConverter.ToUInt16(values, index);
            index += 2;

            string email = Encoding.ASCII.GetString(values, index, emailLength);
            index += emailLength;

            string passengerClass = Encoding.ASCII.GetString(values, index, 1);
            index += 1;

            UInt64 miles = BitConverter.ToUInt64(values, index);
            index += 8;

            Passenger passenger = new Passenger
            {
                ID = id,
                Name = name,
                Age = age,
                Phone = phone,
                Email = email,
                Class = passengerClass,
                Miles = miles
            };
            return passenger;
        }
    }
}
