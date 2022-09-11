namespace Core.Application.Interfaces
{
    public interface IGenericRepositoryAsync<TEntity, TId>
        where TEntity : class
        where TId : struct
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<TEntity> GetAsync(TId id);
    }
}
