using Booking.Identity.Dto;
using Booking.Identity.Services;
using Booking.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class IdentityController(IIdentityService identityService) : ControllerBase
    {
        private readonly IIdentityService _identityService = identityService;

        /// <summary>
        /// Logs in a user with the provided credentials.
        /// </summary>
        /// <param name="model">The model containing user login credentials.</param>
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var userData = await _identityService.GetUserIdentity(model.Name, model.Password);

            var token = _identityService.GenerateToken(userData);

            if (token.IsNullOrEmpty())
            {
                return BadRequest("Can`t login this user");
            }
            else
            {
                return Ok(new { Token = token });
            }
        }

        /// <summary>
        /// Registers a new user with the provided information.
        /// </summary>
        /// <param name="model">The model containing user registration information.</param>
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            try
            {
                await _identityService.Register(new UserRegisterDto
                {
                    Name = model.Name,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    Role = model.Role,
                    Password = model.Password,
                });

                var userData = await _identityService.GetUserIdentity(model.Name, model.Password);

                var token = _identityService.GenerateToken(userData);

                if (token.IsNullOrEmpty())
                {
                    return BadRequest("Can't register this user");
                }
                else
                {
                    return Ok(new { Token = token });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while registering the user: " + ex.Message);
            }
        }
    }
}
