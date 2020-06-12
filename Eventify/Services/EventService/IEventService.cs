using Eventify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventify.Services.EventService
{
    public interface IEventService
    {
        Task<List<Event>> GetAllEvents();
        Task<Event> GetEventById(int id);
        Task<List<Event>> AddEvent(Event newEvent);
    }
}
