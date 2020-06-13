using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventify.DTOs.Event;
using Eventify.Models;
using Eventify.Services.EventService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Eventify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class EventController : ControllerBase
    {
        private static Event e = new Event();
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id = null)
        {
            if (id != null)
            {
                return Ok(await _eventService.GetEventById(Int32.Parse(id)));
            }
            return Ok(await _eventService.GetAllEvents());
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(AddEventDTO e)
        {
            return Ok(await _eventService.AddEvent(e));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvent(UpdateEventDTO e)
        {
            ServiceResponse<GetEventDTO> response = await _eventService.UpdateEvent(e);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            ServiceResponse<GetEventDTO> response = await _eventService.DeleteEvent(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
