using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccommodationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
