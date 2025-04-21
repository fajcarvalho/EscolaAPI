using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EscolaAPI.Infrastructure.Extensions
{
    public static class DatabaseExtensions 
    {
        public static async Task<List<string>> GetTablesAsync(this DatabaseFacade database) 
        {
            var tables = new List<string>();
            await using var command = database.GetDbConnection().CreateCommand();
            command.CommandText = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE'";

            await database.OpenConnectionAsync();

            await using var reader = await command.ExecuteReaderAsync();
            while (await reader.ReadAsync()) 
            {
                tables.Add(reader.GetString(0));
            }
            return tables;
        }
    }
}
    