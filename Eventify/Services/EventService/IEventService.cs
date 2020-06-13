using Eventify.DTOs.Event;
using Eventify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventify.Services.EventService
{
    public interface IEventService
    {
        Task<ServiceResponse<List<GetEventDTO>>> GetAllEvents();
        Task<ServiceResponse<GetEventDTO>> GetEventById(int id);
        Task<ServiceResponse<List<GetEventDTO>>> AddEvent(AddEventDTO newEvent);
        Task<ServiceResponse<GetEventDTO>> UpdateEvent(UpdateEventDTO updatedEvent);
        Task<ServiceResponse<GetEventDTO>> DeleteEvent(int id);
    }
}
