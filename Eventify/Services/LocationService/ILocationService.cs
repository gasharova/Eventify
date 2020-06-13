using Eventify.DTOs.Location;
using Eventify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventify.Services.LocationService
{
    public interface ILocationService
    {
        Task<ServiceResponse<List<GetLocationDTO>>> GetAllLocations();
        Task<ServiceResponse<GetLocationDTO>> GetLocationById(int id);
        Task<ServiceResponse<List<GetLocationDTO>>> AddLocation(AddLocationDTO newLocation);
        Task<ServiceResponse<GetLocationDTO>> UpdateLocation(UpdateLocationDTO updatedLocation);
        Task<ServiceResponse<GetLocationDTO>> DeleteLocation(int id);
    }
}
