using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventify.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public DateTime EventDate { get; set; }
        public Location Location { get; set; }
        [Required]
        public int NumberOfAttendees { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
        [Required]
        public User User { get; set; }
        public List<TicketDetails> Details { get; set; }
    }
}
