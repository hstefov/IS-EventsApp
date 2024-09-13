using EMS.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace EMS.Web.Controllers.REST
{
    public class PartnerEventsController : Controller
    {
        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            string URL = "https://web20240913191052.azurewebsites.net/api/PartnerStore/GetAllEvents";

            HttpResponseMessage response = client.GetAsync(URL).Result;
            var data = response.Content.ReadAsAsync<List<PartnerEventDTO>>().Result;
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
