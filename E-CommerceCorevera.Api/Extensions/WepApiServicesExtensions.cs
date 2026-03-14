using E_CommerceCorevera.Api.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;

namespace E_CommerceCorevera.Api.Extensions
{
    public static class WepApiServicesExtensions
    {

        public static void AddWebApiServices(this WebApplicationBuilder builder ,IConfiguration _configuration)
        {
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<ApiBehaviorOptions>((Options) =>
             {
                 Options.InvalidModelStateResponseFactory = ApiResponseFactory.CustomValidationErrorResponce;
             });



            _ = builder.Services.AddAuthentication(config =>
                {
                    config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = _configuration["JWTOptions:Issuer"],

                        ValidateAudience = true,
                        ValidAudience = _configuration["JWTOptions:Audience"],

                        ValidateLifetime = true,

                        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JWTOptions:SecretKey"])),
                        ValidateIssuerSigningKey = true


                    };

                });

            
          
        }
        




    }
}
