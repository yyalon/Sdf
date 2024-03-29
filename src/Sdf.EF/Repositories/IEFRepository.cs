﻿using Microsoft.EntityFrameworkCore;
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
    public interface IEFRepository<TEntity, TPrimaryKey> : IUowProxy where TEntity : Entity<TPrimaryKey>
    {
        IDbContext GetCurrentDbContext();

        DbSet<TEntity> SetTracking();
        IQueryable<TEntity> SetNoTracking();

        Task<PageResult<TResult>> GetPageListAsync<TResult>(int page, int pageSize, Func<IQueryable<TEntity>, IQueryable<TEntity>> filter, Expression<Func<TEntity, TResult>> selector, bool tracking = false, CancellationToken cancellationToken = default);
        Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> expression, bool tracking = false, CancellationToken cancellationToken = default);
        Task<TEntity> GetAsync(TPrimaryKey id, bool tracking = false, CancellationToken cancellationToken = default);
        Task<TEntity> GetAsync<TProperty>(TPrimaryKey id, bool tracking = false, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, TProperty>> includeFilter = null, CancellationToken cancellationToken = default);
        Task<TEntity> SelectFirseAsync(Expression<Func<TEntity, bool>> expression, bool tracking = false, CancellationToken cancellationToken = default);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
        Task<long> CountLongAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);

        Task InsertAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task InsertRangeAsync(IEnumerable<TEntity> list, CancellationToken cancellationToken = default);
        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);
        Task RemoveAsync(TPrimaryKey id, CancellationToken cancellationToken = default);
        Task RemoveRangeAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
        Task RemoveRangeAsync(IEnumerable<TEntity> list, CancellationToken cancellationToken = default);
        
    }
}
