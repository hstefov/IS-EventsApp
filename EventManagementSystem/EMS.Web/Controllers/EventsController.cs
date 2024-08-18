using EMS.Domain.Models;
using EMS.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EMS.Web.Controllers
{
    public class EventsController : Controller
    {


        private readonly IEventService _eventService;
        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        public IActionResult Index()
        {
            return View(_eventService.GetAllEvents());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _eventService.GetDetailsForEvent(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,EventName,HostName,ImageUrl")] Event newEvent)
        {
            if (ModelState.IsValid)
            {
                newEvent.Id = Guid.NewGuid();
                _eventService.CreateNewEvent(newEvent);
                return RedirectToAction(nameof(Index));
            }
            return View(newEvent);
        }


        // GET: Events/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventDetails = _eventService.GetDetailsForEvent(id);

            if (eventDetails == null)
            {
                return NotFound();
            }

            return View(eventDetails);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,EventName,HostName,ImageUrl")] Event eventDetails)
        {
            if (id != eventDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _eventService.UpdateExistingEvent(eventDetails);
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(eventDetails);
        }

        // GET: Events/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventDetails = _eventService.GetDetailsForEvent(id);
            if (eventDetails == null)
            {
                return NotFound();
            }

            return View(eventDetails);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            _eventService.DeleteEvent(id);
            return RedirectToAction(nameof(Index));
        }



    }
}
