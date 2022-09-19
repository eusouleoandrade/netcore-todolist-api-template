using AutoMapper;
using Core.Application.Dtos.Responses;
using Core.Application.Interfaces.Repositories;
using Core.Application.Interfaces.UseCases;
using Core.Application.Resources;
using Core.Domain.Entities;
using Infra.Notification.Contexts;
using Infra.Notification.Extensions;

namespace Core.Application.UseCases
{
    public class GetTodoUseCase : IGetTodoUseCase
    {
        private readonly NotificationContext _notificationContext;
        private readonly IGenericRepositoryAsync<Todo, int> _genericRepositoryAsync;
        private readonly IMapper _mapper;

        public GetTodoUseCase(NotificationContext notificationContext,
            IGenericRepositoryAsync<Todo, int> genericRepositoryAsync,
            IMapper mapper)
        {
            _notificationContext = notificationContext;
            _genericRepositoryAsync = genericRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<GetTodoUseCaseResponse?> RunAsync(int id)
        {
            Validade(id);

            if (_notificationContext.HasErrorNotification)
                return await Task.FromResult<GetTodoUseCaseResponse?>(default);

            var todo = await _genericRepositoryAsync.GetAsync(id);

            if (todo is null)
            {
                _notificationContext.AddErrorNotification(Msg.DATA_OF_X0_X1_NOT_FOUND_COD,
                    Msg.DATA_OF_X0_X1_NOT_FOUND_TXT.ToFormat("Todo", id));

                return await Task.FromResult<GetTodoUseCaseResponse?>(default);
            }

            var useCaseResponse = _mapper.Map<GetTodoUseCaseResponse>(todo);

            return useCaseResponse;
        }

        private void Validade(int id)
        {
            if (id <= Decimal.Zero)
                _notificationContext.AddErrorNotification(Msg.IDENTIFIER_X0_IS_INVALID_COD,
                    Msg.IDENTIFIER_X0_IS_INVALID_TXT.ToFormat(id));
        }
    }
}