using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Sdf.Dapper
{
    public class DapperOption
    {
        private DapperOption()
        {
            CommontTimeOut = 10;//10s
        }
        public DapperOption(DbType dbType)
        {
            CommontTimeOut = 10;//10s
            DbType = dbType;
        }
        public string DbConnection { get; set; }
        public int CommontTimeOut { get; set; }
        public DbType DbType { get; private set; }
    }
    public enum DbType 
    {
        MSSQL,
        MYSQL
    }
}
