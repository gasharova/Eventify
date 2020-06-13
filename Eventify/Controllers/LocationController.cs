using Eventify.DTOs.Location;
using Eventify.Models;
using Eventify.Services.EventService;
using Eventify.Services.LocationService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventify.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id = null)
        {
            if (id != null)
            {
                return Ok(await _locationService.GetLocationById(Int32.Parse(id)));
            }
            return Ok(await _locationService.GetAllLocations());
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddLocationDTO l)
        {
            return Ok(await _locationService.AddLocation(l));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateLocationDTO l)
        {
            ServiceResponse<GetLocationDTO> response = await _locationService.UpdateLocation(l);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            ServiceResponse<GetLocationDTO> response = await _locationService.DeleteLocation(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

    }
}
