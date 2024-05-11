using Booking.WebApi.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        [Authorize(Policy = IdentityConstants.ClientUserPolicyName)]
        public IActionResult GetList()
        {
            return Ok();
        }

        [HttpGet]
        [Authorize(Policy = IdentityConstants.ManagerUserPolicyName)]
        public IActionResult GetListOrdersToMe()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        [Authorize(Policy = IdentityConstants.ClientOrManagerUserPolicyName)]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        [HttpPost]
        [Authorize(Policy = IdentityConstants.ClientUserPolicyName)]
        public IActionResult Add()
        {
            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = IdentityConstants.ClientOrManagerUserPolicyName)]
        public IActionResult Edit()
        {
            return Ok();
        }

        [HttpDelete]
        [Authorize(Policy = IdentityConstants.ClientOrManagerUserPolicyName)]
        public IActionResult Delete()
        {
            return Ok();
        }
    }
}
