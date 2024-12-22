using AutoMapper;
using EmployeeTimeApi.Application.Accounts.Dtos;
using EmployeeTimeApi.Domain.Accounts.Models;

namespace EmployeeTimeApi.Application.Employees.Mappings;

internal sealed class AccountProfile : Profile
{
    public AccountProfile()
    {
        CreateMap<LoginDto, Account>();
        CreateMap<RegisterDto, Account>();
    }
}
