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
        public string? EventName { get; set; }
        public string? HostName { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsPartnerEvent { get; set; } = false;
        public virtual ICollection<ScheduledEvent> Schedules { get; set; } = new List<ScheduledEvent>();
    }
}
