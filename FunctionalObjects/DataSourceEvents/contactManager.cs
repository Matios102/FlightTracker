using System.Collections.Generic;
using FlightProject.FlightObjects;
using NetworkSourceSimulator;

// Purpose: This class is responsible for updating the contact info of the crew or passenger
namespace FlightProject.FunctionalObjects.DaraSourceEvents
{
    public class contactManager : IUpdateContact
    {

        private List<BaseObject> FTRobjectList;
        private Logger logger;

        public contactManager(List<BaseObject> objectList, Logger logger)
        {
            FTRobjectList = objectList;
            this.logger = logger;
        }

        public void Update(object sender, ContactInfoUpdateArgs args)
        {
            BaseObject obj = FTRobjectList.Find(x => x.ID == args.ObjectID);
            if (obj == null)
            {
                logger.LogChange($"Error: Object with ID {args.ObjectID} not found");
                return;
            }
            string oldPhone = "";
            string oldEmail = "";
            if (obj is Passenger)
            {
                Passenger passenger = obj as Passenger;
                oldPhone = passenger.Phone;
                oldEmail = passenger.Email;
                passenger.Phone = args.PhoneNumber;
                passenger.Email = args.EmailAddress;
            }
            else if (obj is Crew)
            {
                Crew crew = obj as Crew;
                oldPhone = crew.Phone;
                oldEmail = crew.Email;
                crew.Phone = args.PhoneNumber;
                crew.Email = args.EmailAddress;
            }
            else
            {
                logger.LogChange($"Error: {obj} is not a passenger or crew");
                return;
            }

            logger.LogChange($"Contact info of [{args.ObjectID}] changed from {oldPhone}, {oldEmail} to {args.PhoneNumber} and {args.EmailAddress}");
        }
    }
}
