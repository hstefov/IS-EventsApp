using EMS.Domain.DTO;
using EMS.Domain.Models;
using EMS.Service.Implementation;
using EMS.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace EMS.Web.Controllers
{
    public class ScheduledEventsController : Controller
    {
        private readonly IScheduledEventService _scheduledEventService;
        private readonly IEventService _eventService;
        public ScheduledEventsController(IScheduledEventService scheduledEventService, 
                                         IEventService eventService)
        {
            _scheduledEventService = scheduledEventService;
            _eventService = eventService;
        }

        public IActionResult Index()
        {
            return View(_scheduledEventService.GetAllScheduledEvents());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduledEventDetails = _scheduledEventService.GetDetailsForScheduledEvent(id);
            if (scheduledEventDetails == null)
            {
                return NotFound();
            }

            return View(scheduledEventDetails);
        }
        // GET: ScheduledEvents/Create
        public IActionResult Create()
        {
            ScheduledEventDTO dto = new ScheduledEventDTO()
            {
                 ListModel = _eventService.GetAllEvents(),
                 TimeSlot = DateTime.Now
            };

            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("EventId,ImageURL,TimeSlot,Location,ListModel")] ScheduledEventDTO newEvent)
        {
            ModelState.Remove("ListModel");
            if (ModelState.IsValid)
            {
                ScheduledEvent eventObject = new ScheduledEvent()
                {
                    Id = Guid.NewGuid(),
                    EventId = newEvent.EventId,
                    Location = newEvent.Location,
                    TimeSlot = newEvent.TimeSlot,
                    ImageURL = newEvent.ImageURL
                };
                _scheduledEventService.CreateNewScheduledEvent(eventObject);
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_eventService.GetAllEvents(), "Id", "EventName", newEvent.EventId);
            return View(newEvent);
        }



        // GET: Events/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventDetails = _scheduledEventService.GetDetailsForScheduledEvent(id);

            if (eventDetails == null)
            {
                return NotFound();
            }

            return View(eventDetails);
        }
        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,EventId,EventName,HostName,Location,TimeSlot,ImageURL")] ScheduledEvent eventDetails)
        {
            if (id != eventDetails.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _scheduledEventService.UpdateExistingScheduledEvent(eventDetails);
                }
                catch (DbUpdateConcurrencyException)
                {
                    // Handle concurrency issue
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

            var eventDetails = _scheduledEventService.GetDetailsForScheduledEvent(id);
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
            _scheduledEventService.DeleteScheduledEvent(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
