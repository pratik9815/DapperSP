﻿using Microsoft.Data.SqlClient;
using System.Data;

namespace DapperWithSQL.DataContext
{
    public class DapperContext 
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection DbConnection()
        {
            return new SqlConnection(_connectionString);
        }

    }
}
