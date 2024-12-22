using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeTimeApi.Presentation.Controllers.Base;

[ApiController]
[Authorize]
[ProducesDefaultContentType]
public abstract class BaseController : ControllerBase
{

    protected BaseController()
    {
    }

    protected ActionResult<T> OkOrNotFound<T>(T? model)
    {
        if (model is not null)
        {
            return Ok(model);
        }

        return NotFound();
    }
}
