namespace EmployeeTimeApi.Application.Accounts.Dtos;

internal record RegisterDto(
    string Email,
    string Password,
    string RepeatPassword,
    bool IsAdmin);