using EMS.Domain.DTO;
using EMS.Domain.Models;
using EMS.Service.Implementation;
using EMS.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EMS.Web.Controllers
{
    public class TicketsController : Controller
    {

        private readonly ITicketService _ticketService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IScheduledEventService _scheduledEventService;

        public TicketsController(ITicketService ticketService, 
                                 IShoppingCartService shoppingCartService,
                                 IScheduledEventService scheduledEventService)
        {
            _ticketService = ticketService;
            _shoppingCartService = shoppingCartService;
            _scheduledEventService = scheduledEventService;
        }

        public IActionResult Index()
        {
            return View(_ticketService.GetAllTickets());
        }

        public IActionResult AddToCart(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = _ticketService.GetDetailsForTicket(id);

            TicketInShoppingCart ts = new TicketInShoppingCart();

            if (ticket != null)
            {
                ts.TicketId = ticket.Id;
            }

            return View(ts);
        }

        [HttpPost]
        public IActionResult AddToCartConfirmed(TicketInShoppingCart model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _shoppingCartService.AddToShoppingCartConfirmed(model, userId);
            return View("Index", _ticketService.GetAllTickets());
        }



        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = _ticketService.GetDetailsForTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        public IActionResult Create()
        {
            TicketDTO dto = new TicketDTO()
            {
                ListModel = _scheduledEventService.GetAllScheduledEvents(),
            };

            return View(dto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("ScheduledEventId,Price,ListModel")] TicketDTO newTicket)
        {
            ModelState.Remove("ListModel");
            if (ModelState.IsValid)
            {
                Ticket ticketObject = new Ticket()
                {
                    Id = Guid.NewGuid(),
                    ScheduledEventId = newTicket.ScheduledEventId,
                    Price = (double)newTicket.Price
                };
                _ticketService.CreateNewTicket(ticketObject);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ScheduledEventId"] = new SelectList(_scheduledEventService.GetAllScheduledEvents(), "Id", "EventDisplayName", newTicket.ScheduledEventId);
            return View(newTicket);
        }

        // GET: Tickets/Edit/5
        public IActionResult Edit(Guid id)
        {
            // Fetch the existing ticket details
            var ticket = _ticketService.GetDetailsForTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }

            // Prepare the view model
            var dto = new TicketDTO
            {
                Id = id,
                ScheduledEventId = ticket.ScheduledEventId,
                Price = ticket.Price,
                ListModel = _scheduledEventService.GetAllScheduledEvents()
            };

            return View(dto);
        }

        // POST: Tickets/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Id,ScheduledEventId,Price")] TicketDTO updatedTicket)
        {
            ModelState.Remove("ListModel");
            if (ModelState.IsValid)
            {
                var existingTicket = _ticketService.GetDetailsForTicket(updatedTicket.Id);
                if (existingTicket == null)
                {
                    return NotFound();
                }

                existingTicket.ScheduledEventId = updatedTicket.ScheduledEventId;
                existingTicket.Price = (double)updatedTicket.Price;

                _ticketService.UpdateExistingTicket(existingTicket);

                return RedirectToAction(nameof(Index));
            }

            // Prepare the view model if the model state is invalid
            ViewData["ScheduledEventId"] = new SelectList(_scheduledEventService.GetAllScheduledEvents(), "Id", "EventDisplayName", updatedTicket.ScheduledEventId);
            return View(updatedTicket);
        }



        // GET: Tickets/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticketDetails = _ticketService.GetDetailsForTicket(id);
            if (ticketDetails == null)
            {
                return NotFound();
            }

            return View(ticketDetails);
        }

        // POST: Ticket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _ticketService.DeleteTicket(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
