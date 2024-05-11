using Booking.Application.Dto;
using Booking.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Booking.Core.Enums;

namespace Booking.Application.Services;

internal class IdentityService : IIdentityService
{
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
