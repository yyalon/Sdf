using Sdf.Domain.Uow;
using System;
using System.Collections.Generic;

namespace Sdf.Dapper.SqlServer.Repository
{
    public class MysqlDapperRepository : DapperRepository
    {

        public MysqlDapperRepository(IUowManager uowManager):base(uowManager)
        {
          
        }

        public override int Delete<TEntity,TPrimaryKey>(TPrimaryKey id)
        {
            string sql = $"delete ${nameof(TEntity)} where Id=@Id";
            return DbContext.Execute(sql, new { Id=id});
        }
        public override TEntity Get<TEntity, TPrimaryKey>(TPrimaryKey id)
        {
            string sql = $"select * from ${nameof(TEntity)} where Id=@Id limit 1";
            return DbContext.SelectFirse<TEntity>(sql, new { Id = id });
        }
    }
}
