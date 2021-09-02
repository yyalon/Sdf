using Microsoft.EntityFrameworkCore.Query;
using Sdf.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sdf.EF.Repositories
{
    public interface IEFRepositoryStringKey<TEntity> : IEFRepository<TEntity, string> where TEntity : Entity<string>
    {
        TEntity Get<TProperty>(string id, bool noTracking = false, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, TProperty>> includeFilter = null);
    }
}
