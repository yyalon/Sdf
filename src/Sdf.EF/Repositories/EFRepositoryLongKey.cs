using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
    public class EFRepositoryLongKey<TEntity> : IEFRepositoryLongKey<TEntity> where TEntity : Entity<long> , new()
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

        public async Task<List<TEntity>> GetPageListAsync(int page, int pageSize, Func<IQueryable<TEntity>, IQueryable<TEntity>> filter, out int total, bool tracking = false, CancellationToken cancellationToken = defaul)
        {
            var iquery = GetIQueryable();
            if (noTracking)
                iquery = iquery.AsNoTracking();
            iquery = filter?.Invoke(iquery);
            total = iquery.Count();
            return iquery.Skip((page - 1) * pageSize).Take(pageSize);
        }

        public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression, bool tracking = false, CancellationToken cancellationToken = default)
        {
            IQueryable<TEntity> queryable = GetQueryable(tracking);
            if (expression != null)
                queryable = queryable.Where(expression);
            
            return await queryable.ToListAsync();
        }

        public virtual TEntity Get(long id, bool tracking = false)
        {
            IQueryable<TEntity> iQueryable = GetIQueryable().Where(m => m.Id == id);
            if (noTracking)
                iQueryable = iQueryable.AsNoTracking();
            return iQueryable.FirstOrDefault();
        }

        public virtual TEntity SelectFirse(Expression<Func<TEntity, bool>> expression, bool noTracking = false)
        {

            IQueryable<TEntity> iQueryable = GetIQueryable().Where(expression);
            if (noTracking)
                iQueryable = iQueryable.AsNoTracking();
            return iQueryable.FirstOrDefault();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> expression = null)
        {
            if (expression == null)
                return GetIQueryable().Count();
            return GetIQueryable().Where(expression).Count();
        }

        public virtual long CountLong(Expression<Func<TEntity, bool>> expression = null)
        {
            return GetIQueryable().Where(expression).LongCount();
        }
        public virtual bool Any(Expression<Func<TEntity, bool>> expression)
        {
            return GetIQueryable().Any(expression);
        }
        
        public virtual void Insert(TEntity entity)
        {
            Dbset.Add(entity);
        }

        public virtual void InsertRange(IEnumerable<TEntity> list)
        {
            Dbset.AddRange(list);
        }

        public virtual void Update(TEntity entity)
        {
            var entityEntry = DbContext.GetDbContext().Entry<TEntity>(entity);
            if (entityEntry == null)
                return;
            if (entityEntry != null || entityEntry.State != EntityState.Modified)
            {
                entityEntry.State = EntityState.Modified;
            }
        }

        public virtual void Delete(TEntity entity)
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
        }

        public virtual void Delete(long id)
        {
            if (typeof(TEntity).IsAssignableFrom(typeof(ISoftDelete)))
            {
                var entity=Get(id);
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

        public virtual void RemoveRange(IEnumerable<TEntity> list)
        {
            if (list != null)
            {
                foreach (var item in list)
                {
                    Delete(item);
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
