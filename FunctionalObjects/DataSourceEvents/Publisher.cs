using System.Collections.Generic;
using NetworkSourceSimulator;


// Purpose: This class is responsible for notifying the subscribers about the updates coming from the network source simulator
namespace FlightProject.FunctionalObjects.DaraSourceEvents
{
    public class Publisher
    {
        private List<IUpdateContact> Contactsubscribers = new List<IUpdateContact>();
        private List<IUpdateID> IDsubscribers = new List<IUpdateID>();

        private List<IUpdatePosition> Positionsubscribers = new List<IUpdatePosition>();

        public Publisher(NetworkSourceSimulator.NetworkSourceSimulator networkSourceSimulator)
        {
            networkSourceSimulator.OnContactInfoUpdate += notifyContactSubscribers; // This is the event that will be triggered when the contact info is updated
            networkSourceSimulator.OnIDUpdate += notifyIDSubscribers; // This is the event that will be triggered when the ID is updated
            networkSourceSimulator.OnPositionUpdate += notifyPositionSubscribers; // This is the event that will be triggered when the position is updated
        }
        public void subscribe(IUpdateContact subscriber)
        {
            Contactsubscribers.Add(subscriber);
        }

        public void subscribe(IUpdateID subscriber)
        {
            IDsubscribers.Add(subscriber);
        }

        public void subscribe(IUpdatePosition subscriber)
        {
            Positionsubscribers.Add(subscriber);
        }

        public void unSubscribe(IUpdateContact subscriber)
        {
            Contactsubscribers.Remove(subscriber);
        }

        public void unSubscribe(IUpdateID subscriber)
        {
            IDsubscribers.Remove(subscriber);
        }

        public void unSubscribe(IUpdatePosition subscriber)
        {
            Positionsubscribers.Remove(subscriber);
        }

        // This method will notify the subscribers about the contact info update
        public void notifyContactSubscribers(object sender, ContactInfoUpdateArgs args)
        {
            foreach (var subscriber in Contactsubscribers)
            {
                subscriber.Update(sender, args);
            }
        }

        // This method will notify the subscribers about the ID update
        public void notifyIDSubscribers(object sender, IDUpdateArgs args)
        {
            foreach (var subscriber in IDsubscribers)
            {
                subscriber.Update(sender, args);
            }
        }

        // This method will notify the subscribers about the position update
        public void notifyPositionSubscribers(object sender, PositionUpdateArgs args)
        {
            foreach (var subscriber in Positionsubscribers)
            {
                subscriber.Update(sender, args);
            }
        }
    }
}
