using Sdf.Domain.Uow;
using System.Collections.Generic;

namespace Sdf.Dapper
{
    public interface IDapperRepository : IUowProxy
    {
        IEnumerable<T> GetList<T>(string sql, object param = null);
        TEntity Get<TEntity, TPrimaryKey>(TPrimaryKey id);
        T SelectFirse<T>(string sql, object param = null);
        T ExecuteScalar<T>(string sql, object param = null);
        int Insert<TEntity>(TEntity entity) where TEntity : class;
        void InsertBulk<TEntity>(IEnumerable<TEntity> list) where TEntity : class;
        void Update<TEntity>(TEntity entity) where TEntity : class;
        int Delete<TEntity,TPrimaryKey>(TPrimaryKey id);
        int Execute(string sql, object param = null);
        bool Exists(string sql, object param = null);
        DapperDbContext DbContext { get; }
    }

}
