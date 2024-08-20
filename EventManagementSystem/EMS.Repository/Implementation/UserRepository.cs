using EMS.Domain.Identity;
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
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext context;
        private DbSet<Attendee> entities;
        string errorMessage = string.Empty;

        public UserRepository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<Attendee>();
        }
        public IEnumerable<Attendee> GetAll()
        {
            return entities.AsEnumerable();
        }

        public Attendee Get(string id)
        {
            return entities
               .Include(z => z.ShoppingCart)
               .Include("ShoppingCart.TicketsInShoppingCart")
               .Include("ShoppingCart.TicketsInShoppingCart.Ticket")
               .Include("ShoppingCart.TicketsInShoppingCart.Ticket.ScheduledEvent")
               .Include("ShoppingCart.TicketsInShoppingCart.Ticket.ScheduledEvent.Event")
               .SingleOrDefault(s => s.Id == id);
        }
        public void Insert(Attendee entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }

        public void Update(Attendee entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            context.SaveChanges();
        }

        public void Delete(Attendee entity)
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
