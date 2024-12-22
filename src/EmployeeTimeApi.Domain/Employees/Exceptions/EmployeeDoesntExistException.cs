using EmployeeTimeApi.Shared.Abstractions.Exceptions;

namespace EmployeeTimeApi.Domain.Employees.Exceptions;

internal class EmployeeDoesntExistException : BusinessRuleException
{
    public EmployeeDoesntExistException(int id) : base($"Employee with id: {id} doesn't exist.")
    {
    }
}
