using Core.Application.Exceptions;
using Core.Application.Interfaces;
using Core.Application.Resources;
using Core.Domain.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Infra.Persistence.Repositories
{
    public class TodoListRepository : GenericRepositoryAsync<TodoList, int>, ITodoListRepository
    {
        public TodoListRepository(IConfiguration configuration)
            : base(configuration) { }

        public async Task CreateAsync(TodoList entity)
        {
            try
            {
                string insertSql = @"INSERT INTO TodoList (title, done)
                                    VALUES(@title, @done)
                                    RETURNING id;";

                var id = await _connection.ExecuteScalarAsync<int>(insertSql,
                new
                {
                    title = entity.Title,
                    done = entity.Done
                });


                await _connection.ExecuteAsync("INSERT INTO Product (Name, Description)" +
                    "VALUES (@Name, @Description);", entity);
            }
            catch (Exception ex)
            {
                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }

        }
    }
}