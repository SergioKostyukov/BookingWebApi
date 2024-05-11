using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class AccountController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
