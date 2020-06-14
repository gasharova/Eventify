using Eventify.DTOs.TicketDetails;
using Eventify.Models;
using Eventify.Services.TicketDetailsService;
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
    public class TicketDetailsController : ControllerBase
    {
        private readonly ITicketDetailsService _tdService;

        public TicketDetailsController(ITicketDetailsService tdService)
        {
            _tdService = tdService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string id = null)
        {
            if (id != null)
            {
                return Ok(await _tdService.GetTicketDetailsById(Int32.Parse(id)));
            }
            return Ok(await _tdService.GetAllTicketDetails());
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddTicketDetailsDTO td)
        {
            return Ok(await _tdService.AddTicketDetails(td));
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateTicketDetailsDTO td)
        {
            ServiceResponse<GetTicketDetailsDTO> response = await _tdService.UpdateTicketDetails(td);
            if (response.Data == null)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResponse<GetTicketDetailsDTO> response = await _tdService.DeleteTicketDetails(id);
            if (response.Data == null)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
