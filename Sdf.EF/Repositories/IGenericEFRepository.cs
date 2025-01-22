using Microsoft.EntityFrameworkCore;
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
    public interface IGenericEFRepository<TPrimaryKey> : IUowProxy
    {
        IDbContext GetCurrentDbContext();
        void SetDbContext(IDbContext dbContext);

        DbSet<TEntity> SetTracking<TEntity>() where TEntity : Entity<TPrimaryKey>;
        IQueryable<TEntity> SetNoTracking<TEntity>() where TEntity : Entity<TPrimaryKey>;
    }
}
