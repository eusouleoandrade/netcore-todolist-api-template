using Core.Domain.Entities;

namespace Core.Application.Interfaces.Repositories
{
    public interface ITodoRepositoryAsync : IGenericRepositoryAsync<Todo, int>
    {
        Task<Todo?> CreateAsync(Todo entity);

        Task<bool> DeleteAsync(int id);

        Task<bool> UpdateAsync(Todo entity);
    }
}