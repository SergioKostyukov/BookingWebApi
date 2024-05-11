using Booking.Application;
using Booking.Infrastructure.Data;
using Booking.WebApi.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Booking.WebApi.Validations;

namespace Booking;

public class Program
{
    public static void Main(string[] args)
    {
        #region Configure services
        var builder = WebApplication.CreateBuilder(args);

        // Add services related to storage using the configuration specified
        builder.Services.AddStorage(builder.Configuration);

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

        builder.Services.AddAuthorization(options =>
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

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddServices(builder.Configuration);

        builder.Services.AddFluentValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<IAssemblyMarker>();

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Please enter token.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });
        #endregion

        #region Configure pipeline
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
        #endregion
    }
}
