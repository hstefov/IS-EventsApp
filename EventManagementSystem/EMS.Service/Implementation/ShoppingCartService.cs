using EMS.Domain.DTO;
using EMS.Domain.Models;
using EMS.Repository.Interface;
using EMS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service.Implementation
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository<ShoppingCart> _shoppingCartRepository;
        private readonly IRepository<TicketInShoppingCart> _ticketInShoppingCartRepository;
        private readonly IRepository<TicketInOrder> _ticketInOrderRepository;
        private readonly IRepository<Order> _orderRepository;
        private readonly IUserRepository _userRepository;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, 
                                   IRepository<TicketInShoppingCart> ticketInShoppingCartRepository, 
                                   IRepository<TicketInOrder> ticketInOrderRepository, 
                                   IRepository<Order> orderRepository, 
                                   IUserRepository userRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _ticketInShoppingCartRepository = ticketInShoppingCartRepository;
            _ticketInOrderRepository = ticketInOrderRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
        }

        public bool AddToShoppingCartConfirmed(TicketInShoppingCart ticket, string userAttendeeId)
        {
            var loggedInUser = _userRepository.Get(userAttendeeId);
            var userShoppingCart = loggedInUser.ShoppingCart;
            if (userShoppingCart != null)
            {
                userShoppingCart.TicketsInShoppingCart = new List<TicketInShoppingCart>();  
            }
            userShoppingCart.TicketsInShoppingCart.Add(ticket);
            _shoppingCartRepository.Update(userShoppingCart);
            return true;
        }

        public bool deleteTicketFromShoppingCart(string userAttendeeId, Guid ticketId)
        {
            if(ticketId != null)
            {
                var loggedInUser = _userRepository.Get(userAttendeeId);
                var userShoppingCart = loggedInUser.ShoppingCart;
                var ticket = userShoppingCart.TicketsInShoppingCart.Where(x => x.TicketId == ticketId).FirstOrDefault();
                userShoppingCart.TicketsInShoppingCart.Remove(ticket);
                _shoppingCartRepository.Update(userShoppingCart);
                return true;
            }
            return false;
        }

        public ShoppingCartDTO GetShoppingCart(string userAttendeeId)
        {
            var loggedInUser = _userRepository.Get(userAttendeeId);
            var userShoppingCart = loggedInUser.ShoppingCart;
            var allTickets = userShoppingCart?.TicketsInShoppingCart?.ToList();
            var totalPrice = allTickets.Select(x => (x.Ticket.price * x.Quantity)).Sum();
            ShoppingCartDTO dto = new ShoppingCartDTO
            {
                Tickets = allTickets,
                TotalPrice = totalPrice
            };
            return dto;
        }

        public bool order(string userAttendeeId)
        {
            if (userAttendeeId != null)
            {
                var loggedInUser = _userRepository.Get(userAttendeeId);
                var userShoppingCart = loggedInUser.ShoppingCart;
                var ticketsInOrder = new List<TicketInOrder>();

                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    UserAttendeeId = userAttendeeId,
                    UserAttendee = loggedInUser
                };

                var ticketsList = userShoppingCart.TicketsInShoppingCart.Select(x => 
                        new TicketInOrder
                        {
                            Id = Guid.NewGuid(),
                            Ticket = x.Ticket,
                            TicketId = x.TicketId,
                            Order = order,
                            OrderId = order.Id,
                            Quantity = x.Quantity
                        }).ToList();

                ticketsInOrder.AddRange(ticketsList);
                foreach (var ticket in ticketsInOrder)
                {
                    _ticketInOrderRepository.Insert(ticket);
                }

                loggedInUser.ShoppingCart.TicketsInShoppingCart.Clear();
                _userRepository.Update(loggedInUser);

                return true;
            }
            return false;
        }
    }
}
