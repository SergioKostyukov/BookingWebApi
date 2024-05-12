using Booking.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class AccountController(IHttpContextAccessor httpContextAccessor,
                                   IAccountService accountService) : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IAccountService _accountService = accountService;

        /// <summary>
        /// Retrieves the currently authenticated user's information.
        /// Policy requirements: Authorized users only 
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string? userId = _httpContextAccessor.HttpContext?.User.FindFirst("UserId")?.Value
                ?? throw new InvalidOperationException("User not found");

            var user = await _accountService.Get(int.Parse(userId));

            return Ok(new { User = user });
        }

        /// <summary>
        /// Retrieves user information by their ID.
        /// Policy requirements: Authorized users only 
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve.</param>
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var user = await _accountService.Get(userId);

            return Ok(new { User = user });
        }

        /// <summary>
        /// Deletes a user by their ID.
        /// Policy requirements: Authorized users only 
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _accountService.Delete(id);

            return Ok();
        }
    }
}
