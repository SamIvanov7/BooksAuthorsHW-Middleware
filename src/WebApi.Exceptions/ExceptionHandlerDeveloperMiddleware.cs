using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace WebApi.Exceptions;

public class ExceptionHandlerDeveloperMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly IExceptionToResponseDeveloperMapper _exceptionToResponseDeveloperMapper;

    public ExceptionHandlerDeveloperMiddleware(
        ILogger<ExceptionHandlerMiddleware> logger,
        IExceptionToResponseDeveloperMapper exceptionToResponseDeveloperMapper)
    {
        _logger = logger;
        _exceptionToResponseDeveloperMapper = exceptionToResponseDeveloperMapper;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            await HandleException(context, ex);
        }
    }

    private async Task HandleException(HttpContext context, Exception exception)
    {
        var exceptionResponse = _exceptionToResponseDeveloperMapper.Map(exception);
        context.Response.StatusCode = (int)exceptionResponse.StatusCode;
        context.Response.ContentType = "application/json";
        await JsonSerializer.SerializeAsync(context.Response.Body, exceptionResponse.Data, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
    }
}
