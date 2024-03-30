using System;
using System.Text;
namespace FlightProject
{
    public class Byte_CrewFactory : Byte_Factory
    {
        public override Crew Create(byte[] values)
        {
            int index = 0;
            string identifier = Encoding.ASCII.GetString(values, index, 3);
            if (identifier != "NCR")
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

            UInt16 practice = BitConverter.ToUInt16(values, index);
            index += 2;

            string role = Encoding.ASCII.GetString(values, index, 1);
            index += 1;

            Crew crew = new Crew
            {
                ID = id,
                Name = name,
                Age = age,
                Phone = phone,
                Email = email,
                Practice = practice,
                Role = role
            };
            return crew;
        }
    }
}