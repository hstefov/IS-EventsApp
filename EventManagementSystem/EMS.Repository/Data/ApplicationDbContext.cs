using EMS.Domain.Identity;
using EMS.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMS.Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext<Attendee>
    {
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<ScheduledEvent> ScheduledEvents { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<TicketInShoppingCart> TicketInShoppingCarts { get; set; }
        public virtual DbSet<TicketInOrder> TicketInOrders { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
