﻿using Sdf.Core;
using Sdf.Dapper;
using Sdf.Dapper.Interfaces;
using Sdf.Dapper.SqlServer.Repository;
using Sdf.Domain.Db;
using Sdf.Domain.Uow;
using System;

namespace Sdf.Dapper.SqlServer
{
    public static class SdfConfigManagerExtensions
    {
        public static SdfConfigManager UseDapper(this SdfConfigManager sdfConfig,Action<DapperOption> action)
        {
            DapperOption dapperOption = new DapperOption(DbType.MSSQL);
            action(dapperOption);
            sdfConfig.Register.RegisterSingleton<DapperOption>(dapperOption);
            sdfConfig.Register.RegisterTransient<IDbConnectionProvider, DbConnectionProvider>();
            sdfConfig.Register.RegisterTransient<IDapperRepository, SqlServerDapperRepository>();
            return sdfConfig;
        }
    }
}
