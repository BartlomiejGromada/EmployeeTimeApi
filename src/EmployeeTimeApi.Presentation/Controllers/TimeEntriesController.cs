using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using EmployeeTimeApi.Presentation.Controllers.Base;
using EmployeeTimeApi.Application.Shared;
using EmployeeTimeApi.Application.TimeEntries.Services;
using EmployeeTimeApi.Application.TimeEntries.Dtos;
using EmployeeTimeApi.Application.TimeEntries.ApiObjects;
using Microsoft.AspNetCore.Authorization;

namespace EmployeeTimeApi.Modules.WorkTimeManagement.Api.Controllers;


[Route("employees/{id:int}/time-entries")]
internal class TimeEntriesController : BaseController
{
    private readonly ITimeEntriesService _services;
    public TimeEntriesController(ITimeEntriesService services)
    {
        _services = services;
    }


    [HttpGet]
    [SwaggerOperation("Get time entries for employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Paged<TimeEntryDto>>> GetPagedAsync(
        [FromRoute] int id,
        [FromQuery] BrowseTimeEntriesQuery browseQuery,
        CancellationToken cancellationToken = default)
            => Ok(await _services.GetPagedAsync(id, browseQuery, cancellationToken));

    [HttpPost]
    [SwaggerOperation("Add time entry for employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AddAsync(
        [FromRoute] int id,
        [FromBody] AddTimeEntryDto dto,
        CancellationToken cancellationToken = default)
    {
        await _services.AddAsync(id, dto, cancellationToken);

        return Created("", null);
    }

    [HttpPut("{entryId:int}")]
    [SwaggerOperation("Update time entry for employee")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> UpdateAsync(
       [FromRoute] int id,
       [FromRoute] int entryId,
       [FromBody] UpdateTimeEntryDto dto,
       CancellationToken cancellationToken = default)
    {
        await _services.UpdateAsync(id, entryId, dto, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{entryId:int}")]
    [SwaggerOperation("Delete time entry for employee")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteAsync(
       [FromRoute] int id,
       [FromRoute] int entryId,
       CancellationToken cancellationToken = default)
    {
        await _services.DeleteByIdAsync(id, entryId, cancellationToken);

        return NoContent();
    }
}
