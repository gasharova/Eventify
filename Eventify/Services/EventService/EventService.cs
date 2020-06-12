using Eventify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventify.Services.EventService
{
    public class EventService : IEventService
    {
        private static List<Event> events = new List<Event> {
            new Event(),
            new Event { Id = 1, Name = "Event1" }
        };

        public async Task<List<Event>> AddEvent(Event newEvent)
        {
            events.Add(newEvent);
            return events;
        }

        public async Task<List<Event>> GetAllEvents()
        {
            return events;
        }

        public async Task<Event> GetEventById(int id)
        {
            return events.FirstOrDefault(c => c.Id == id);
        }
    }
}
