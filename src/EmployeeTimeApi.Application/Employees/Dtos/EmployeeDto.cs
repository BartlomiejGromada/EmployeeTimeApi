namespace EmployeeTimeApi.Application.Employees.Dtos;

internal record EmployeeDto(
    int Id,
    string FirstName,
    string LastName,
    string Email);