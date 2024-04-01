using System;
using System.Text;

namespace FlightProject.FlightObjects.Factories.Byte_FlightFactories
{
    public class Byte_CargoFactory : Byte_Factory
    {
        public override Cargo Create(byte[] values)
        {
            int index = 0;
            string identifier = Encoding.ASCII.GetString(values, index, 3);
            if(identifier != "NCA")
            {
                throw new ArgumentException("invalid values");
            }
            index += 3;

            uint followingMessageLength = BitConverter.ToUInt32(values, index);
            if(followingMessageLength < 20)
            {
                throw new ArgumentException("invalid values");
            }
            index += 4;

            UInt64 id = BitConverter.ToUInt64(values, index);
            index += 8;

            Single weight = BitConverter.ToSingle(values, index);
            index += 4;

            string code = Encoding.ASCII.GetString(values, index, 6);
            index += 6;

            UInt16 descriptionLength = BitConverter.ToUInt16(values, index);
            index += 2;

            string description = Encoding.ASCII.GetString(values, index, descriptionLength);
            index += descriptionLength;

            Cargo cargo = new Cargo
            {
                ID = id,
                Weight = weight,
                Code = code,
                Description = description
            };

            return cargo;
        }
    }
}
