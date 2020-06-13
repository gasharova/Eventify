using AutoMapper;
using Eventify.Data;
using Eventify.DTOs.Location;
using Eventify.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventify.Services.LocationService
{
    public class LocationService : ILocationService
    {
        private readonly IMapper _mapper;

        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public LocationService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetLocationDTO>>> GetAllLocations()
        {
            ServiceResponse<List<GetLocationDTO>> serviceResponse = new ServiceResponse<List<GetLocationDTO>>();
            List<Location> dbLocations = await _context.Locations.Where(l => l.IsDeleted == false).ToListAsync();
            serviceResponse.Data = (dbLocations.Select(e => _mapper.Map<GetLocationDTO>(e))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetLocationDTO>> GetLocationById(int id)
        {
            ServiceResponse<GetLocationDTO> serviceResponse = new ServiceResponse<GetLocationDTO>();
            Location dbLocation = await _context.Locations.Where(l => l.IsDeleted == false)
                .FirstOrDefaultAsync(l => l.Id == id);
            serviceResponse.Data = _mapper.Map<GetLocationDTO>(dbLocation);
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetLocationDTO>>> AddLocation(AddLocationDTO newLocation)
        {
            ServiceResponse<List<GetLocationDTO>> serviceResponse = new ServiceResponse<List<GetLocationDTO>>();
            Location l = _mapper.Map<Location>(newLocation);
            l.IsDeleted = false;
            if((l.Capacity == 0) || (l.Address == null))
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Please enter sufficient data. Capacity or address is missing.";
                return serviceResponse;
            }
            await _context.Locations.AddAsync(l);
            await _context.SaveChangesAsync();

            serviceResponse.Data = (_context.Locations.Select(e => _mapper.Map<GetLocationDTO>(l))).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetLocationDTO>> UpdateLocation(UpdateLocationDTO updatedLocation)
        {
            ServiceResponse<GetLocationDTO> serviceResponse = new ServiceResponse<GetLocationDTO>();
            try
            {
                Location l = await _context.Locations.Where(l => l.IsDeleted == false)
                    .FirstOrDefaultAsync(l => l.Id == updatedLocation.Id);

                l.SquareMeters = updatedLocation.SquareMeters;

                if(updatedLocation.Capacity != 0)
                    l.Capacity = updatedLocation.Capacity;
                if (updatedLocation.Address != null)
                    l.Address = updatedLocation.Address;


                _context.Locations.Update(l);
                await _context.SaveChangesAsync();

                serviceResponse.Data = _mapper.Map<GetLocationDTO>(l);
            }
            catch (Exception exc)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = exc.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<GetLocationDTO>> DeleteLocation(int id)
        {
            ServiceResponse<GetLocationDTO> serviceResponse = new ServiceResponse<GetLocationDTO>();
            try
            {
                Location l = await _context.Locations.Where(l => l.IsDeleted == false)
                    .FirstOrDefaultAsync(l => l.Id == id);

                l.IsDeleted = true;
                _context.Locations.Update(l);
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
