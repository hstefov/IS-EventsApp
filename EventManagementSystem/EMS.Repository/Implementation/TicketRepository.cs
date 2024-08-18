using EMS.Domain.Models;
using EMS.Repository.Data;
using EMS.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Repository.Implementation
{
    public class TicketRepository : ITicketRepository

    {
        private readonly ApplicationDbContext context;
        private DbSet<Ticket> entities;

        public TicketRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Ticket>();
        }

        public List<Ticket> GetAllTickets()
        {
            return entities
                    .Include(z => z.ScheduledEvent)
                    .ToList();
        }

        public Ticket GetDetailsForTicket(BaseEntity id)
        {
            return entities
                .Include(z => z.ScheduledEvent)
                .SingleOrDefaultAsync(z => z.Id == id.Id).Result;
        }
    }
}
