using System.Net;
using Library.Core.Exceptions;
using WebApi.Exceptions;

namespace Library.Infrastructure.Exceptions;

public class ExceptionToResponseMapper : IExceptionToResponseMapper
{
    public ExceptionResponse Map(Exception exception)
    {
        return exception switch
        {
            NotFoundException ex => new ExceptionResponse(
                StatusCode: HttpStatusCode.NotFound,
                Data: new
                {
                    ex.Message
                }),
            AlreadyExistsException ex => new ExceptionResponse(
                StatusCode: HttpStatusCode.Conflict,
                Data: new
                {
                    ex.Details,
                    ex.Message
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
                        .ToList()
                }),
            RuleValidationException ex => new ExceptionResponse(
                StatusCode: HttpStatusCode.BadRequest,
                Data: new
                {
                    ex.Message,
                    Errors = ex.Failures
                }),
            // todo: think where to put this const
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
