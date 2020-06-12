﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eventify.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EventDate { get; set; }
        public string Location { get; set; }
        public int NumberOfAttendees { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}