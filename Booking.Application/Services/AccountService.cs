using AutoMapper;
using Booking.Application.Dto;
using Booking.Application.Interfaces;
using Booking.Infrastructure.Data;

namespace Booking.Application.Services;

internal class AccountService(BookingDbContext dbContext,
                              IMapper mapper) : IAccountService
{
    private readonly BookingDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public async Task<UserDto> Get(int id)
    {
        var user = await _dbContext.Users
            .FindAsync(id) ?? throw new InvalidOperationException("User not found");

        return _mapper.Map<UserDto>(user);
    }

    public async Task Delete(int id)
    {
        var user = await _dbContext.Users
            .FindAsync(id) ?? throw new InvalidOperationException("User not found");

        _dbContext.Users.Remove(user);

        await _dbContext.SaveChangesAsync();
    }
}
