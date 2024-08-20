using EMS.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Domain.Models
{
    public class Order : BaseEntity
    {
        public string? UserAttendeeId { get; set; }
        public Attendee? UserAttendee { get; set; }
        public virtual ICollection<TicketInOrder>? TicketsInOrder { get; set; }
        
    }
}
