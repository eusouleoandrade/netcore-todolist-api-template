using Core.Application.Exceptions;
using Core.Application.Interfaces.Repositories;
using Core.Application.Resources;
using Core.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Infra.Persistence.Repositories
{
    public class TodoRepositoryAsync : GenericRepositoryAsync<Todo, int>, ITodoRepositoryAsync
    {
        public TodoRepositoryAsync(IConfiguration configuration)
            : base(configuration) { }

        public async Task<Todo?> CreateAsync(Todo entity)
        {
            try
            {
                string insertSql = @"INSERT INTO todo (title, done)
                                    VALUES(@title, @done)
                                    RETURNING id;";

                var id = await _connection.ExecuteScalarAsync<int>(insertSql,
                new
                {
                    title = entity.Title,
                    done = entity.Done
                });

                if (id > decimal.Zero)
                    return await base.GetAsync(id);

                return await Task.FromResult<Todo?>(default);
            }
            catch (Exception ex)
            {
                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }

        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                string deleteSql = @"DELETE FROM todo
                                    WHERE id = @id";

                var affectedrows = await _connection.ExecuteAsync(deleteSql, new
                {
                    id
                });

                return affectedrows > Decimal.Zero;
            }
            catch (Exception ex)
            {
                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }
        }

        public async Task<bool> UpdateAsync(Todo entity)
        {
            try
            {
                string updateSql = @"UPDATE todo
                                    SET title=@title, done=@done
                                    WHERE id=@id";

                var affectedrows = await _connection.ExecuteAsync(updateSql, new
                {
                    id = entity.Id,
                    title = entity.Title,
                    done = entity.Done
                });

                return affectedrows > Decimal.Zero;
            }
            catch (Exception ex)
            {
                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }
        }
    }
}