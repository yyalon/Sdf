using MySql.Data.MySqlClient;
using Sdf.Dapper.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Sdf.Dapper.Mysql
{
    public class DbConnectionProvider : IDbConnectionProvider
    {
        private readonly DapperOption _dapperOption;
        public DbConnectionProvider(DapperOption dapperOption)
        {
            _dapperOption = dapperOption;
        }
        public IDbConnection GetDbConnection()
        {
            return new MySqlConnection(_dapperOption.DbConnection);
        }
    }
}
