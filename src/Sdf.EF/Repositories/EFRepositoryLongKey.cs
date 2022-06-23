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
    public class EFRepositoryLongKey<TEntity> : IEFRepositoryLongKey<TEntity> where TEntity : Entity<long>, new()
    {
        private EFDbContext _dbContext;
        public EFDbContext DbContext
        {
            get
            {
                if (_dbContext == null)
                {
                    if (_uowManager.Currnet == null)
                        throw new Exception("DbContext is null");
                    var dbContext = _uowManager.Currnet.GetDbContext() as EFDbContext;
                    if (dbContext == null)
                        throw new Exception("DbContext is null");
                    return dbContext;
                }
                return _dbContext;
            }
            private set
            {
                _dbContext = value;
            }
        }
        public virtual DbSet<TEntity> Dbset
        {
            get
            {
                var dbSet = DbContext.GetDbContext().Set<TEntity>();
                if (dbSet == null)
                    throw new Exception("Dbset is null");
                return dbSet;
            }
        }
        protected IUowManager _uowManager;
        public EFRepositoryLongKey(IUowManager uowManager)
        {
            _uowManager = uowManager;
        }

        public DbSet<TEntity> SetTracking()
        {
            return Dbset;
        }
        public IQueryable<TEntity> SetNoTracking()
        {
            return Dbset.AsQueryable();
        }

        private IQueryable<TEntity> GetQueryable(bool tracking = false)
        {
            if (tracking)
            {
                return SetTracking().AsQueryable();
            }
            else
            {
                return SetNoTracking();
            }
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

        public virtual async Task<TEntity> GetAsync(long id, bool tracking = false, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = GetQueryable(tracking);
            queryable = queryable.Where(m => m.Id == id);

            return await queryable.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        }

        public virtual async Task<TEntity> GetAsync<TProperty>(long id, bool tracking = false, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, TProperty>> includeFilter = null, CancellationToken cancellationToken = default)
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
        public virtual async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
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

        public virtual async Task DeleteAsync(long id, CancellationToken cancellationToken = default)
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

        public virtual async Task RemoveRange(IEnumerable<TEntity> list, CancellationToken cancellationToken = default)
        {
            if (list != null)
            {
                foreach (var item in list)
                {
                   await DeleteAsync(item, cancellationToken);
                }
            }
        }
        public virtual void RemoveRange(Expression<Func<TEntity, bool>> expression)
        {
            var list = GetIQueryable().Where(expression).Select(m=>new TEntity() {  Id=m.Id}).ToList();
            if (list != null)
            {
                foreach (var item in list)
                {
                    Delete(item);
                }
            }
        }
        public virtual IDbContext GetCurrentDbContext()
        {
            return this.DbContext;
        }

        public TEntity Add(TEntity entity)
        {
            Dbset.Add(entity);
            return entity;
        }

        public void SetDbContext(IDbContext dbContext)
        {
            this._dbContext = dbContext as EFDbContext;
        }

        public TEntity Get<TProperty>(long id, bool noTracking = false, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, TProperty>> includeFilter = null)
        {
            var iQueryable = GetIQueryable();
            if (noTracking)
            {
                iQueryable = iQueryable.AsNoTracking();
            }
            if (includeFilter != null)
            {
                iQueryable = includeFilter(iQueryable);
            }
            return iQueryable.Where(m => m.Id == id).FirstOrDefault();
        }
    }
}
