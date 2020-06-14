using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventify.DTOs.TicketDetails
{
    public class AddTicketDetailsDTO
    {
        public Eventify.Models.Event Event { get; set; }
        public DateTime DateAdded { get; set; }
        public double Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}
