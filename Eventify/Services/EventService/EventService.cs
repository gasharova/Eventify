using AutoMapper;
using Eventify.DTOs.Event;
using Eventify.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private readonly IMapper _mapper;

        public EventService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetEventDTO>>> AddEvent(AddEventDTO newEvent)
        {
            ServiceResponse<List<GetEventDTO>> serviceResponse = new ServiceResponse<List<GetEventDTO>>();

            Event e = _mapper.Map<Event>(newEvent);
            e.Id = events.Max(e => e.Id) + 1;
            events.Add(e);

            serviceResponse.Data = (events.Select(e => _mapper.Map<GetEventDTO>(e))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetEventDTO>>> GetAllEvents()
        {
            ServiceResponse<List<GetEventDTO>> serviceResponse = new ServiceResponse<List<GetEventDTO>>();
            serviceResponse.Data = (events.Select(e => _mapper.Map<GetEventDTO>(e))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEventDTO>> GetEventById(int id)
        {
            ServiceResponse<GetEventDTO> serviceResponse = new ServiceResponse<GetEventDTO>();
            serviceResponse.Data = _mapper.Map<GetEventDTO>(events.FirstOrDefault(c => c.Id == id));
            return serviceResponse;
        }
    }
}
