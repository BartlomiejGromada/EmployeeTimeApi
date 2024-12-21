namespace EmployeeTimeApi.Application.Employees.Dtos;

internal record AddEmployeeDto(
    string FirstName,
    string LastName,
    string Email);
