using EmployeeTimeApi.Shared.Abstractions.Exceptions;

namespace EmployeeTimeApi.Shared.Exceptions;

internal interface IExceptionCompositionRoot
{
    ExceptionResponse Map(Exception exception);
}
