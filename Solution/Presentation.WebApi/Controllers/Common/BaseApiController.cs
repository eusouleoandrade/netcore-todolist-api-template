using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebApi.Controllers.Common
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public abstract class BaseApiController : ControllerBase
    {
    }
}
