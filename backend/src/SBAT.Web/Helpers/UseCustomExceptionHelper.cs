using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using SBAT.Web.Models.Common;
using SBAT.Web.Models.Response;

namespace SBAT.Web.Helpers
{
    public static class UseCustomExceptionHelper
    {
        public static void UseCustomException(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.Use(WriteDevelopmentResponse);
            }
            else
            {
                app.Use(WriteProductionResponse);
            }
        }

        private static Task WriteDevelopmentResponse(HttpContext httpContext, Func<Task> next)
            => WriteResponse(httpContext, includeDetails: true);

        private static Task WriteProductionResponse(HttpContext httpContext, Func<Task> next)
            => WriteResponse(httpContext, includeDetails: false);

        private static async Task WriteResponse(HttpContext httpContext, bool includeDetails)
        {
            var exceptionDetails = httpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = exceptionDetails?.Error;
            var jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            if (exception is not null && exception.GetType() == typeof(ValidationException))
            {
                httpContext.Response.ContentType = "application/json";
                var title = includeDetails ? exception.Message : "A Validation Error Occurred";
                var sbatResponse = Response<EmptyResponse>.CreateResponse(new EmptyResponse(), new List<string> { title }, ResponseCode.BadRequest);
                var stream = httpContext!.Response.Body;
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                await JsonSerializer.SerializeAsync(stream, sbatResponse, jsonSerializerOptions);
            }
            else if (exception is not null)
            {
                httpContext.Response.ContentType = "application/json";
                var title = includeDetails ? exception.Message : "An error occured";
                var errors = new List<string>() { title };
                if (includeDetails) 
                {
                    errors.Add(exception.ToString());
                }
                
                var sbatResponse = Response<EmptyResponse>.CreateResponse(new EmptyResponse(), new List<string> { title, }, ResponseCode.ServerError);
                var stream = httpContext!.Response.Body;
                await JsonSerializer.SerializeAsync(stream, sbatResponse);
            }
        }
    }
}