using Core.Domain.Entities;

namespace Core.Application.Interfaces
{
    public interface ITodoListRepository
    {
        Task CreateAsync(TodoList entity);
    }
}
