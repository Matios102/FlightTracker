using System;
using System.Collections.Generic;
using FlightProject.FlightObjects;
using NetworkSourceSimulator;

namespace FlightProject.FunctionalObjects.DaraSourceEvents
{
    public class positionManager : IUpdatePosition
    {
        private List<BaseObject> FTRobjectList;
        private Logger logger;

        public event Action<positionManager, PositionUpdateArgs, Flight> FlightUpdateEvent;

        public positionManager(List<BaseObject> FTRobjectList, Logger logger)
        {
            this.FTRobjectList = FTRobjectList;
            this.logger = logger;
        }

        public void Update(object sender, PositionUpdateArgs args)
        {
            BaseObject obj = FTRobjectList.Find(x => x.ID == args.ObjectID);
            if (obj == null)
            {
                logger.LogChange($"Error: Object with ID {args.ObjectID} not found");
                return;
            }
            if (obj is Flight)
            {
                Flight flight = obj as Flight;
                FlightUpdateEvent.Invoke(this, args, flight);
            }
            else
            {
                logger.LogChange($"Error: {obj} is not a flight cannot update position");
            }
        }
    }
}
