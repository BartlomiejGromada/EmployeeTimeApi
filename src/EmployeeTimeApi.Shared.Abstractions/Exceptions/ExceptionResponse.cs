using System.Net;

namespace EmployeeTimeApi.Shared.Abstractions.Exceptions;

public record ExceptionResponse(object Response, HttpStatusCode StatusCode);