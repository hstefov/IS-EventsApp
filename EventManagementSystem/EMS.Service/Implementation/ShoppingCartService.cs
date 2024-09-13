using EMS.Domain.DTO;
using EMS.Domain.MailMessage;
using EMS.Domain.Models;
using EMS.Repository.Interface;
using EMS.Service.Interface;
using System;
using System.Collections;
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
        private readonly IEmailService _emailService;

        public ShoppingCartService(IRepository<ShoppingCart> shoppingCartRepository, 
                                   IRepository<TicketInShoppingCart> ticketInShoppingCartRepository, 
                                   IRepository<TicketInOrder> ticketInOrderRepository, 
                                   IRepository<Order> orderRepository, 
                                   IUserRepository userRepository,
                                   IEmailService emailservice)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _ticketInShoppingCartRepository = ticketInShoppingCartRepository;
            _ticketInOrderRepository = ticketInOrderRepository;
            _orderRepository = orderRepository;
            _userRepository = userRepository;
            _emailService = emailservice;
        }

        public bool AddToShoppingCartConfirmed(TicketInShoppingCart ticket, string userAttendeeId)
        {
            var loggedInUser = _userRepository.Get(userAttendeeId);
            var userShoppingCart = loggedInUser.ShoppingCart;
            if (userShoppingCart == null)
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
            var totalPrice = allTickets.Select(x => (x.Ticket.Price * x.Quantity)).Sum();
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
                EmailMessage message = new EmailMessage();
                message.Subject = "Successfull order for purchasing tickets";
                message.MailTo = loggedInUser.Email;
                Order order = new Order
                {
                    Id = Guid.NewGuid(),
                    UserAttendeeId = userAttendeeId,
                    UserAttendee = loggedInUser
                };
                _orderRepository.Insert(order);


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

                var ticketsInOrder = new List<TicketInOrder>();
                StringBuilder sb = new StringBuilder();

                var totalPrice = 0.0;

                sb.AppendLine("Your order is completed. The order contains the following tickets: ");
                for (int i = 1; i <= ticketsList.Count(); i++)
                {
                    var currentItem = ticketsList[i - 1];
                    totalPrice += currentItem.Quantity * currentItem.Ticket.Price;
                    sb.AppendLine(i.ToString() + ". " + currentItem.Quantity+ " tickets for " + currentItem.Ticket.ScheduledEvent.Event.EventName + " with price of: $" + currentItem.Ticket.Price);
                }
                sb.AppendLine("Total price for your order: " + totalPrice.ToString());
                message.Content = sb.ToString();


                ticketsInOrder.AddRange(ticketsList);
                foreach (var ticket in ticketsInOrder)
                {
                    _ticketInOrderRepository.Insert(ticket);
                }

                loggedInUser.ShoppingCart.TicketsInShoppingCart.Clear();
                _userRepository.Update(loggedInUser);
                this._emailService.SendEmailAsync(message);
                return true;
            }
            return false;
        }
    }
}
