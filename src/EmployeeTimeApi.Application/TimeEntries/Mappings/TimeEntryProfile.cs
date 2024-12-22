using AutoMapper;
using EmployeeTimeApi.Application.TimeEntries.Dtos;
using EmployeeTimeApi.Domain.TimeEntries.Models;

namespace EmployeeTimeApi.Application.Employees.Mappings;

internal sealed class TimeEntryProfile : Profile
{
    public TimeEntryProfile()
    {
        CreateMap<TimeEntry, TimeEntryDto>();
        CreateMap<AddTimeEntryDto, TimeEntry>();
        CreateMap<UpdateTimeEntryDto, TimeEntry>();
    }
}
