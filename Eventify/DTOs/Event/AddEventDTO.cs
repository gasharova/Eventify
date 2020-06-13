using Eventify.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventify.DTOs.Event
{
    public class AddEventDTO
    {
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public int NumberOfAttendees { get; set; }
        public bool IsActive { get; set; }
        public User User { get; set; }
    }
}
