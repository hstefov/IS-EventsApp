using EMS.Domain.DTO;
using EMS.Domain.Models;
using EMS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMS.Service.Implementation
{
    public class PartnerEventService : IPartnerEventService
    {
        private readonly HttpClient _httpClient;
        private readonly IEventService _eventService; 

        public PartnerEventService(HttpClient httpClient, IEventService eventService)
        {
            _httpClient = httpClient;
            _eventService = eventService;
        }

        public async Task SyncPartnerEvents(List<PartnerEventDTO> partnerEvents)
        {
            List<Guid> eventIds = _eventService.GetAllEventsIds();
            foreach (var partnerEvent in partnerEvents)
            {
                if (!eventIds.Contains(partnerEvent.Id)){ 
                    var newEvent = new Event
                    {
                        Id = partnerEvent.Id,
                        EventName = partnerEvent.Name,
                        HostName = GenerateRandomHostName(), 
                        ImageUrl = "https://t4.ftcdn.net/jpg/01/75/46/69/360_F_175466970_aRDdYku348o2ytRIJgeTuqychevhUe7u.jpg",
                        IsPartnerEvent = true
                    };

                    _eventService.CreateNewEvent(newEvent);
                }
            }
            
        }

        private string GenerateRandomHostName()
        {
            var randomNames = new List<string> { "HostA", "HostB", "HostC", "HostD", "HostE" };
            Random rand = new Random();
            return randomNames[rand.Next(randomNames.Count)];
        }
    }
}
