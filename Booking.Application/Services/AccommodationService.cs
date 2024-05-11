using AutoMapper;
using Booking.Application.Dto;
using Booking.Application.Interfaces;
using Booking.Core.Entities;
using Booking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Services;

internal class AccommodationService(BookingDbContext dbContext,
                            IMapper mapper) : IAccommodationService
{
    private readonly BookingDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<List<AccommodationViewDto>> GetList(int hotelId)
    {
        var accommodations = await _dbContext.Accommodations
            .Where(a => a.HotelId == hotelId)
            .ToListAsync();

        return _mapper.Map<List<AccommodationViewDto>>(accommodations);
    }

    public async Task<AccommodationDto> Get(int id)
    {
        var accommodation = await _dbContext.Accommodations
            .Where(h => h.Id == id)
            .FirstOrDefaultAsync();

        return _mapper.Map<AccommodationDto>(accommodation);
    }

    public async Task Add(AccommodationAddDto request, int numder)
    {
        for (int i = 0; i < numder; i++)
        {
            await _dbContext.Accommodations.AddAsync(_mapper.Map<Accommodation>(request));
        }

        await _dbContext.SaveChangesAsync();
    }

    public async Task Update(AccommodationUpdateDto request)
    {
        var accommodation = await _dbContext.Accommodations
            .FindAsync(request.Id) ?? throw new InvalidOperationException("Accommodation not found");

        accommodation.Name = request.Name;
        accommodation.Type = request.Type;
        accommodation.Description = request.Description;
        accommodation.Price = request.Price;

        await _dbContext.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var accommodation = await _dbContext.Accommodations.FindAsync(id) ?? throw new InvalidOperationException("Accommodation not found");

        _dbContext.Accommodations.Remove(accommodation);

        await _dbContext.SaveChangesAsync();
    }

}
