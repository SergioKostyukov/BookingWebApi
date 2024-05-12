using Booking.Identity.MappingProfiles;
using Booking.Identity.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using Booking.WebApi.Identity.Constants;

namespace Booking.Identity;

public static class RegistrationExtensions
{
    public static IServiceCollection AddAuthenticationAndAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        // Configure JWT authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });

        // Configure authorization policies
        services.AddAuthorizationBuilder()
            .AddPolicy(IdentityConstants.AdminUserPolicyName,
                policy => policy.RequireClaim(ClaimTypes.Role, IdentityConstants.AdminUserClaimName))
            .AddPolicy(IdentityConstants.ManagerUserPolicyName,
                policy => policy.RequireClaim(ClaimTypes.Role, IdentityConstants.ManagerUserClaimName))
            .AddPolicy(IdentityConstants.ClientUserPolicyName, policy =>
                policy.RequireClaim(ClaimTypes.Role, IdentityConstants.ClientUserClaimName))
            .AddPolicy(IdentityConstants.ClientOrManagerUserPolicyName,
                policy => policy.RequireClaim(ClaimTypes.Role, IdentityConstants.ClientUserPolicyName, IdentityConstants.ManagerUserPolicyName));

        return services;
    }

    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserProfile));

        services.AddScoped<IIdentityService, IdentityService>();

        return services;
    }
}
