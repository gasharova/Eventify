using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventify.Models
{
    public class TicketDetails
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public Event Event { get; set; }
        public DateTime DateAdded { get; set; }
        public double Price { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }
}
