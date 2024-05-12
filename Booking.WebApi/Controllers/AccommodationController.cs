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
    public class AccommodationController(IAccommodationService accommodationService) : ControllerBase
    {
        private readonly IAccommodationService _accommodationService = accommodationService;

        /// <summary>
        /// Retrieves a list of accommodations for a specific hotel.
        /// Policy requirements: none 
        /// </summary>
        /// <param name="hotelId">The ID of the hotel to retrieve accommodations for.</param>
        [HttpGet("{hotelId}")]
        public async Task<IActionResult> GetList(int hotelId)
        {
            var accommodations = await _accommodationService.GetList(hotelId);

            return Ok(new { Accommodations = accommodations });
        }

        /// <summary>
        /// Retrieves a specific accommodation by its ID.
        /// Policy requirements: none
        /// </summary>
        /// <param name="id">The ID of the accommodation to retrieve.</param>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var accommodation = await _accommodationService.Get(id);

            return Ok(new { Accommodation = accommodation });
        }

        /// <summary>
        /// Adds a new accommodation.
        /// Policy requirements: Managers only 
        /// </summary>
        /// <param name="model">The model containing information about the new accommodation.</param>
        [HttpPost]
        [Authorize(Policy = IdentityConstants.ManagerUserPolicyName)]
        public async Task<IActionResult> Add(AddAccommodationModel model)
        {
            await _accommodationService.Add(new AccommodationAddDto
            {
                HotelId = model.HotelId,
                Name = model.Name,
                Type = model.Type,
                Description = model.Description,
                Price = model.Price
            }, model.Numder);

            return Ok();
        }

        /// <summary>
        /// Updates an existing accommodation.
        /// Policy requirements: Managers only 
        /// </summary>
        /// <param name="model">The model containing updated information about the accommodation.</param>
        [HttpPut]
        [Authorize(Policy = IdentityConstants.ManagerUserPolicyName)]
        public async Task<IActionResult> Update(UpdateAccommodationModel model)
        {
           await _accommodationService.Update(new AccommodationUpdateDto(
               Id: model.Id,
               Name: model.Name,
               Type: model.Type,
               Description: model.Description, 
               Price: model.Price
           ));

            return Ok();
        }

        /// <summary>
        /// Deletes an accommodation by its ID.
        /// Policy requirements: Managers only 
        /// </summary>
        /// <param name="id">The ID of the accommodation to delete.</param>
        [HttpDelete("{id}")]
        [Authorize(Policy = IdentityConstants.ManagerUserPolicyName)]
        public async Task<IActionResult> Delete(int id)
        {
            await _accommodationService.Delete(id);

            return Ok();
        }
    }
}
