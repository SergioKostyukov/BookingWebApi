using Booking.Application.Dto;
using Booking.Application.Interfaces;
using Booking.WebApi.Identity;
using Booking.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AccommodationController(IAccommodationService accommodationService) : ControllerBase
    {
        private readonly IAccommodationService _accommodationService = accommodationService;

        [HttpGet("{hotelId}")]
        public async Task<IActionResult> GetList(int hotelId)
        {
            var accommodations = await _accommodationService.GetList(hotelId);

            return Ok(new { Accommodations = accommodations });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var accommodation = await _accommodationService.Get(id);

            return Ok(new { Accommodation = accommodation });
        }

        [HttpPost]
        [Authorize(Policy = IdentityConstants.ManagerUserPolicyName)]
        public async Task<IActionResult> Add(AddAccommodationModel request)
        {
            await _accommodationService.Add(new AccommodationAddDto
            {
                HotelId = request.HotelId,
                Name = request.Name,
                Type = request.Type,
                Description = request.Description,
                Price = request.Price
            }, request.Numder);

            return Ok();
        }

        [HttpPut]
        [Authorize(Policy = IdentityConstants.ManagerUserPolicyName)]
        public IActionResult Edit()
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = IdentityConstants.ManagerUserPolicyName)]
        public async Task<IActionResult> Delete(int id)
        {
            await _accommodationService.Delete(id);

            return Ok();
        }
    }
}
