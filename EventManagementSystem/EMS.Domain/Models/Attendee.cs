using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace EMS.Domain.Models
{
    public class Attendee : IdentityUser
    {
        public string? Name { get; set; }
        public ICollection<EventAttendee> EventAttendees { get; set; } = new List<EventAttendee>();
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
