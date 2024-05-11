using AutoMapper;
using Booking.Application.Dto;
using Booking.Application.Interfaces;
using Booking.Core.Entities;
using Booking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Booking.Application.Services;

internal class AccountService(BookingDbContext dbContext,
                              IMapper mapper) : IAccountService
{
    private readonly BookingDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;

    public UserIdentityDto GetUserIdentity(string userName, string password)
    {
        var userData = _dbContext.Users
            .Where(u => u.Name == userName)
            .FirstOrDefault();

        if (userData is not null)
        {
            bool passwordMatch = BCrypt.Net.BCrypt.Verify(password, userData.Password);

            if (passwordMatch)
            {
                return _mapper.Map<UserIdentityDto>(userData);
            }
        }

        throw new Exception("User not found or password incorrect.");
    }

    public async Task Create(UserRegisterDto request)
    {
        request.Password = BCrypt.Net.BCrypt.HashPassword(request.Password);
        request.Email = request.Email.ToLower();

        var user = _mapper.Map<User>(request);
        user.CreatedDate = DateTime.UtcNow;

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<UserDto> Get(int id)
    {
        var user = await _dbContext.Users
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();

        return _mapper.Map<UserDto>(user);
    }

    public async Task Delete(int id)
    {
        var user = await _dbContext.Users.FindAsync(id) ?? throw new InvalidOperationException("User not found");

        _dbContext.Users.Remove(user);

        await _dbContext.SaveChangesAsync();
    }
}
