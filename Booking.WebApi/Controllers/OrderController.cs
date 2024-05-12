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
    public class OrderController(IHttpContextAccessor httpContextAccessor,
                                 IOrderService orderService) : ControllerBase
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
        private readonly IOrderService _orderService = orderService;

        /// <summary>
        /// Retrieves a list of all orders (bookings) for the client.
        /// Policy requirements: Clients only
        /// </summary>
        /// <returns>List of all client orders (bookings)</returns>
        [HttpGet]
        [Authorize(Policy = IdentityConstants.ClientUserPolicyName)]
        public async Task<IActionResult> GetList()
        {
            string? userId = _httpContextAccessor.HttpContext?.User.FindFirst("UserId")?.Value
                ?? throw new InvalidOperationException("User not found");

            var orders = await _orderService.GetList(int.Parse(userId));

            return Ok(new { Orders = orders });
        }

        /// <summary>
        /// Retrieves a list of orders (bookings) assigned to the manager.
        /// Policy requirements: Managers only
        /// </summary>
        [HttpGet]
        [Authorize(Policy = IdentityConstants.ManagerUserPolicyName)]
        public async Task<IActionResult> GetListOrdersToMe()
        {
            string? userId = _httpContextAccessor.HttpContext?.User.FindFirst("UserId")?.Value
                ?? throw new InvalidOperationException("User not found");

            var orders = await _orderService.GetListOrdersToMe(int.Parse(userId));

            return Ok(new { Orders = orders });
        }

        /// <summary>
        /// Retrieves a specific order (booking) by its ID.
        /// Policy requirements: Clients or Managers only
        /// </summary>
        /// <param name="id">The ID of the order to retrieve.</param>
        [HttpGet("{id}")]
        [Authorize(Policy = IdentityConstants.ClientOrManagerUserPolicyName)]
        public async Task<IActionResult> Get(int id)
        {
            var (order, orderItems) = await _orderService.Get(id);

            if (order == null)
            {
                return NotFound();
            }

            var response = new
            {
                Order = order,
                OrderItems = orderItems
            };

            return Ok(response);
        }

        /// <summary>
        /// Adds an accommodation to an existing order (booking).
        /// Policy requirements: Clients only
        /// </summary>
        /// <param name="id">The ID of the accommodation to add.</param>
        [HttpPost("{id}")]
        [Authorize(Policy = IdentityConstants.ClientUserPolicyName)]
        public async Task<IActionResult> AddAccommodationToOrder(int id)
        {
            string? userId = _httpContextAccessor.HttpContext?.User.FindFirst("UserId")?.Value
                ?? throw new InvalidOperationException("User not found");

            await _orderService.Add(new OrderAddDto
            {
                AccommodationId = id,
                CustomerId = int.Parse(userId)
            });

            return Ok();
        }

        /// <summary>
        /// Confirms an order (booking).
        /// Policy requirements: Clients only
        /// </summary>
        /// <param name="model">The model containing confirmation information.</param>
        [HttpPost]
        [Authorize(Policy = IdentityConstants.ClientUserPolicyName)]
        public async Task<IActionResult> Confirm(OrderConfirmModel model)
        {
            await _orderService.Confirm(new OrderConfirmDto
            {
                Id = model.Id,
                StartTime = model.StartTime,
                EndTime = model.EndTime,
                PaymentType = model.PaymentType,
                Comment = model.Comment,
            });

            return Ok();
        }

        /// <summary>
        /// Confirms payment for an order (booking).
        /// Policy requirements: Clients only
        /// </summary>
        /// <param name="id">The ID of the order to confirm payment for.</param>
        [HttpPost]
        [Authorize(Policy = IdentityConstants.ClientUserPolicyName)]
        public async Task<IActionResult> ConfirmPayment(int id)
        {
            await _orderService.ConfirmPayment(id);

            return Ok();
        }

        /// <summary>
        /// Deletes an accommodation from an order (booking).
        /// Policy requirements: Clients or Managers only
        /// </summary>
        /// <param name="orderId">The ID of the order.</param>
        /// <param name="accommodationId">The ID of the accommodation to delete.</param>
        [HttpDelete]
        [Authorize(Policy = IdentityConstants.ClientOrManagerUserPolicyName)]
        public async Task<IActionResult> DeleteAccommodationFromOrder([FromQuery] int orderId, [FromQuery] int accommodationId)
        {
            await _orderService.DeleteAccommodationFromOrder(orderId, accommodationId);

            return Ok();
        }

        /// <summary>
        /// Deletes an order (booking) by its ID.
        /// Policy requirements: Clients or Managers only
        /// </summary>
        /// <param name="id">The ID of the order to delete.</param>
        [HttpDelete("{id}")]
        [Authorize(Policy = IdentityConstants.ClientOrManagerUserPolicyName)]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderService.Delete(id);

            return Ok();
        }
    }
}
