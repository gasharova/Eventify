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

        public async Task<ServiceResponse<List<Event>>> AddEvent(Event newEvent)
        {
            ServiceResponse<List<Event>> serviceResponse = new ServiceResponse<List<Event>>();
            events.Add(newEvent);
            serviceResponse.Data = events;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Event>>> GetAllEvents()
        {
            ServiceResponse<List<Event>> serviceResponse = new ServiceResponse<List<Event>>();
            serviceResponse.Data = events;
            return serviceResponse;
        }

        public async Task<ServiceResponse<Event>> GetEventById(int id)
        {
            ServiceResponse<Event> serviceResponse = new ServiceResponse<Event>();
            serviceResponse.Data = events.FirstOrDefault(c => c.Id == id);
            return serviceResponse;
        }
    }
}
