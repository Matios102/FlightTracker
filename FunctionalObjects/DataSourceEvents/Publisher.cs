using System.Collections.Generic;
using NetworkSourceSimulator;

namespace FlightProject.FunctionalObjects.DaraSourceEvents
{
    public class Publisher
    {
        private List<IUpdateContact> Contactsubscribers = new List<IUpdateContact>();
        private List<IUpdateID> IDsubscribers = new List<IUpdateID>();

        private List<IUpdatePosition> Positionsubscribers = new List<IUpdatePosition>();

        public Publisher(NetworkSourceSimulator.NetworkSourceSimulator networkSourceSimulator)
        {
            networkSourceSimulator.OnContactInfoUpdate += notifyContactSubscribers;
            networkSourceSimulator.OnIDUpdate += notifyIDSubscribers;
            networkSourceSimulator.OnPositionUpdate += notifyPositionSubscribers;
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

        public void notifyContactSubscribers(object sender, ContactInfoUpdateArgs args)
        {
            foreach (var subscriber in Contactsubscribers)
            {
                subscriber.Update(sender, args);
            }
        }

        public void notifyIDSubscribers(object sender, IDUpdateArgs args)
        {
            foreach (var subscriber in IDsubscribers)
            {
                subscriber.Update(sender, args);
            }
        }

        public void notifyPositionSubscribers(object sender, PositionUpdateArgs args)
        {
            foreach (var subscriber in Positionsubscribers)
            {
                subscriber.Update(sender, args);
            }
        }
    }
}
