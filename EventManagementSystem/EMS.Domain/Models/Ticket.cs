using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Domain.Models
{
    public class Ticket : BaseEntity
    {
        public Guid AttendeeId { get; set; }
        public Attendee? Attendee { get; set; }
    }
}
