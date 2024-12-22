using AutoMapper;
using EmployeeTimeApi.Application.Employees.Dtos;
using EmployeeTimeApi.Domain.Employees.Models;

namespace EmployeeTimeApi.Application.Employees.Mappings;

internal sealed class EmployeeProfile : Profile
{
    public EmployeeProfile()
    {
        CreateMap<Employee, EmployeeDto>();
        CreateMap<AddEmployeeDto, Employee>();
        CreateMap<UpdateEmployeeDto, Employee>();
    }
}
