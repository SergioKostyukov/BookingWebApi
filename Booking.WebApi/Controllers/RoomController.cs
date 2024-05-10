using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
