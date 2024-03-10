using System;
using System.Text;


namespace FlightProject
{
    public class Byte_FlightFactory : Byte_Factory
    {

        public override Flight Create(byte[] values)
        {

            int index = 0;
            string identifier = Encoding.ASCII.GetString(values, index, 3);
            if(identifier != "NFL")
            {
                throw new ArgumentException("invalid values");
            }
            index += 3;

            uint followingMessageLength = BitConverter.ToUInt32(values, index);
            index += 4;

            if(followingMessageLength < 52)
            {
                throw new ArgumentException("invalid values");
            }

            UInt64 id = BitConverter.ToUInt64(values, index);
            index += 8;

            UInt16 origin = BitConverter.ToUInt16(values, index);
            index += 8;

            UInt16 target = BitConverter.ToUInt16(values, index);
            index += 8;

            long takeOffTimeInMs = BitConverter.ToInt64(values, index);
            DateTimeOffset takeOffTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(takeOffTimeInMs);
            string takeOffTime = takeOffTimeOffset.ToString("HH:mm");
            index += 8;

            long landingTimeInMs = BitConverter.ToInt64(values, index);
            DateTimeOffset landingTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(landingTimeInMs);
            string landingTime = landingTimeOffset.ToString("HH:mm");
            index += 8;

            UInt64 planeID = BitConverter.ToUInt64(values, index);
            index += 8;

            UInt16 crewIDsLength = BitConverter.ToUInt16(values, index);
            index += 2;

            UInt64[] crewIDs = new UInt64[crewIDsLength];
            for (int i = 0; i < crewIDsLength; i++)
            {
                crewIDs[i] = BitConverter.ToUInt64(values, index);
                index += 8;
            }

            UInt16 loadIDsLength = BitConverter.ToUInt16(values, index);
            index += 2;

            UInt64[] loadIDs = new UInt64[loadIDsLength];
            for (int i = 0; i < loadIDsLength; i++)
            {
                loadIDs[i] = BitConverter.ToUInt64(values, index);
                index += 8;
            }

            Flight flight = new Flight
            {
                ID = id,
                Origin = origin,
                Target = target,
                TakeOffTime = takeOffTime,
                LandingTime = landingTime,
                PlaneID = planeID,
                CrewIDs = crewIDs,
                LoadIDs = loadIDs
            };

            //Set the paramiters that are not in the byte array as a default value for now
            flight.Latitude = 0.0f;
            flight.Longitude = 0.0f;
            flight.AMSL = 0.0f;

            return flight;
        }
    }
}
