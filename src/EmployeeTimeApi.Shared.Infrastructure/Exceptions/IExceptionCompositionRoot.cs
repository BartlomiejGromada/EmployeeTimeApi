using EmployeeTimeApi.Shared.Abstractions.Exceptions;

namespace EmployeeTimeApi.Shared.Infrastructure.Exceptions;

public interface IExceptionCompositionRoot
{
    ExceptionResponse Map(Exception exception);
}