using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using EmployeeTimeApi.Presentation.Controllers.Base;
using EmployeeTimeApi.Application.Shared;

namespace EmployeeTimeApi.Modules.WorkTimeManagement.Api.Controllers;


[Route("employees/{id:int}/time-entries")]
internal class TimeEntriesController : BaseController
{
    //public TimeEntriesController(IDispatcher dispatcher) : base(dispatcher)
    //{
    //}


    //[HttpGet]
    //[SwaggerOperation("Get time entries for employee")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //public async Task<ActionResult<Paged<TimeEntriesDto>>> GetPagedAsync(
    //    [FromRoute] int id,
    //    [FromQuery] BrowseTimeEntriesQuery browseQuery,
    //    CancellationToken cancellationToken = default)
    //        => Ok(await dispatcher.QueryAsync(new TimeEntriesQuery(id, browseQuery), cancellationToken));

    //[HttpPost]
    //[SwaggerOperation("Add time entry for employee")]
    //[ProducesResponseType(StatusCodes.Status200OK)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<ActionResult> AddAsync(
    //    [FromRoute] int id,
    //    [FromBody] AddTimeEntryDto dto,
    //    CancellationToken cancellationToken = default)
    //{
    //    await dispatcher.SendAsync(new AddTimeEntryCommand(id, dto), cancellationToken);

    //    return Created("", null);
    //}

    //[HttpPut("{entryId:int}")]
    //[SwaggerOperation("Update time entry for employee")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<ActionResult> UpdateAsync(
    //   [FromRoute] int id,
    //   [FromRoute] int entryId,
    //   [FromBody] UpdateTimeEntryrDto dto,
    //   CancellationToken cancellationToken = default)
    //{
    //    await dispatcher.SendAsync(
    //        new UpdateTimeEntryCommand(id, entryId, dto), cancellationToken);

    //    return NoContent();
    //}

    //[HttpDelete("{entryId:int}")]
    //[SwaggerOperation("Delete time entry for employee")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //public async Task<ActionResult> DeleteAsync(
    //   [FromRoute] int id,
    //   [FromRoute] int entryId,
    //   CancellationToken cancellationToken = default)
    //{
    //    await dispatcher.SendAsync(
    //        new DeleteTimeEntryCommand(id, entryId), cancellationToken);

    //    return NoContent();
    //}
}
