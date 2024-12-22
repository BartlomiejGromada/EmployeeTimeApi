using AutoMapper;
using EmployeeTimeApi.Application.Employees.Dtos;
using EmployeeTimeApi.Application.Employees.Repositories;
using EmployeeTimeApi.Domain.Accounts.Exceptions;
using EmployeeTimeApi.Shared.Abstractions.Auth;
using EmployeeTimeApi.Shared.Abstractions.Contexts;

namespace EmployeeTimeApi.Application.Employees.Services;

internal sealed class EmployeeAccessValidationService : IEmployeeAccessValidationService
{
    private readonly IEmployeesRepository _employeesRepository;
    private readonly IAccountContext _accountContext;
    private readonly IMapper _mapper;

    public EmployeeAccessValidationService(
        IEmployeesRepository employeesRepository,
        IAccountContext accountContext,
        IMapper mapper)
    {
        _employeesRepository = employeesRepository;
        _accountContext = accountContext;
        _mapper = mapper;
    }

    public async Task<EmployeeDto?> ValidateAsync(
        int employeeId,
        CancellationToken? cancellationToken = null)
    {
        var employee = await _employeesRepository.GetByIdAsync(employeeId, cancellationToken);

        if (employee is null)
        {
            return null;
        }

        var requestingEmail = _accountContext.AccountEmail;
        var employeeEmail = employee.Email;
        var isUserAccount = _accountContext.IsInRole(Roles.User);

        if(isUserAccount && employeeEmail != _accountContext.AccountEmail)
        {
            throw new UnauthorizedAccessAccountException(requestingEmail, employeeEmail);
        }

        return _mapper.Map<EmployeeDto>(employee);
    }
}
