using Core.Application.Dtos.Requests;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.UseCases;
using Core.Application.Resources;
using Core.Domain.Entities;
using Infra.Notification.Contexts;
using Infra.Notification.Extensions;

namespace Core.Application.UseCases
{
    public class UpdateTodoUseCase : IUpdateTodoUseCase
    {
        private readonly IGenericRepositoryAsync<Todo, int> _genericRepositoryAsync;
        private readonly NotificationContext _notificationContext;
        private readonly IGetTodoUseCase _getTodoUseCase;

        public UpdateTodoUseCase(IGenericRepositoryAsync<Todo, int> genericRepositoryAsync,
           NotificationContext notificationContext,
           IGetTodoUseCase getTodoUseCase)
        {
            _genericRepositoryAsync = genericRepositoryAsync;
            _notificationContext = notificationContext;
            _getTodoUseCase = getTodoUseCase;
        }

        public async Task RunAsync(UpdateTodoUseCaseRequest request)
        {
            if (request.HasErrorNotification)
            {
                _notificationContext.AddErrorNotifications(request);
                return;
            }

            var getTodoUseCaseResponse = await _getTodoUseCase.RunAsync(request.Id);

            if (_notificationContext.HasErrorNotification || getTodoUseCaseResponse is null)
                return;

            var todo = new Todo(getTodoUseCaseResponse.Id, request.Title, request.Done);

            var updated = await _genericRepositoryAsync.UpdateAsync(todo);

            if (!updated)
                _notificationContext.AddErrorNotification(Msg.FAILED_TO_UPDATE_X0_COD, Msg.FAILED_TO_UPDATE_X0_TXT.ToFormat("Todo"));
        }
    }
}