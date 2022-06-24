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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sdf.EF.Repositories
{
    public class EFRepositoryStringKey<TEntity> : EFRepository<TEntity, string>, IEFRepositoryStringKey<TEntity> where TEntity : Entity<string>, new()
    {
        public EFRepositoryStringKey(IUowManager uowManager) : base(uowManager)
        {

        }

        public virtual async Task<PageResult<TEntity>> GetPageListAsync(int page, int pageSize, Func<IQueryable<TEntity>, IQueryable<TEntity>> filter, Func<IQueryable<TEntity>, IQueryable<TEntity>> selectFilter = null, bool tracking = false, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = GetQueryable(tracking);
            queryable = filter?.Invoke(queryable);
            var total = await queryable.CountAsync(cancellationToken: cancellationToken);
            if (selectFilter != null)
            {
                queryable = selectFilter(queryable);
            }
            var items = await queryable.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken: cancellationToken);

            return new PageResult<TEntity>(total, items, pageSize);
        }

        public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression, bool tracking = false, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = GetQueryable(tracking);
            if (expression != null)
                queryable = queryable.Where(expression);

            return await queryable.ToListAsync(cancellationToken: cancellationToken);
        }

        public virtual async Task<TEntity> GetAsync(string id, bool tracking = false, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = GetQueryable(tracking);
            queryable = queryable.Where(m => m.Id == id);

            return await queryable.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public virtual async Task<TEntity> GetAsync<TProperty>(string id, bool tracking = false, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, TProperty>> includeFilter = null, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = GetQueryable(tracking);
            queryable = queryable.Where(m => m.Id == id);
            queryable = includeFilter?.Invoke(queryable);

            return await queryable.FirstOrDefaultAsync(cancellationToken: cancellationToken);
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
            await Dbset.AddAsync(entity, cancellationToken);
        }

        public virtual async Task InsertRangeAsync(IEnumerable<TEntity> list, CancellationToken cancellationToken = default)
        {
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
            await Task.CompletedTask;
        }

        public virtual async Task RemoveAsync(string id, CancellationToken cancellationToken = default)
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
            var list = GetQueryable().Where(expression).Select(m => new TEntity() { Id = m.Id }).ToList();
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
            return this.DbContext;
        }

    }
}
