using AutoMapper;
using Eventify.Data;
using Eventify.DTOs.Event;
using Eventify.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Eventify.Services.EventService
{
    public class EventService : IEventService
    {
        private readonly IMapper _mapper;

        private readonly DataContext _context;
        public EventService(IMapper mapper, DataContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetEventDTO>>> GetAllEvents()
        {
            ServiceResponse<List<GetEventDTO>> serviceResponse = new ServiceResponse<List<GetEventDTO>>();
            List<Event> dbEvents = await _context.Events.ToListAsync();
            serviceResponse.Data = (dbEvents.Select(e => _mapper.Map<GetEventDTO>(e))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEventDTO>> GetEventById(int id)
        {
            ServiceResponse<GetEventDTO> serviceResponse = new ServiceResponse<GetEventDTO>();
            Event dbEvent = await _context.Events.FirstOrDefaultAsync(e => e.Id == id);
            serviceResponse.Data = _mapper.Map<GetEventDTO>(dbEvent);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetEventDTO>>> AddEvent(AddEventDTO newEvent)
        {
            ServiceResponse<List<GetEventDTO>> serviceResponse = new ServiceResponse<List<GetEventDTO>>();
            Event e = _mapper.Map<Event>(newEvent);

            await _context.Events.AddAsync(e);
            await _context.SaveChangesAsync();

            serviceResponse.Data = (_context.Events.Select(e => _mapper.Map<GetEventDTO>(e))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEventDTO>> UpdateEvent(UpdateEventDTO updatedEvent)
        {
            ServiceResponse<GetEventDTO> serviceResponse = new ServiceResponse<GetEventDTO>();

            try
            {
                Event e = await _context.Events.FirstOrDefaultAsync(ev => ev.Id == updatedEvent.Id);

                e.Name = updatedEvent.Name;
                e.EventDate = updatedEvent.EventDate;
                e.Location = updatedEvent.Location;
                e.NumberOfAttendees = updatedEvent.NumberOfAttendees;
                e.CreatedBy = updatedEvent.CreatedBy;
                e.IsActive = updatedEvent.IsActive;

                _context.Events.Update(e);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetEventDTO>(e);
            }
            catch(Exception exc)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "No such record. Check id.";
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEventDTO>> DeleteEvent(int id)
        {
            ServiceResponse<GetEventDTO> serviceResponse = new ServiceResponse<GetEventDTO>();
            try
            {
                Event e = await _context.Events.FirstAsync(ev => ev.Id == id);
                _context.Events.Remove(e);
                await _context.SaveChangesAsync();
            }
            catch(Exception exc)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "No such record. Check id.";
            }
            return serviceResponse;
        }
    }
}
