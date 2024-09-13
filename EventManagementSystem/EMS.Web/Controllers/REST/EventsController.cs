using EMS.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EMS.Domain.Models;

namespace EMS.Web.Controllers.REST
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {

        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet("[action]")]
        public List<Event> GetAllEvents()
        {
            return this._eventService.GetAllEvents();
        }

        [HttpGet("{id}")]
        public Event GetDetailsForEvent(Guid id)
        {
            return this._eventService.GetDetailsForEvent(id);
        }

    }
}
