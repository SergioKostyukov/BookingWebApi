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

        services.AddAuthorization(options =>
        {
            options.AddPolicy(IdentityConstants.AdminUserPolicyName,
                policy => policy.RequireClaim(ClaimTypes.Role, IdentityConstants.AdminUserClaimName));

            options.AddPolicy(IdentityConstants.ManagerUserPolicyName,
                policy => policy.RequireClaim(ClaimTypes.Role, IdentityConstants.ManagerUserClaimName));

            options.AddPolicy(IdentityConstants.ClientUserPolicyName,
                policy => policy.RequireClaim(ClaimTypes.Role, IdentityConstants.ClientUserClaimName));

            options.AddPolicy(IdentityConstants.ClientOrManagerUserPolicyName,
                policy => policy.RequireClaim(ClaimTypes.Role, IdentityConstants.ClientUserPolicyName, IdentityConstants.ManagerUserPolicyName));
        });

        return services;
    }

    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(UserProfile));

        services.AddScoped<IIdentityService, IdentityService>();

        return services;
    }
}
