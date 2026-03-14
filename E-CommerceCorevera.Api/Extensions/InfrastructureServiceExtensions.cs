using Domain.Contracts;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Presistence.Data;
using Presistence.Identity;
using Presistence.Repository;
using StackExchange.Redis;

namespace E_CommerceCorevera.Api.Extensions
{
    public static class InfrastructureServiceExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            builder.Services.AddDbContext<CoreveraDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
            builder.Services.AddDbContext<CoreveraIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("IdentityConnection"));
            });

            builder.Services.AddScoped<IBasketRepository, BasketRepository>();
            builder.Services.AddScoped<IDataSeeding, DataSeeding>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var redisConnectionString = configuration.GetConnectionString("RedisConnection");
                if (string.IsNullOrEmpty(redisConnectionString))
                {
                    throw new InvalidOperationException("Redis connection string is not configured.");
                }
                return ConnectionMultiplexer.Connect(redisConnectionString);
            });


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                { 
            
                    options.Password.RequireDigit = true;
                    options.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.User.RequireUniqueEmail = true;
                      }).AddEntityFrameworkStores<CoreveraIdentityDbContext>()
                      .AddDefaultTokenProviders(); 

            return builder.Services;
        }
    }
}
