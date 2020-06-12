using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Eventify.Models;
using Eventify.Services.EventService;
using Microsoft.AspNetCore.Mvc;

namespace Eventify.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
                return Ok(_eventService.GetEventById(Int32.Parse(id)));
            }
            return Ok(_eventService.GetAllEvents());
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(Event e)
        {
            return Ok(_eventService.AddEvent(e));
        }
    }
}
