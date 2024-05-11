using AutoMapper;
using Booking.Application.Dto;
using Booking.Application.Interfaces;
using Booking.Core.Entities;
using Booking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Services;

internal class HotelService(BookingDbContext dbContext,
                            IMapper mapper) : IHotelService
{
    private readonly BookingDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<List<HotelViewDto>> GetList()
    {
        var hotels = await _dbContext.Hotels.ToListAsync();

        return _mapper.Map<List<HotelViewDto>>(hotels);
    }

    public async Task<List<HotelViewDto>> GetListByUserId(int userId)
    {
        var hotels = await _dbContext.Hotels
            .Where(h => h.ManagerId == userId)
            .ToListAsync();

        return _mapper.Map<List<HotelViewDto>>(hotels);
    }

    public async Task<HotelDto> Get(int id)
    {
        var hotel = await _dbContext.Hotels
            .Where(h => h.Id == id)
            .FirstOrDefaultAsync();

        return _mapper.Map<HotelDto>(hotel);
    }

    public async Task Add(HotelAddDto request)
    {
        await _dbContext.Hotels.AddAsync(_mapper.Map<Hotel>(request));

        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(HotelUpdateDto request)
    {
        var hotel = await _dbContext.Hotels
            .FindAsync(request.Id) ?? throw new InvalidOperationException("Hotel not found");

        hotel.Name = request.Name;
        hotel.Type = request.Type;
        hotel.Description = request.Description;
        hotel.Country = request.Country;
        hotel.City = request.City;
        hotel.Address = request.Address;

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var hotel = await _dbContext.Hotels.FindAsync(id) ?? throw new InvalidOperationException("Hotel not found");

        _dbContext.Hotels.Remove(hotel);

        await _dbContext.SaveChangesAsync();
    }
}
