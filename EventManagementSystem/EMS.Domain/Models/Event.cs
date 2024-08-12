using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections.Generic;

namespace EMS.Domain.Models
{
    public class Event : BaseEntity
    {
        public string? Name { get; set; }
        public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();
        public virtual ICollection<EventAttendee> EventAttendees { get; set; } = new List<EventAttendee>();
    }
}
