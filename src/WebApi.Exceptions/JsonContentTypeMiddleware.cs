using Microsoft.AspNetCore.Http;

namespace WebApi.Exceptions;

public class JsonContentTypeMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        await _next(context);

        if (context.Response.HasStarted)
        {
            return;
        }

        if (!context.Response.Headers.ContainsKey("Content-Type"))
        {
            context.Response.Headers["Content-Type"] = "application/json";
        }
    }
}
