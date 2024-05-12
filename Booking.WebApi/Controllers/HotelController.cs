using Booking.Application.Dto;
using Booking.Application.Interfaces;
using Booking.WebApi.Identity.Constants;
using Booking.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class HotelController(IHttpContextAccessor httpContextAccessor,
                                 IHotelService hotelService) : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IHotelService _hotelService = hotelService;

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var hotels = await _hotelService.GetList();

            return Ok(new { Hotels = hotels });
        }

        [HttpGet]
        [Authorize(Policy = IdentityConstants.ManagerUserPolicyName)]
        public async Task<IActionResult> GetMyHotelsList()
        {
            string? userId = _httpContextAccessor.HttpContext?.User.FindFirst("UserId")?.Value
                ?? throw new InvalidOperationException("User not found");

            var hotels = await _hotelService.GetListByUserId(int.Parse(userId));

            return Ok(new { Hotels = hotels });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var hotel = await _hotelService.Get(id);

            return Ok(new { Hotel = hotel });
        }

        [HttpPost]
        [Authorize(Policy = IdentityConstants.ManagerUserPolicyName)]
        public async Task<IActionResult> Add(AddHotelModel model)
        {
            string? userId = _httpContextAccessor.HttpContext?.User.FindFirst("UserId")?.Value
                ?? throw new InvalidOperationException("User not found");

            await _hotelService.Add(new HotelAddDto
            {
                ManagerId = int.Parse(userId),
                Name = model.Name,
                Type = model.Type,
                Description = model.Description,
                Country = model.Country,
                City = model.City,
                Address = model.Address,
            });

            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = IdentityConstants.ManagerUserPolicyName)]
        public async Task<IActionResult> Update(UpdateHotelModel model)
        {
            await _hotelService.Update(new HotelUpdateDto(
               Id: model.Id,
               Name: model.Name,
               Type: model.Type,
               Description: model.Description,
               Country: model.Country,
               City: model.City,
               Address: model.City
           ));

            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = IdentityConstants.ManagerUserPolicyName)]
        public async Task<IActionResult> Delete(int id)
        {
            await _hotelService.Delete(id);

            return Ok();
        }
    }
}
