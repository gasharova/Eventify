using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Eventify.DTOs.Event;
using Eventify.DTOs.Location;
using Eventify.DTOs.TicketDetails;
using Eventify.Models;

namespace Eventify
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Event, GetEventDTO>();
            CreateMap<AddEventDTO, Event>();
            CreateMap<Location, GetLocationDTO>();
            CreateMap<AddLocationDTO, Location>();
            CreateMap<TicketDetails, GetTicketDetailsDTO>();
            CreateMap<AddTicketDetailsDTO, TicketDetails>();
        }
    }
}
