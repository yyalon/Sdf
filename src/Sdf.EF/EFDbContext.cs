using Microsoft.EntityFrameworkCore;
using Sdf.Common;
using Sdf.Domain.Db;
using Sdf.Domain.Entities;
using Sdf.Exceptions;
using Sdf.Fundamentals.Logs;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Concurrent;

namespace Sdf.EF
{
    public class EFDbContext : IDbContext
    {
        private readonly ConcurrentDictionary<string, object> Leve1CacheData = new();
        private readonly DbContext _dbContext;
        private readonly ILog _logger;
        private IDbChangeEventHandler _dbChangeEventHandler;
        public EFDbContext(DbContext dbContext, ILog log, IDbChangeEventHandler dbChangeEventHandler)
        {
            _dbContext = dbContext;
            _logger = log;
            _dbChangeEventHandler = dbChangeEventHandler;
        }
        public void Dispose()
        {
            Leve1CacheData.Clear();
            if (_dbContext != null) _dbContext.Dispose();
        }

        public IDbConnection GetDbConnection()
        {
            return _dbContext.Database.GetDbConnection();
        }

        public DbContext GetDbContext()
        {
            return _dbContext;
        }
        public bool TryGetCacheValue(string key, out object value)
        {
            return Leve1CacheData.TryGetValue(key, out value);
        }
        public bool TryAddCache(string key, object value)
        {
            return Leve1CacheData.TryAdd(key, value);
        }
        public bool TryRemoveCache(string key)
        {
            return Leve1CacheData.TryRemove(key, out _);
        }
        public async Task<DbChangeResult> SaveChageAsync(CancellationToken cancellationToken = default)
        {

            var entrys = _dbContext.ChangeTracker.Entries();
            bool changeFlag = false;//默认没有更改
            List<ValidationResult> validationResults = new List<ValidationResult>();
            foreach (var item in entrys)
            {
                if (item.State != EntityState.Unchanged)
                {
                    changeFlag = true;
                    if (item.Entity is IUpdateTimeField)
                    {
                        var updateTimeField = item.Entity as IUpdateTimeField;
                        updateTimeField.UpdateTime = DateTime.Now;
                    }
                }

                var validateList = ValidatorHelper.GetValidationResult(item.Entity);
                if (validateList != null && validateList.Count > 0)
                {
                    validationResults.AddRange(validateList);
                }
            }

            if (!changeFlag)
            {
                return new DbChangeResult(true);
            }
            if (validationResults.Count > 0)
            {
                return new DbChangeResult(0, false, validationResults.Select(m => new InvalidateException(m)));
            }
            try
            {
                int changeNum = await _dbContext.SaveChangesAsync(cancellationToken);
                if (_dbChangeEventHandler.Disable)
                {
                    if (changeNum > 0)
                    {
                        foreach (var item in entrys)
                        {
                            switch (item.State)
                            {
                                case EntityState.Deleted:
                                    _dbChangeEventHandler.OnEntityDeleted(item.Entity);
                                    break;
                                case EntityState.Modified:
                                    _dbChangeEventHandler.OnEntityModified(item.Entity);
                                    break;
                                case EntityState.Added:
                                    _dbChangeEventHandler.OnEntityAdded(item.Entity);
                                    break;
                                case EntityState.Unchanged:
                                case EntityState.Detached:
                                default:
                                    break;
                            }
                        }
                    }
                }
                return new DbChangeResult(changeNum, changeNum > 0);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return new DbChangeResult(0, false, new List<Exception>() { ex });
            }
        }
    }
}
