using Microsoft.AspNetCore.Mvc;

namespace EmsAdminApp.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
