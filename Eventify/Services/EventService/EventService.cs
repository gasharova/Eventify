using AutoMapper;
using Eventify.Data;
using Eventify.DTOs.Event;
using Eventify.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Eventify.Services.EventService
{
    public class EventService : IEventService
    {
        private readonly IMapper _mapper;

        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public EventService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetEventDTO>>> GetAllEvents()
        {
            ServiceResponse<List<GetEventDTO>> serviceResponse = new ServiceResponse<List<GetEventDTO>>();
            List<Event> dbEvents = await _context.Events.ToListAsync();
            serviceResponse.Data = (dbEvents.Select(e => _mapper.Map<GetEventDTO>(e))).
                Where(e => e.IsDeleted == false).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEventDTO>> GetEventById(int id)
        {
            ServiceResponse<GetEventDTO> serviceResponse = new ServiceResponse<GetEventDTO>();
            Event dbEvent = await _context.Events.Where(e => e.IsDeleted == false).FirstOrDefaultAsync(e => e.Id == id);
            serviceResponse.Data = _mapper.Map<GetEventDTO>(dbEvent);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetEventDTO>>> AddEvent(AddEventDTO newEvent)
        {
            ServiceResponse<List<GetEventDTO>> serviceResponse = new ServiceResponse<List<GetEventDTO>>();
            Event e = _mapper.Map<Event>(newEvent);
            e.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            e.EventDate = DateTime.Now;
            e.IsDeleted = false;
            if(e.NumberOfAttendees == 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Please enter sufficient data. Number of attendees is missing.";
                return serviceResponse;
            }

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
                Event e = await _context.Events.Where(e => e.IsDeleted == false).
                    Include(ev => ev.User).FirstOrDefaultAsync(ev => ev.Id == updatedEvent.Id);

                if (e.User.Id == GetUserId())
                {
                    e.Name = updatedEvent.Name;
                    e.Location = updatedEvent.Location;

                    if (updatedEvent.EventDate != null)
                        e.EventDate = updatedEvent.EventDate;
                    if (updatedEvent.NumberOfAttendees != 0)
                        e.NumberOfAttendees = updatedEvent.NumberOfAttendees;
                    if (updatedEvent.User != null)
                        e.User = updatedEvent.User;
                    if (updatedEvent.IsDeleted != true)
                        e.IsDeleted = updatedEvent.IsDeleted;

                    _context.Events.Update(e);
                    await _context.SaveChangesAsync();

                    serviceResponse.Data = _mapper.Map<GetEventDTO>(e);
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "You do not have the permissions to edit this event. Only the creator of the event can update it.";
                }
            }
            catch(Exception exc)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = exc.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetEventDTO>> DeleteEvent(int id)
        {
            ServiceResponse<GetEventDTO> serviceResponse = new ServiceResponse<GetEventDTO>();
            try
            {
                Event e = await _context.Events.Where(e => e.IsDeleted == false)
                    .Include(ev => ev.User).FirstAsync(ev => ev.Id == id);

                if (e.User.Id == GetUserId())
                {
                    e.IsDeleted = true;
                    _context.Events.Update(e);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = "You do not have the permissions to delete this event. Only the creator of the event can delete it.";
                }
            }
            catch(Exception exc)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "No such record. Check id.";
            }
            return serviceResponse;
        }

        // current user id
        private int GetUserId() =>
            int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
