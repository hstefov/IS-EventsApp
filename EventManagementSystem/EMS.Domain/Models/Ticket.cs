using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EMS.Domain.Identity;

namespace EMS.Domain.Models
{
    public class Ticket : BaseEntity
    {
        public double price { get; set; }
        public Guid ScheduledEventId { get; set; }
        public ScheduledEvent? ScheduledEvent { get; set; }
        public virtual ICollection<TicketInShoppingCart>? TicketsInShoppingCart { get; set; }
        public virtual ICollection<TicketInOrder>? TicketsInOrder { get; set; }

    }
}
