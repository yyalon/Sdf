using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Sdf.Application.Dto;
using Sdf.Domain.Db;
using Sdf.Domain.Entities;
using Sdf.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Sdf.EF.Repositories
{
    public class EFRepositoryLongKey<TEntity> : EFRepository<TEntity, long>, IEFRepositoryLongKey<TEntity> where TEntity : Entity<long>, new()
    {
        public EFRepositoryLongKey(IUowManager uowManager):base(uowManager)
        {
         
        }
        
        public virtual async Task<PageResult<TResult>> GetPageListAsync<TResult>(int page, int pageSize, Func<IQueryable<TEntity>, IQueryable<TEntity>> filter, Expression<Func<TEntity, TResult>> selector, bool tracking = false, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = GetQueryable(tracking);
            queryable = filter?.Invoke(queryable);
            var total = await queryable.CountAsync(cancellationToken: cancellationToken);
            
            var items = await queryable.Skip((page - 1) * pageSize).Take(pageSize).Select(selector).ToListAsync(cancellationToken: cancellationToken);

            return new PageResult<TResult>(total, items, pageSize);
        }

        public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression, bool tracking = false, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = GetQueryable(tracking);
            if (expression != null)
                queryable = queryable.Where(expression);

            return await queryable.ToListAsync(cancellationToken: cancellationToken);
        }
        
        public virtual async Task<TEntity> GetAsync(long id, bool tracking = false, CancellationToken cancellationToken = default)
        {
            if (TryGetCacheValue(id, out TEntity entity))
            {
                return entity;
            }

            IQueryable<TEntity> queryable = GetQueryable(tracking);
            queryable = queryable.Where(m => m.Id == id);

            entity= await queryable.FirstOrDefaultAsync(cancellationToken: cancellationToken);
            
            TryAddCache(entity);

            return entity;
        }

        public virtual async Task<TEntity> GetAsync<TProperty>(long id, bool tracking = false, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, TProperty>> includeFilter = null, CancellationToken cancellationToken = default)
        {
            if (TryGetCacheValue(id, out TEntity entity))
            {
                return entity;
            }

            IQueryable<TEntity> queryable = GetQueryable(tracking);
            queryable = queryable.Where(m => m.Id == id);
            queryable = includeFilter?.Invoke(queryable);

            entity = await queryable.FirstOrDefaultAsync(cancellationToken: cancellationToken);
            TryAddCache(entity);

            return entity;
        }

        public virtual async Task<TEntity> SelectFirseAsync(Expression<Func<TEntity, bool>> expression, bool tracking = false, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = GetQueryable(tracking);
            if (expression != null)
                queryable = queryable.Where(expression);

            return await queryable.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = GetQueryable();
            if (expression != null)
                queryable = queryable.Where(expression);

            return await queryable.Where(expression).CountAsync(cancellationToken);
        }

        public virtual async Task<long> CountLongAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = GetQueryable();
            if (expression != null)
                queryable = queryable.Where(expression);

            return await queryable.Where(expression).LongCountAsync(cancellationToken);
        }
        
        public virtual async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = GetQueryable();
            if (expression != null)
                queryable = queryable.Where(expression);

            return await queryable.AnyAsync(cancellationToken);
        }
        
        public virtual async Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            TryAddCache(entity);
            await Dbset.AddAsync(entity, cancellationToken);
        }

        public virtual async Task InsertRangeAsync(IEnumerable<TEntity> list, CancellationToken cancellationToken = default)
        {
            foreach (var entity in list)
            {
                TryAddCache(entity);
            }
           await Dbset.AddRangeAsync(list, cancellationToken);
        }

        public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            DbContext.GetDbContext().Update(entity);
            await Task.CompletedTask;
        }
        
        public virtual async Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity is ISoftDelete)
            {
                ISoftDelete softDelete = entity as ISoftDelete;
                softDelete.Delete();
            }
            else
            {
                Dbset.Remove(entity);
            }
            TryRemoveCache(entity.Id);
            await Task.CompletedTask;
        }

        public virtual async Task RemoveAsync(long id, CancellationToken cancellationToken = default)
        {
            if (typeof(TEntity).IsAssignableFrom(typeof(ISoftDelete)))
            {
                var entity = await GetAsync(id, true, cancellationToken);
                ISoftDelete softDelete = entity as ISoftDelete;
                softDelete.Delete();
            }
            else
            {
                var entity = new TEntity();
                entity.SetId(id);
                Dbset.Remove(entity);
            }
        }

        public virtual async Task RemoveRangeAsync(IEnumerable<TEntity> list, CancellationToken cancellationToken = default)
        {
            if (list != null)
            {
                foreach (var item in list)
                {
                   await RemoveAsync(item, cancellationToken);
                }
            }
        }
        
        public virtual async Task RemoveRangeAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            var list = GetQueryable().Where(expression).Select(m=>new TEntity() {  Id=m.Id}).ToList();
            if (list != null)
            {
                foreach (var item in list)
                {
                   await RemoveAsync(item, cancellationToken);
                }
            }
        }
        
        public virtual IDbContext GetCurrentDbContext()
        {
            return DbContext;
        }

    }
}
