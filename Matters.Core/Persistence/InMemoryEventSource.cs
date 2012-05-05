﻿using System;
using System.Collections.Generic;
using System.Linq;
using Matters.Core.Domain;

namespace Matters.Core.Persistence
{
    public class InMemoryEventSource : IEventSource
    {
        private readonly IEventPublisher _publisher;

        private struct EventDescriptor
        {
            
            public readonly Event EventData;
            public readonly Guid Id;
            public readonly int Version;

            public EventDescriptor(Guid id, Event eventData, int version)
            {
                EventData = eventData;
                Version = version;
                Id = id;
            }
        }

        public InMemoryEventSource(IEventPublisher publisher)
        {
            _publisher = publisher;
        }

        private readonly Dictionary<Guid, List<EventDescriptor>> _current = new Dictionary<Guid, List<EventDescriptor>>(); 
        
        public void SaveEvents(Guid eventSourcedId, IEnumerable<Event> events, int expectedVersion)
        {
            List<EventDescriptor> eventDescriptors;
            if(!_current.TryGetValue(eventSourcedId, out eventDescriptors))
            {
                eventDescriptors = new List<EventDescriptor>();
                _current.Add(eventSourcedId,eventDescriptors);
            }
            else if(eventDescriptors[eventDescriptors.Count - 1].Version != expectedVersion && expectedVersion != -1)
            {
                throw new ConcurrencyException();
            }
            var i = expectedVersion;
            foreach (var @event in events)
            {
                i++;
                @event.SetActualVersion(i);
                eventDescriptors.Add(new EventDescriptor(eventSourcedId,@event,i));
                _publisher.Publish(@event);
            }
        }

        public  List<Event> GetEvents(Guid eventSourcedId)
        {
            List<EventDescriptor> eventDescriptors;
            if (!_current.TryGetValue(eventSourcedId, out eventDescriptors))
            {
                throw new AggregateNotFoundException();
            }
            return eventDescriptors.Select(desc => desc.EventData).ToList();
        }
    }
}
