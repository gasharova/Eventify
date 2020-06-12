using Eventify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventify.Services.EventService
{
    public interface IEventService
    {
        Task<ServiceResponse<List<Event>>> GetAllEvents();
        Task<ServiceResponse<Event>> GetEventById(int id);
        Task<ServiceResponse<List<Event>>> AddEvent(Event newEvent);
    }
}
