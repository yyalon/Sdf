using Microsoft.EntityFrameworkCore.Query;
using Sdf.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sdf.EF.Repositories
{
    public interface IEFRepositoryLongKey<TEntity> : IEFRepository<TEntity,long> where TEntity : Entity<long>
    {
        
    }
}
