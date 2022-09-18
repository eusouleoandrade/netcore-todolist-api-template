using AutoMapper;
using Core.Application.Dtos.Queries;
using Core.Application.Dtos.Requests;
using Core.Application.Dtos.Wrappers;
using Core.Application.Interfaces.UseCases;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebApi.Controllers.Common;

namespace Presentation.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TodoController : BaseApiController
    {
        private readonly IGetAllTodoUseCase _getAllTodoUseCase;
        private readonly IMapper _mapper;
        private readonly ICreateTodoUseCase _createTodoUseCase;

        public TodoController(IGetAllTodoUseCase getAllTodoUseCase,
           IMapper mapper,
           ICreateTodoUseCase createTodoUseCase)
        {
            _getAllTodoUseCase = getAllTodoUseCase;
            _mapper = mapper;
            _createTodoUseCase = createTodoUseCase;
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
    }
}