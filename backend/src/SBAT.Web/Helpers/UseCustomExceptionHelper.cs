using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

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
            if (exception is not null)
            {
                httpContext.Response.ContentType = "application/problem+json";
                var title = includeDetails ? exception.Message : "An error occured";
                var detail = includeDetails ? exception.ToString() : null;
                var problem = new ProblemDetails
                {
                    Status = 500,
                    Title = title,
                    Detail = detail
                };

                var traceId = Activity.Current?.Id ?? httpContext?.TraceIdentifier;
                if (traceId is not null)
                {
                    problem.Extensions["tradeId"] = traceId;
                }

                var stream = httpContext!.Response.Body;
                await JsonSerializer.SerializeAsync(stream, problem);
            }
        }
    }
}