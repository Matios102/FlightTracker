using System.Collections.Generic;
using FlightProject.FlightObjects;
using NetworkSourceSimulator;

namespace FlightProject.FunctionalObjects.DaraSourceEvents
{
    public class idManager : IUpdateID
    {
        private List<BaseObject> FTRobjectList;
        private Logger logger;

        public idManager(List<BaseObject> FTRobjectList, Logger logger)
        {
            this.FTRobjectList = FTRobjectList;
            this.logger = logger;
        }

        public void Update(object sender, IDUpdateArgs args)
        {
            BaseObject obj = FTRobjectList.Find(x => x.ID == args.ObjectID);
            if (obj == null)
            {
                logger.LogChange($"Error: Object with ID {args.ObjectID} not found");
                return;
            }
            obj.ID = args.NewObjectID;
            logger.LogChange($"ID updated from {args.ObjectID} to {args.NewObjectID}");
        }
    }
}
