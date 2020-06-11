using Sdf.Core;
using Sdf.Dapper;
using Sdf.Domain.Db;
using Sdf.Domain.Uow;

namespace Sdf.Dapper
{
    public static class SdfConfigManagerExtensions
    {
        public static SdfConfigManager UseDapper(this SdfConfigManager sdfConfig)
        {
            sdfConfig.Register.RegisterTransient<IDbContext, DapperDbContext>();
            sdfConfig.Register.RegisterTransient<IUnitOfWork, DapperUnitOfWork>();
            return sdfConfig;
        }
    }
}
