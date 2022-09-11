using Core.Application.Interfaces;
using Infra.Persistence.Configs;
using Microsoft.Extensions.Configuration;

namespace Infra.Persistence.Repositories
{
    public abstract class GenericRepositoryAsync<TEntity, TId> : ConnectionConfig, IGenericRepositoryAsync<TEntity, TId>
        where TEntity : class
        where TId : struct
    {
        protected GenericRepositoryAsync(IConfiguration configuration)
            : base(configuration) { }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            throw new NotImplementedException();

            //try
            //{
            //    return await _connection.QueryAsync<TEntity>();
            //}
            //catch (Exception ex)
            //{
            //    throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            //}
        }

        public virtual async Task<TEntity> GetAsync(TId id)
        {
            throw new NotImplementedException();

            //try
            //{
            //    return await _connection.GetAsync<TEntity>(id);
            //}
            //catch (Exception ex)
            //{
            //    throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            //}
        }
    }
}