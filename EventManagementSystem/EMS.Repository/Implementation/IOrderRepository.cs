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
    public class OrderRepository : IOrderRepository

    {
        private readonly ApplicationDbContext context;
        
        public OrderRepository(ApplicationDbContext context)
        {
            this.context = context;
           
        }
        private DbSet<Order> entities => context.Set<Order>();
        public List<Order> GetAllOrders()
        {
            return entities
                .Include(z => z.TicketsInOrder)
                .Include(z => z.UserAttendee)
                .Include("TicketsInOrder.Ticket")
                .ToList();
        }

        public Order GetDetailsForOrder(BaseEntity id)
        {
            return entities
                .Include(z => z.TicketsInOrder)
                .Include(z => z.UserAttendee)
                .Include("TicketsInOrder.Ticket")
                .SingleOrDefaultAsync(z => z.Id == id.Id).Result;
        }
    }
}
