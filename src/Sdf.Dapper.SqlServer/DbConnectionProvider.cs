using Sdf.Dapper.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Sdf.Dapper.SqlServer
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
            return new SqlConnection(_dapperOption.DbConnection);
        }
    }
}
