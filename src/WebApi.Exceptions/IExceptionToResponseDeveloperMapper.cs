namespace WebApi.Exceptions;

public interface IExceptionToResponseDeveloperMapper
{
    ExceptionResponse Map(Exception exception);
}
