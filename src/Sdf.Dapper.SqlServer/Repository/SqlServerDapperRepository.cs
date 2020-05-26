using Sdf.Domain.Uow;
using System;
using System.Collections.Generic;

namespace Sdf.Dapper.SqlServer.Repository
{
    public class SqlServerDapperRepository : DapperRepository
    {

        public SqlServerDapperRepository(IUowManager uowManager):base(uowManager)
        {
          
        }

        public override int Delete<TEntity,TPrimaryKey>(TPrimaryKey id)
        {
            string sql = $"delete ${nameof(TEntity)} where Id=@Id";
            return DbContext.Execute(sql, new { Id=id});
        }
        public override TEntity Get<TEntity, TPrimaryKey>(TPrimaryKey id)
        {
            string sql = $"select top 1 * from ${nameof(TEntity)} where Id=@Id";
            return DbContext.SelectFirse<TEntity>(sql, new { Id = id });
        }
    }
}
