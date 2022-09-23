using Core.Application.Dtos.Queries;
using Core.Application.Dtos.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApi.Controllers.Common;

namespace Presentation.WebApi.Controllers.v2
{
    [ApiVersion("2.0")]
    public class TodoController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<Response<List<TodoQuery>>>> Get()
        {
            IReadOnlyList<TodoQuery> todoQuery = new List<TodoQuery>
            {
                new TodoQuery(1, "Ir ao mercado", false),
                new TodoQuery(2, "Ir ao médico", false),
                new TodoQuery(3, "Fazer invesitimentos", false)
            };

            return Ok(await Task.FromResult(new Response<IReadOnlyList<TodoQuery>>(todoQuery, true)));
        }
    }
}