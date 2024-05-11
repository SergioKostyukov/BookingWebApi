using Booking.WebApi.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HotelController : ControllerBase
    {
        [HttpGet]
        public IActionResult Index()
        {
            return Ok();
        }

        [Authorize(Policy = IdentityConstants.AdminUserPolicyName)]
        [HttpGet]
        public IActionResult Secret()
        {
            return Ok();
        }
    }
}
