using EmployeeTimeApi.Application.Shared;
using EmployeeTimeApi.Application.TimeEntries.Dtos;

namespace EmployeeTimeApi.Application.TimeEntries.ApiObjects;

internal class BrowseTimeEntriesQuery : PagedQuery<TimeEntryDto>
{
}

