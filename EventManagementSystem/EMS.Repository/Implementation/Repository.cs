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
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
          
            if (typeof(T) == typeof(ScheduledEvent))
            {
                var scheduledEvents = entities as DbSet<ScheduledEvent>;
                return scheduledEvents?.Include(e => e.Event).ToList() as IEnumerable<T>;
            }

            if (typeof(T) == typeof(Ticket))
            {
                var tickets = entities as DbSet<Ticket>;
                return tickets?
                    .Include(t => t.ScheduledEvent)
                        .ThenInclude(se => se.Event)
                    .ToList() as IEnumerable<T>;
            }

            return entities.AsEnumerable();
        }


        public T Get(Guid? id)
        {
            if (typeof(T) == typeof(ScheduledEvent))
            {
                return entities.Include("Event").SingleOrDefault(s => s.Id == id) as T;
            }

            if (typeof(T) == typeof(Ticket))
            {
                return entities.Include("ScheduledEvent").Include("ScheduledEvent.Event").SingleOrDefault(s => s.Id == id) as T;
            }

            return entities.SingleOrDefault(s => s.Id == id);
        }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            if (entity is ScheduledEvent scheduledEvent)
            {
                // Ensure related Event entity is tracked
                var existingEvent = context.Events.Find(scheduledEvent.EventId);
                if (existingEvent == null)
                {
                    throw new InvalidOperationException("The related Event does not exist.");
                }
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}
