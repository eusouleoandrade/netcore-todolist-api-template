﻿using Core.Application.Exceptions;
using Core.Application.Interfaces.Repositories;
using Core.Application.Resources;
using Dapper.Contrib.Extensions;
using Infra.Persistence.Configs;
using Microsoft.Extensions.Configuration;

namespace Infra.Persistence.Repositories
{
    public class GenericRepositoryAsync<TEntity, TId> : ConnectionConfig, IGenericRepositoryAsync<TEntity, TId>
        where TEntity : class
        where TId : struct
    {
        protected GenericRepositoryAsync(IConfiguration configuration)
            : base(configuration) { }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                return await _connection.GetAllAsync<TEntity>();
            }
            catch (Exception ex)
            {
                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }
        }

        public virtual async Task<TEntity> GetAsync(TId id)
        {
            try
            {
                return await _connection.GetAsync<TEntity>(id);
            }
            catch (Exception ex)
            {
                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }
        }
    }
}