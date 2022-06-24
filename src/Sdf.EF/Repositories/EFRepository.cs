using Microsoft.EntityFrameworkCore;
using Sdf.Domain.Entities;
using Sdf.Domain.Uow;
using System;
using System.Linq;

namespace Sdf.EF.Repositories
{
    public class EFRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {
        protected EFDbContext _dbContext;
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
            protected set
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
        public EFRepository(IUowManager uowManager)
        {
            _uowManager = uowManager;
        }

        protected IQueryable<TEntity> GetQueryable(bool tracking = false)
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

        public virtual DbSet<TEntity> SetTracking()
        {
            return Dbset;
        }

        public virtual IQueryable<TEntity> SetNoTracking()
        {
            return Dbset.AsQueryable();
        }
    }
}
