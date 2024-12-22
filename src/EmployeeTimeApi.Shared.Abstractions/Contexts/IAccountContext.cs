namespace EmployeeTimeApi.Shared.Abstractions.Contexts;

public interface IAccountContext
{
    bool IsAuthenticated { get; }
    int AccountId { get; }
    string AccountEmail { get; }
    bool IsInRole(string role);
}
