using Domain.Contracts;
using E_CommerceCorevera.Api.Extensions;
using E_CommerceCorevera.Api.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens.Experimental;
using Presistence;
using Presistence.Data;
using Presistence.Repository;
using ServicesAbstraction;
using ServicesAbstraction.MappingProfiles;
using ServicesImplementation;
using Shared.ErrorModels;
using ValidationError = Shared.ErrorModels.ValidationError;

namespace E_CommerceCorevera.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

         
            builder.Services.AddCoreServices();
            builder.AddInfrastructureServices(builder.Configuration);
            builder.AddWebApiServices(builder.Configuration);

        
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

     
            using (var scope = app.Services.CreateScope())
            {
                try
                {
                    var ObjectOfDataSeeding = scope.ServiceProvider.GetRequiredService<IDataSeeding>();
                    ObjectOfDataSeeding.SeedData();
                    ObjectOfDataSeeding.SeedIdentityDataAsync();
                }
                catch (Exception ex)
                {
                 
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred during data seeding.");
                }
            }

         
            app.UseCors("MyPolicy");

      
            app.UseMiddleware<CustomExceptionHandlerMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // app.UseHttpsRedirection(); 
            app.UseStaticFiles();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}