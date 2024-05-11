using AutoMapper;
using Booking.Application.Dto;
using Booking.Application.Interfaces;
using Booking.Core.Entities;
using Booking.Infrastructure.Data;

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
}
