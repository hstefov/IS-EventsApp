using EMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Domain.DTO
{
    public class TicketDTO
    {
        public Guid? Id { get; set; }
        public double? Price { get; set; }
        public Guid ScheduledEventId { get; set; }
        public List<ScheduledEvent> ListModel { get; set; }
    }
}
