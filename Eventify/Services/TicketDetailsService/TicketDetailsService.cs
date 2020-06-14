using AutoMapper;
using Eventify.Data;
using Eventify.DTOs.TicketDetails;
using Eventify.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventify.Services.TicketDetailsService
{
    public class TicketDetailsService : ITicketDetailsService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TicketDetailsService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<GetTicketDetailsDTO>>> GetAllTicketDetails()
        {
            ServiceResponse<List<GetTicketDetailsDTO>> serviceResponse = new ServiceResponse<List<GetTicketDetailsDTO>>();
            List<TicketDetails> dbDetails = await _context.TicketDetails.Where(td => td.IsDeleted == false).ToListAsync();
            serviceResponse.Data = (dbDetails.Select(e => _mapper.Map<GetTicketDetailsDTO>(e))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTicketDetailsDTO>> GetTicketDetailsById(int id)
        {
            ServiceResponse<GetTicketDetailsDTO> serviceResponse = new ServiceResponse<GetTicketDetailsDTO>();
            TicketDetails dbDetails = await _context.TicketDetails.Where(td => td.IsDeleted == false)
                .FirstOrDefaultAsync(td => td.Id == id);
            serviceResponse.Data = _mapper.Map<GetTicketDetailsDTO>(dbDetails);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetTicketDetailsDTO>>> AddTicketDetails(AddTicketDetailsDTO newTicketDetails)
        {
            ServiceResponse<List<GetTicketDetailsDTO>> serviceResponse = new ServiceResponse<List<GetTicketDetailsDTO>>();
            TicketDetails td = _mapper.Map<TicketDetails>(newTicketDetails);
            td.IsDeleted = false;
            td.DateAdded = DateTime.Now;
            if (td.Event == null)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Please enter sufficient data. Event is missing.";
                return serviceResponse;
            }
            await _context.TicketDetails.AddAsync(td);
            await _context.SaveChangesAsync();

            serviceResponse.Data = (_context.TicketDetails.Select(e => _mapper.Map<GetTicketDetailsDTO>(td))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTicketDetailsDTO>> UpdateTicketDetails(UpdateTicketDetailsDTO updatedTicketDetails)
        {
            ServiceResponse<GetTicketDetailsDTO> serviceResponse = new ServiceResponse<GetTicketDetailsDTO>();
            try
            {
                TicketDetails td = await _context.TicketDetails.Where(td => td.IsDeleted == false)
                    .FirstOrDefaultAsync(td => td.Id == updatedTicketDetails.Id);

                td.DateAdded = updatedTicketDetails.DateAdded;
                td.Price = updatedTicketDetails.Price;

                if (updatedTicketDetails.Event != null)
                    td.Event = updatedTicketDetails.Event;

                _context.TicketDetails.Update(td);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetTicketDetailsDTO>(td);
            }
            catch (Exception exc)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = exc.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetTicketDetailsDTO>> DeleteTicketDetails(int id)
        {
            ServiceResponse<GetTicketDetailsDTO> serviceResponse = new ServiceResponse<GetTicketDetailsDTO>();
            try
            {
                TicketDetails td = await _context.TicketDetails.Where(td => td.IsDeleted == false)
                    .FirstOrDefaultAsync(td => td.Id == id);

                td.IsDeleted = true;

                _context.TicketDetails.Update(td);
                await _context.SaveChangesAsync();
            }
            catch (Exception exc)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = exc.Message;
            }

            return serviceResponse;
        }
    }
}
