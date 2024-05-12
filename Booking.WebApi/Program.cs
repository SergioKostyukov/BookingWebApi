using Booking.Application;
using Booking.Identity;
using Booking.Infrastructure.Data;
using Booking.WebApi.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

namespace Booking;

public class Program
{
    public static void Main(string[] args)
    {
        #region Configure services
        var builder = WebApplication.CreateBuilder(args);

        // Add services related to storage using the configuration specified
        builder.Services.AddStorage(builder.Configuration);

        builder.Services.AddAuthenticationAndAuthorization(builder.Configuration);

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddIdentityServices();
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
