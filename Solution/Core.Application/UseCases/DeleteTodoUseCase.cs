using AutoMapper;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.UseCases;
using Core.Application.Resources;
using Core.Domain.Entities;
using Infra.Notification.Contexts;
using Infra.Notification.Extensions;

namespace Core.Application.UseCases
{
    public class DeleteTodoUseCase : IDeleteTodoUseCase
    {
        private readonly NotificationContext _notificationContext;
        private readonly IGenericRepositoryAsync<Todo, int> _genericRepositoryAsync;
        private readonly IGetTodoUseCase _getTodoUseCase;
        private readonly IMapper _mapper;

        public DeleteTodoUseCase(IGenericRepositoryAsync<Todo, int> genericRepositoryAsync,
            NotificationContext notificationContext,
            IGetTodoUseCase getTodoUseCase,
            IMapper mapper)
        {
            _genericRepositoryAsync = genericRepositoryAsync;
            _notificationContext = notificationContext;
            _getTodoUseCase = getTodoUseCase;
            _mapper = mapper;
        }

        public async Task RunAsync(int id)
        {
            var getTodoUseCaseResponse = await _getTodoUseCase.RunAsync(id);

            if (_notificationContext.HasErrorNotification)
                return;

            var todo = _mapper.Map<Todo>(getTodoUseCaseResponse);

            var removed = await _genericRepositoryAsync.DeleteAsync(todo);

            if (!removed)
                _notificationContext.AddErrorNotification(Msg.FAILED_TO_REMOVE_X0_COD,
                    Msg.FAILED_TO_REMOVE_X0_TXT.ToFormat("Todo"));
        }
    }
}