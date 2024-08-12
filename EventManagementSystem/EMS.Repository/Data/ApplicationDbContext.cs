using EMS.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EMS.Repository.Data
{
    public class ApplicationDbContext : IdentityDbContext<Attendee>
    {
        public virtual DbSet<Attendee> Attendees { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<EventAttendee> EventAttendees { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
