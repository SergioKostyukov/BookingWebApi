using AutoMapper;
using Booking.Application.Dto;
using Booking.Application.Interfaces;
using Booking.Core.Entities;
using Booking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Services;

internal class OrderService(BookingDbContext dbContext,
                            IMapper mapper) : IOrderService
{
    private readonly BookingDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<List<OrderViewDto>> GetList(int userId)
    {
        var orders = await _dbContext.Orders
            .Where(x => x.CustomerId == userId)
            .ToListAsync();

        return _mapper.Map<List<OrderViewDto>>(orders);
    }

    public async Task<List<OrderViewDto>> GetListOrdersToMe(int userId)
    {
        var orders = await (
            from hotel in _dbContext.Hotels
            join order in _dbContext.Orders on hotel.Id equals order.HotelId
            where hotel.ManagerId == userId
            select order
        ).ToListAsync();

        return _mapper.Map<List<OrderViewDto>>(orders);
    }

    public async Task<(OrderDto, List<OrderListDto>)> Get(int id)
    {
        var orderData = await (
            from order in _dbContext.Orders
            join orderItem in _dbContext.OrdersLists on order.Id equals orderItem.OrderId into orderItemsGroup
            where order.Id == id
            select new
            {
                Order = order,
                OrderItems = orderItemsGroup.ToList()
            }
        ).FirstOrDefaultAsync() ?? throw new InvalidOperationException("Order not found");

        var orderDto = _mapper.Map<OrderDto>(orderData.Order);
        var orderListDtos = _mapper.Map<List<OrderListDto>>(orderData.OrderItems);

        return (orderDto, orderListDtos);
    }

    public async Task Add(OrderAddDto request)
    {
        var hotelId = await _dbContext.Accommodations
            .Where(a => a.Id == request.AccommodationId)
            .Select(a => a.HotelId)
            .FirstAsync();

        var order = await _dbContext.Orders
            .Where(x => x.CustomerId == request.CustomerId && x.HotelId == hotelId)
            .FirstOrDefaultAsync();

        if (order is null)
        {
            var orderData = new OrderDto
            {
                HotelId = hotelId,
                CustomerId = request.CustomerId,
                CreateDate = DateTime.UtcNow
            };

            await _dbContext.Orders.AddAsync(_mapper.Map<Order>(orderData));

            await _dbContext.SaveChangesAsync();

            order = await _dbContext.Orders
            .Where(x => x.CustomerId == request.CustomerId && x.HotelId == hotelId)
            .FirstAsync();
        }

        _dbContext.OrdersLists.Add(new OrderList
        {
            AccommodationId = request.AccommodationId,
            OrderId = order.Id
        });

        var accommodationPrice = await _dbContext.Accommodations
                .Where(a => a.Id == request.AccommodationId)
                .Select(a => a.Price)
                .FirstAsync();

        order.CheckAmount += accommodationPrice;

        _dbContext.Orders.Update(order);

        await _dbContext.SaveChangesAsync();
    }

    public async Task Confirm(OrderConfirmDto request)
    {
        var order = await _dbContext.Orders
            .Where(x => x.Id == request.Id)
            .FirstAsync();

        order.StartTime = request.StartTime;
        order.EndTime = request.EndTime;
        order.PaymentType = request.PaymentType;
        order.Comment = request.Comment;

        _dbContext.Orders.Update(order);

        await _dbContext.SaveChangesAsync();
    }

    public async Task ConfirmPayment(int id)
    {
        var order = await _dbContext.Orders
            .Where(x => x.Id == id)
            .FirstAsync();

        order.PaymentStatus = true;

        _dbContext.Orders.Update(order);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAccommodationFromOrder(int orderId, int accommodationId)
    {
        await _dbContext.OrdersLists
                .Where(o => o.OrderId == orderId && o.AccommodationId == accommodationId)
                .ExecuteDeleteAsync();

        var accommodationPrice = await _dbContext.Accommodations
                .Where(a => a.Id == accommodationId)
                .Select(a => a.Price)
                .FirstAsync();

        var order = await _dbContext.Orders
            .Where(o => o.Id == orderId)
            .FirstOrDefaultAsync();

        order.CheckAmount -= accommodationPrice;

        if (order.CheckAmount <= 0)
        {
            _dbContext.Remove(order);
        }
        else
        {
            _dbContext.Orders.Update(order);
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var order = await _dbContext.Orders
            .FindAsync(id) ?? throw new InvalidOperationException("Order not found");

        await _dbContext.OrdersLists
                .Where(o => o.OrderId == id)
                .ExecuteDeleteAsync();

        _dbContext.OrdersLists.RemoveRange();

        _dbContext.Orders.Remove(order);

        await _dbContext.SaveChangesAsync();
    }
}
