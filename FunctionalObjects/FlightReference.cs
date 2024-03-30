using System;
using System.Collections.Generic;

namespace FlightProject
{
    public class FlightReference
    {
        private readonly List<BaseObject> objectList;

        public FlightReference(List<BaseObject> objectList)
        {
            this.objectList = objectList;
        }

        public Airport FindAirportByID(UInt64 id)
        {
            return (Airport)objectList.Find(airport => airport.ID == id);
        }

        public List<Crew> FindCrewListByID(UInt64[] ids)
        {
            List<Crew> crewList = new List<Crew>();
            foreach(var c in ids)
            {
                Crew crew = (Crew)objectList.Find(x => x.ID == c);
                crewList.Add(crew);
            }

            return crewList;
        }

        public List<BaseObject> FindLoadListByID(UInt64[] ids)
        {
            List<BaseObject> loadList = new List<BaseObject>();
            foreach(var c in ids)
            {
                BaseObject load = objectList.Find(x => x.ID == c);
                loadList.Add(load);
            }

            return loadList;
        }
    }
}
