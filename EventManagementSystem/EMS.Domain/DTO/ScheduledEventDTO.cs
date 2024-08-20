using EMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EMS.Domain.DTO
{
    public class ScheduledEventDTO
    {
        public string? ImageURL { get; set; }
        public DateTime TimeSlot { get; set; }
        public string? Location { get; set; }
        public Guid EventId { get; set; }
        public List<Event> ListModel { get; set; }
    }
}
