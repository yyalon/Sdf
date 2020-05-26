using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sdf.Common;
using Sdf.Domain.Db;
using Sdf.Domain.Entities;
using Sdf.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;

namespace Sdf.EF
{
    public class EFDbContext : IDbContext
    {
        private DbContext _dbContext;
        private readonly ILogger _logger;
        private IDbChangeEventHandler _dbChangeEventHandler;
        public EFDbContext(DbContext dbContext, ILogger<EFDbContext> log, IDbChangeEventHandler dbChangeEventHandler)
        {
            _dbContext = dbContext;
            _logger = log;
            _dbChangeEventHandler = dbChangeEventHandler;
        }
        public void Dispose()
        {
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
        public DbChangeResult SaveChage()
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
                int changeNum = _dbContext.SaveChanges();
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
                var innerException = ExceptionHelper.GetInnerExceptionMess(ex);
                _logger.LogWarning(innerException, innerException.Message);
                return new DbChangeResult(0, false, new List<Exception>() { innerException});
            }
        }
    }
}
