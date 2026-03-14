using Domain.Contracts;
using E_CommerceCorevera.Api.Factories;
using Microsoft.AspNetCore.Mvc;
using Presistence;
using Presistence.Repository;
using ServicesAbstraction;
using ServicesAbstraction.MappingProfiles;
using ServicesImplementation;

namespace E_CommerceCorevera.Api.Extensions
{
    public static class CoreServicesExtensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
      
            services.AddAutoMapper(cfg => { }, typeof(ProductProfile).Assembly);
            services.AddScoped<IServiceManager, ServiceManager>();
         
            return services;
        }


    }
}
