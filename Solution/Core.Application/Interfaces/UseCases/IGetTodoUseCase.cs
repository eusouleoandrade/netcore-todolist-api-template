using Core.Application.Dtos.Responses;

namespace Core.Application.Interfaces.UseCases
{
    public interface IGetTodoUseCase : IUseCase<int, GetTodoUseCaseResponse>
    {
    }
}
