﻿using Core.Application.Exceptions;
using Core.Application.Resources;
using Dapper;
using Infra.Persistence.Configs;
using Infra.Persistence.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Infra.Persistence.Bootstraps
{
    public class BootstrapRepository : ConnectionConfig, IDatabaseBootstrap
    {
        public BootstrapRepository(IConfiguration configuration)
            : base(configuration) { }

        public void Setup()
        {
            try
            {
                // Bootstrap table: TodoList
                string selectTableNameSql = @"SELECT name
                                            FROM sqlite_master
                                            WHERE type='table' AND name = 'TodoList';";

                var table = _connection.Query<string>(selectTableNameSql);

                var tableName = table.FirstOrDefault();

                if (!string.IsNullOrEmpty(tableName) && tableName == "TodoList")
                    return;

                string createTableSql = @"CREATE TABLE TodoList (
                                        Id INTEGER PRIMARY KEY,
                                        Title VARCHAR(60) NOT NULL,
                                        Done BOOLEAN NOT NULL);";

                _connection.Execute(createTableSql);
            }
            catch (Exception ex)
            {
                throw new AppException(Msg.DATA_BASE_SERVER_ERROR_TXT, ex);
            }
        }
    }
}