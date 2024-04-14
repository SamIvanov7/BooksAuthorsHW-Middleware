using System.Net;

namespace WebApi.Exceptions;

// todo: think do we need to create for each exception own type like with problem details
public record ExceptionResponse(HttpStatusCode StatusCode, object Data);
