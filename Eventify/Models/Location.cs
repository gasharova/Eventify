using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Eventify.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Capacity { get; set; }
        public double SquareMeters { get; set; }
        [Required]
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
        public List<Event> Events { get; set; }
    }
}
