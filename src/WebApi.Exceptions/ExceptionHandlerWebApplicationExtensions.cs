using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WebApi.Exceptions;

public static class ExceptionHandlerWebApplicationExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app, IWebHostEnvironment environment)
    {
        if (environment.IsDevelopment())
        {
            app.UseMiddleware<ExceptionHandlerDeveloperMiddleware>();
        }
        else
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
