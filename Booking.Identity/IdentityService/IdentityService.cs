using Booking.Identity.Dto;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Booking.Core.Enums;
using AutoMapper;
using Booking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Booking.Core.Entities;

namespace Booking.Identity.Services;

internal class IdentityService(BookingDbContext dbContext,
                              IMapper mapper) : IIdentityService
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

        bool isNameUnique = await _dbContext.Users.AllAsync(u => u.Name != request.Name);
        if (!isNameUnique)
        {
            throw new Exception("User with this name already exists.");
        }

        var user = _mapper.Map<User>(request);
        user.CreatedDate = DateTime.UtcNow;

        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public string GenerateToken(UserIdentityDto request)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Kjf2gj98Sd98SdHrCAsdUwJkNcnKdF8SECRET"));

        var claims = new List<Claim>
        {
            new("UserId", request.Id.ToString()),
            new(ClaimTypes.Name, request.Name),
            new(ClaimTypes.Email, request.Email),
            new(ClaimTypes.Role, Enum.GetName(typeof(UserRole), request.Role))
        };

        var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var tokenOptions = new JwtSecurityToken(
            issuer: "https://localhost:7224",
            audience: "https://localhost:7224",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: signinCredentials
        );
        var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return tokenString;
    }
}
