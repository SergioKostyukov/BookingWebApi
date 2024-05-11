using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        [HttpGet("GetOrder")]
        public IActionResult Index()
        {
            return Ok();
        }
    }
}
