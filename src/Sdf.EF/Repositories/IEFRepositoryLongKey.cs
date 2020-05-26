using Microsoft.EntityFrameworkCore.Query;
using Sdf.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sdf.EF.Repositories
{
    public interface IEFRepositoryLongKey<TEntity> : IEFRepository<TEntity,long> where TEntity : Entity<long>
    {
        TEntity Get<TProperty>(long id, bool noTracking = false, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, TProperty>> includeFilter = null);
    }
}
