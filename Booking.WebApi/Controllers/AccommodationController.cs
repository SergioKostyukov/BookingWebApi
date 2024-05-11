using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccommodationController : ControllerBase
    {
        [HttpGet("GetAccommodation")]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
