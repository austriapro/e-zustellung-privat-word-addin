//===============================================================================
// Microsoft patterns & practices
// Unity Application Block
//===============================================================================
// Copyright © Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================

using System;
using System.Collections.Generic;
using SimpleEventBroker;
using LogService;


namespace SimpleEventBroker
{
    public class EventBroker
    {
        private Dictionary<string, PublishedEvent> eventPublishers = new Dictionary<string, PublishedEvent>();

        public IEnumerable<string> RegisteredEvents
        {
            get 
            {
                foreach(string eventName in eventPublishers.Keys)
                {
                    yield return eventName;
                } 
            }
        }

        public void RegisterPublisher(string publishedEventName, object publisher, string eventName)
        {
            Log.Debug("Registering EventName: {0}, Publisher: {1}", CallerInfo.Create(), publishedEventName, publisher.GetType().FullName);
            PublishedEvent @event = GetEvent(publishedEventName);
            @event.AddPublisher(publisher, eventName);
        }

        public void UnregisterPublisher(string publishedEventName, object publisher, string eventName)
        {
            Log.Debug("Deregistering EventName: {0}, Publisher: {1}", CallerInfo.Create(), publishedEventName, publisher.GetType().FullName);
            PublishedEvent @event = GetEvent(publishedEventName);
            @event.RemovePublisher(publisher, eventName);
            RemoveDeadEvents();
        }

        public void RegisterSubscriber(string publishedEventName, EventHandler subscriber)
        {
            Log.Debug("Registering EventName: {0}", CallerInfo.Create(), publishedEventName);
            PublishedEvent publishedEvent = GetEvent(publishedEventName);
            publishedEvent.AddSubscriber(subscriber);
        }

        public void UnregisterSubscriber(string publishedEventName, EventHandler subscriber)
        {
            Log.Debug("Dergistering EventName: {0}", CallerInfo.Create(),                                                       publishedEventName);

            PublishedEvent @event = GetEvent(publishedEventName);
            @event.RemoveSubscriber(subscriber);
            RemoveDeadEvents();
        }

        public IEnumerable<object> GetPublishersFor(string publishedEvent)
        {
            foreach(object publisher in GetEvent(publishedEvent).Publishers)
            {
                yield return publisher;
            }
        }

        public IEnumerable<EventHandler> GetSubscribersFor(string publishedEvent)
        {
            foreach(EventHandler subscriber in GetEvent(publishedEvent).Subscribers)
            {
                yield return subscriber;
            }
        }

        private PublishedEvent GetEvent(string eventName)
        {
            if(!eventPublishers.ContainsKey(eventName))
            {
                eventPublishers[eventName] = new PublishedEvent();
            }
            return eventPublishers[eventName];
        }

        private void RemoveDeadEvents()
        {
            List<string> deadEvents = new List<string>();
            foreach(KeyValuePair<string, PublishedEvent> publishedEvent in eventPublishers)
            {
                if(!publishedEvent.Value.HasPublishers && !publishedEvent.Value.HasSubscribers)
                {
                    deadEvents.Add(publishedEvent.Key);
                }
            }

            deadEvents.ForEach(delegate(string eventName) { eventPublishers.Remove(eventName); } );
        }
    }
}
