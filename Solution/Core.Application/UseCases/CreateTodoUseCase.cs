using AutoMapper;
using Core.Application.Dtos.Requests;
using Core.Application.Dtos.Responses;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.UseCases;
using Core.Domain.Entities;
using Infra.Notification.Contexts;

namespace Core.Application.UseCases
{
    public class CreateTodoUseCase : ICreateTodoUseCase
    {
        private readonly NotificationContext _notificationContext;
        private readonly IMapper _mapper;
        private readonly ITodoRepositoryAsync _todoRepositoryAsync;

        public CreateTodoUseCase(NotificationContext notificationContext,
            IMapper mapper,
            ITodoRepositoryAsync todoRepositoryAsync)
        {
            _notificationContext = notificationContext;
            _mapper = mapper;
            _todoRepositoryAsync = todoRepositoryAsync;
        }

        public async Task<CreateTodoUseCaseResponse?> RunAsync(CreateTodoUseCaseRequest request)
        {
            if (request.HasErrorNotification)
            {
                _notificationContext.AddErrorNotifications(request);

                return await Task.FromResult<CreateTodoUseCaseResponse?>(default);
            }

            var todo = _mapper.Map<Todo>(request);

            var todoRepositoryResponse = await _todoRepositoryAsync.CreateAsync(todo);

            return _mapper.Map<CreateTodoUseCaseResponse>(todoRepositoryResponse);
        }
    }
}