using EmployeeTimeApi.Application.Accounts.Dtos;
using EmployeeTimeApi.Application.Accounts.Services;
using EmployeeTimeApi.Presentation.Controllers.Base;
using EmployeeTimeApi.Shared.Abstractions.Auth;
using EmployeeTimeApi.Shared.Abstractions.Contexts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace EmployeeTimeApi.Presentation.Controllers;

[Route("api/account")]
internal class AccountController : BaseController
{
    private const string AccessTokenCookie = "__access-token";

    private readonly IAccountService _accountService;
    private readonly CookieOptions _cookieOptions;
    private readonly IAccountContext _context;
    public AccountController(
        IAccountService accountService,
        CookieOptions cookieOptions,
        IAccountContext context)
    {
        _accountService = accountService;
        _cookieOptions = cookieOptions;
        _context = context;
    }

    [HttpPost("register")]
    [Authorize(Roles = Roles.Admin)]
    [SwaggerOperation("Register for application")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> RegisterAsync(
        [FromBody] RegisterDto dto,
        CancellationToken cancellationToken)
    {
        await _accountService.RegisterAsync(dto, cancellationToken);

        return NoContent();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    [SwaggerOperation("Log in to the application")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> LoginAsync(
        [FromBody] LoginDto dto,
        CancellationToken cancellationToken = default)
    {
        var jwt = await _accountService.GenerateTokenAsync(dto, cancellationToken);
        AddCookie(AccessTokenCookie, jwt);

        return NoContent();
    }

    [HttpDelete("sign-out")]
    [SwaggerOperation("Sign out")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> SignOut()
    {
        _accountService.Signout(_context.AccountId);

        DeleteCookie(AccessTokenCookie);

        return NoContent();
    }

    private void AddCookie(string key, string value) => Response.Cookies.Append(key, value, _cookieOptions);

    private void DeleteCookie(string key) => Response.Cookies.Delete(key, _cookieOptions);
}
