using EMS.Domain.DTO;
using EMS.Domain.Identity;
using EMS.Domain.Models;
using EMS.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EMS.Web.Controllers.REST
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly UserManager<Attendee> _userManager;
        public AdminController(IOrderService orderService, UserManager<Attendee> userManager)
        {
            _orderService = orderService;
            _userManager = userManager;
        }

        [HttpGet("[action]")]
        public List<Order> GetAllOrders()
        {
            return this._orderService.GetAllOrders();
        }

        [HttpPost("[action]")]
        public Order GetDetails(BaseEntity id)
        {
            return this._orderService.GetDetailsForOrder(id);
        }

        [HttpPost("[action]")]
        public bool ImportAllAttendees(List<ImportAttendeeDTO> model)
        {
            bool status = true;

            foreach (var item in model)
            {
                var attendeeCheck = _userManager.FindByEmailAsync(item.Email).Result;

                if (attendeeCheck == null)
                {
                    var attendee = new Attendee
                    {
                        UserName = item.Email,
                        NormalizedUserName = item.Email,
                        Email = item.Email,
                        EmailConfirmed = true,
                        ShoppingCart = new ShoppingCart()
                    };

                    var result = _userManager.CreateAsync(attendee, item.Password).Result;
                    status = status && result.Succeeded;
                }
                else
                {
                    continue;
                }
            }
            return status;
        }

    }
}
