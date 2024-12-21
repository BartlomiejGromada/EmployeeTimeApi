namespace EmployeeTimeApi.Shared.Abstractions.Exceptions;

public abstract class BusinessRuleException : Exception
{
    protected BusinessRuleException(string message) : base(message)
    {
    }
}