using Eventify.DTOs.TicketDetails;
using Eventify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventify.Services.TicketDetailsService
{
    public interface ITicketDetailsService
    {
        Task<ServiceResponse<List<GetTicketDetailsDTO>>> GetAllTicketDetails();
        Task<ServiceResponse<GetTicketDetailsDTO>> GetTicketDetailsById(int id);
        Task<ServiceResponse<List<GetTicketDetailsDTO>>> AddTicketDetails(AddTicketDetailsDTO newTicketDetails);
        Task<ServiceResponse<GetTicketDetailsDTO>> UpdateTicketDetails(UpdateTicketDetailsDTO updatedTicketDetails);
        Task<ServiceResponse<GetTicketDetailsDTO>> DeleteTicketDetails(int id);
    }
}
