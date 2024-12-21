using Microsoft.AspNetCore.Mvc;
using EmployeeTimeApi.Presentation.Controllers.Base;
using Swashbuckle.AspNetCore.Annotations;
using EmployeeTimeApi.Application.Employees.Dtos;
using EmployeeTimeApi.Application.Employees.ApiObjects;
using EmployeeTimeApi.Application.Employees.Services;
using EmployeeTimeApi.Application.Shared;

namespace EmployeeTimeApi.Presentation.Controllers;


[Route("employees")]
internal class EmployeesController : BaseController
{
    private readonly IEmployeesService _services;
    public EmployeesController(IEmployeesService services)
    {
        _services = services;
    }

    [HttpGet]
    [SwaggerOperation("Get employees")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Paged<EmployeeDto>>> GetPagedAsync(
        [FromQuery] BrowseEmployeesQuery browseQuery,
        CancellationToken cancellationToken = default)
            => Ok(await _services.GetPagedAsync(browseQuery, cancellationToken));


    [HttpGet("{id:int}")]
    [ActionName("GetEmployeeDetails")]
    [SwaggerOperation("Get employee details")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EmployeeDto>> GetByIdAsync(
        [FromRoute] int id,
        CancellationToken cancellationToken = default)
            => OkOrNotFound(await _services.GetByIdAsync(id, cancellationToken));

    [HttpPost]
    [SwaggerOperation("Add employee")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AddAsync(
        [FromBody] AddEmployeeDto dto,
        CancellationToken cancellationToken = default)
    {
        var addedEmployeeId = await _services.AddAsync(dto, cancellationToken);

        return CreatedAtAction("GetEmployeeDetails", new { id = addedEmployeeId }, null);
    }

    [HttpPut("{id:int}")]
    [SwaggerOperation("Update employee")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<EmployeeDto>> UpdateAsync(
        [FromRoute] int id,
        [FromBody] UpdateEmployeeDto dto,
        CancellationToken cancellationToken = default)
    {
        await _services.UpdateAsync(id, dto, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [SwaggerOperation("Delete employee")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> DeleteAsync(
        [FromRoute] int id,
        CancellationToken cancellationToken = default)
    {
        await _services.DeleteByIdAsync(id, cancellationToken);

        return NoContent();
    }
}
