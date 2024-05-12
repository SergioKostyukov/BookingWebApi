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

        /// <summary>
        /// Retrieves a list of all hotels.
        /// Policy requirements: none 
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var hotels = await _hotelService.GetList();

            return Ok(new { Hotels = hotels });
        }

        /// <summary>
        /// Retrieves a list of hotels managed by the currently authenticated user.
        /// Policy requirements: Managers only
        /// </summary>
        [HttpGet]
        [Authorize(Policy = IdentityConstants.ManagerUserPolicyName)]
        public async Task<IActionResult> GetMyHotelsList()
        {
            string? userId = _httpContextAccessor.HttpContext?.User.FindFirst("UserId")?.Value
                ?? throw new InvalidOperationException("User not found");

            var hotels = await _hotelService.GetListByUserId(int.Parse(userId));

            return Ok(new { Hotels = hotels });
        }

        /// <summary>
        /// Retrieves a specific hotel by its ID.
        /// Policy requirements: none
        /// </summary>
        /// <param name="id">The ID of the hotel to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var hotel = await _hotelService.Get(id);

            return Ok(new { Hotel = hotel });
        }

        /// <summary>
        /// Adds a new hotel.
        /// Policy requirements: Managers only
        /// </summary>
        /// <param name="model">The model containing information about the new hotel.</param>
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

        /// <summary>
        /// Updates an existing hotel.
        /// Policy requirements: Managers only
        /// </summary>
        /// <param name="model">The model containing updated information about the hotel.</param>
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

        /// <summary>
        /// Deletes a hotel by its ID.
        /// Policy requirements: Managers only
        /// </summary>
        /// <param name="id">The ID of the hotel to delete.</param>
        [HttpDelete("{id}")]
        [Authorize(Policy = IdentityConstants.ManagerUserPolicyName)]
        public async Task<IActionResult> Delete(int id)
        {
            await _hotelService.Delete(id);

            return Ok();
        }
    }
}
