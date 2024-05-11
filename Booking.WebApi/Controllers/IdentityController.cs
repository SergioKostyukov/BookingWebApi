using Booking.Application.Dto;
using Booking.Application.Interfaces;
using Booking.WebApi.Models.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class IdentityController(IIdentityService identityService,
                                    IAccountService userService) : ControllerBase
    {
        private readonly IIdentityService _identityService = identityService;
        private readonly IAccountService _accountService = userService;

        [HttpPost]
        public IActionResult Login(LoginModel model)
        {
            var userData = _accountService.GetUserIdentity(model.Name, model.Password);

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

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            await _accountService.Create(new UserRegisterDto
            {
                Name = model.Name,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                Role = model.Role,
                Password = model.Password,
            });

            var userData = _accountService.GetUserIdentity(model.Name, model.Password);

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
    }
}
