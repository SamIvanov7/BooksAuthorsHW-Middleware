using System.Net;
using Library.Core.Exceptions;
using WebApi.Exceptions;

namespace Library.Infrastructure.Exceptions;

public class ExceptionToResponseDeveloperMapper : IExceptionToResponseDeveloperMapper
{
    public ExceptionResponse Map(Exception exception)
    {
        return exception switch
        {
            NotFoundException => new ExceptionResponse(
                StatusCode: HttpStatusCode.NotFound,
                Data: new
                {
                    exception.Message,
                    exception.StackTrace
                }),
            AlreadyExistsException ex => new ExceptionResponse(
                StatusCode: HttpStatusCode.Conflict,
                Data: new
                {
                    ex.Details,
                    ex.Message,
                    ex.StackTrace
                }),
            ValidationException ex => new ExceptionResponse(
                StatusCode: HttpStatusCode.BadRequest,
                Data: new
                {
                    ex.Message,
                    Errors = ex.Failures
                        .Select(e => new
                        {
                            e.PropertyName,
                            e.ErrorMessage
                        })
                        .ToList(),
                    exception.StackTrace
                }),
            RuleValidationException ex => new ExceptionResponse(
                StatusCode: HttpStatusCode.BadRequest,
                Data: new
                {
                    ex.Message,
                    Errors = ex.Failures,
                    exception.StackTrace
                }),
            _ => new ExceptionResponse(
                StatusCode: HttpStatusCode.InternalServerError,
                Data: new
                {
                    exception.Message,
                    exception.StackTrace
                })
        };
    }
}
