using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventify.DTOs.TicketDetails
{
    public class UpdateTicketDetailsDTO
    {
        public int Id { get; set; }
        public Eventify.Models.Event Event { get; set; }
        public DateTime DateAdded { get; set; }
        public double Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}
