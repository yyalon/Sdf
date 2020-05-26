using Sdf.Domain.Uow;
using System;
using System.Collections.Generic;

namespace Sdf.Dapper
{
    public abstract class DapperRepository : IDapperRepository
    {
        protected IUowManager _uowManager;
        public DapperRepository(IUowManager uowManager)
        {
            _uowManager = uowManager;
        }
        public DapperDbContext DbContext
        {
            get
            {
                if (_uowManager.Currnet == null)
                    throw new Exception("DbContext is null");
                var dbContext = _uowManager.Currnet.GetDbContext() as DapperDbContext;
                if (dbContext == null)
                    throw new Exception("DbContext is null");
                return dbContext;
            }
        }


        public abstract int Delete<TEntity, TPrimaryKey>(TPrimaryKey id);

        public virtual int Execute(string sql, object param = null)
        {
            return DbContext.Execute(sql, param);
        }
        public abstract TEntity Get<TEntity, TPrimaryKey>(TPrimaryKey id);

        public virtual IEnumerable<T> GetList<T>(string sql, object param = null)
        {
            return DbContext.GetList<T>(sql, param);
        }

        public virtual int Insert<TEntity>(TEntity entity) where TEntity : class
        {
            DbContext.Insert<TEntity>(entity);
            return 1;
        }

        public virtual void InsertBulk<TEntity>(IEnumerable<TEntity> list) where TEntity : class
        {
            DbContext.InsertBulk(list);
        }

        public virtual T SelectFirse<T>(string sql, object param = null)
        {
            return DbContext.SelectFirse<T>(sql, param);
        }

        public virtual void Update<TEntity>(TEntity entity) where TEntity : class
        {
             DbContext.Update<TEntity>(entity);
        }
        public virtual bool Exists(string sql, object param = null)
        {
            var obj = DbContext.SelectFirse<object>(sql, param);
            return obj != null;
        }
    }
}
