using Domain.Exceptions;
using Shared.ErrorModels;
using System.Net;
using System.Text.Json;

namespace E_CommerceCorevera.Api
{
    public class CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger _logger = logger;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
                if (httpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    var Response = new ErrorToReturn()
                    {
                        StatusCode =StatusCodes.Status404NotFound,
                        ErrorMessage=$"End Point {httpContext.Request.Path} Is Not Found"
                    };
                   await httpContext.Response.WriteAsJsonAsync(Response);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                //set Status Code 
                httpContext.Response.StatusCode = ex switch
                {
                    NotFoundException =>StatusCodes.Status404NotFound,
                    UnAuthorizedException => StatusCodes.Status401Unauthorized,
                   
                    (_) => StatusCodes.Status500InternalServerError
                };
              // httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;


                //Content Type
                httpContext.Response.ContentType = "application/json";
                //Response object
                var Response = new ErrorToReturn()
                {
                    StatusCode = httpContext.Response.StatusCode,
                    ErrorMessage = ex.Message
                };

                //return object As Json
                await httpContext.Response.WriteAsJsonAsync(Response);
            }
        }
    }
}
