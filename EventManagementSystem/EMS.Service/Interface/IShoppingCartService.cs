using EMS.Domain.DTO;
using EMS.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDTO GetShoppingCart(string userAttendeeId);
        bool deleteTicketFromShoppingCart(string userAttendeeId, Guid ticketId);
        bool order(string userAttendeeId);
        bool AddToShoppingCartConfirmed(TicketInShoppingCart ticket, string userAttendeeId);
    }
}
