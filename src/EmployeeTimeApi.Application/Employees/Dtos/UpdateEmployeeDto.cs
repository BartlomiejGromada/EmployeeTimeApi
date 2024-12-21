namespace EmployeeTimeApi.Application.Employees.Dtos;

internal record UpdateEmployeeDto(
    string FirstName,
    string LastName,
    string Email);
