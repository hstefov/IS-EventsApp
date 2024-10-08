﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EMS.Domain.Models
{
    public class ScheduledEvent : BaseEntity
    {
        public string? ImageURL { get; set; }
        public DateTime TimeSlot { get; set; }
        public string? Location { get; set; }
        public Guid EventId { get; set; }
        public Event? Event { get; set; }
        public virtual ICollection<Ticket>? Tickets { get; set; }
        // Computed property
        public string EventDisplayName => Event != null ? $"{Event.EventName} ({Event.HostName})" : string.Empty;
    }
}

