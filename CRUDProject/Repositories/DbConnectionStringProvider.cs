using CRUDProject.Interfaces.Repositories;
using CRUDProject.Interfaces.Services;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace CRUDProject.Repositories
{
    public class DbConnectionStringProvider: IDbConnectionStringProvider
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DbConnectionStringProvider(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _connectionString = _configuration.GetConnectionString("SqlConnection");

        }

        public IDbConnection CreateConnection()  => new SqlConnection(_connectionString);

    }
}
