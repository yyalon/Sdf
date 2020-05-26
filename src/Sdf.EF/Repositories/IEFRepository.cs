using Microsoft.EntityFrameworkCore.Query;
using Sdf.Domain.Db;
using Sdf.Domain.Entities;
using Sdf.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Sdf.EF.Repositories
{
    public interface IEFRepository<TEntity, TPrimaryKey> : IUowProxy where TEntity : Entity<TPrimaryKey>
    {
        IQueryable<TEntity> GetIQueryable();
        void SetDbContext(IDbContext dbContext);
        IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> expression = null, bool noTracking = false);
        TEntity Get(TPrimaryKey id, bool noTracking = false);
     
        TEntity SelectFirse(Expression<Func<TEntity, bool>> expression, bool noTracking = false);
        int Count(Expression<Func<TEntity, bool>> expression = null);
        long CountLong(Expression<Func<TEntity, bool>> expression = null);
        void Insert(TEntity entity);
        void InsertRange(IEnumerable<TEntity> list);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(TPrimaryKey id);
        void RemoveRange(Expression<Func<TEntity, bool>> expression);
        void RemoveRange(IEnumerable<TEntity> list);
        bool Any(Expression<Func<TEntity, bool>> expression);
        IQueryable<TEntity> GetPageList(int page, int pageSize, Func<IQueryable<TEntity>, IQueryable<TEntity>> filter, out int total, bool noTracking = false);
        IDbContext GetCurrentDbContext();
        TEntity Add(TEntity entity);
    }
}
