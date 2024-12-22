namespace EmployeeTimeApi.Shared.Abstractions.Contexts;

public interface IAccountContext
{
    bool IsAuthenticated { get; }
    int AccountId { get; }
}
