using AutoMapper;
using Core.Application.Dtos.Queries;
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

        public TodoController(IGetAllTodoUseCase getAllTodoUseCase,
           IMapper mapper)
        {
            _getAllTodoUseCase = getAllTodoUseCase;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Response<List<TodoQuery>>>> Get()
        {
            var useCaseResponse = await _getAllTodoUseCase.RunAsync();

            return Ok(new Response<IReadOnlyList<TodoQuery>>(useCaseResponse, true));
        }
    }
}