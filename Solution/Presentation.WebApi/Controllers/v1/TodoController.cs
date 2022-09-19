using AutoMapper;
using Core.Application.Dtos.Queries;
using Core.Application.Dtos.Requests;
using Core.Application.Dtos.Wrappers;
using Core.Application.Interfaces.UseCases;
using Infra.Notification.Contexts;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApi.Controllers.Common;

namespace Presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TodoController : BaseApiController
    {
        private readonly IGetAllTodoUseCase _getAllTodoUseCase;
        private readonly ICreateTodoUseCase _createTodoUseCase;
        private readonly IDeleteTodoUseCase _deleteTodoUseCase;
        private readonly IGetTodoUseCase _getTodoUseCase;
        //private readonly IUpdateTodoUseCase _updateTodoUseCase;
        //private readonly ISetDoneTodoUseCase _setDoneTodoUseCase;
        private readonly IMapper _mapper;
        private readonly NotificationContext _notificationContext;

        public TodoController(IGetAllTodoUseCase getAllTodoUseCase,
            ICreateTodoUseCase createTodoUseCase,
            IMapper mapper,
            NotificationContext notificationContext,
            IDeleteTodoUseCase deleteTodoUseCase,
            //IUpdateTodoUseCase updateTodoUseCase,
            //ISetDoneTodoUseCase setDoneTodoUseCase,
            IGetTodoUseCase getTodoUseCase)
        {
            _getAllTodoUseCase = getAllTodoUseCase;
            _createTodoUseCase = createTodoUseCase;
            _mapper = mapper;
            _notificationContext = notificationContext;
            _deleteTodoUseCase = deleteTodoUseCase;
            _getTodoUseCase = getTodoUseCase;
            //_updateTodoUseCase = updateTodoUseCase;
            //_setDoneTodoUseCase = setDoneTodoUseCase;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<TodoQuery>>>> Get()
        {
            var useCaseResponse = await _getAllTodoUseCase.RunAsync();

            return Ok(new Response<IReadOnlyList<TodoQuery>>(useCaseResponse, true));
        }

        [HttpPost]
        public async Task<ActionResult<Response<CreateTodoQuery>>> Post([FromBody] CreateTodoRequest request)
        {
            var useCaseResponse = await _createTodoUseCase.RunAsync(_mapper.Map<CreateTodoUseCaseRequest>(request));

            if (useCaseResponse == null)
                return BadRequest();

            var response = _mapper.Map<CreateTodoQuery>(useCaseResponse);

            return Created($"/api/v1/todo/{response.Id}", new Response<CreateTodoQuery>(data: response, succeeded: true));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response>> Delete(int id)
        {
            await _deleteTodoUseCase.RunAsync(id);

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<GetTodoQuery>>> Get(int id)
        {
            var useCaseResponse = await _getTodoUseCase.RunAsync(id);

            var response = _mapper.Map<GetTodoQuery>(useCaseResponse);

            return Ok(new Response<GetTodoQuery>(succeeded: true, data: response));
        }
    }
}