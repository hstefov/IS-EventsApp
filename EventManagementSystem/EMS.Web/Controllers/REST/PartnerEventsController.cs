using EMS.Domain.DTO;
using EMS.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace EMS.Web.Controllers.REST
{
    public class PartnerEventsController : Controller
    {

        private readonly IPartnerEventService _partnerEventService;

        public PartnerEventsController(IPartnerEventService partnerEventService)
        {
            _partnerEventService = partnerEventService;
        }
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            string URL = "https://web20240913191052.azurewebsites.net/api/PartnerStore/GetAllEvents";

            HttpResponseMessage response = client.GetAsync(URL).Result;
            var data = response.Content.ReadAsAsync<List<PartnerEventDTO>>().Result;
            _partnerEventService.SyncPartnerEvents(data);
            return View(data);
        }

        public IActionResult Details(string id)
        {
            HttpClient client = new HttpClient();

            string URL = "https://web20240913191052.azurewebsites.net/api/PartnerStore/GetDetailsForEvent/" + id;

            HttpResponseMessage response = client.GetAsync(URL).Result;
            var data = response.Content.ReadAsAsync<PartnerEventDTO>().Result;
            return View(data);
        }
    }
}
