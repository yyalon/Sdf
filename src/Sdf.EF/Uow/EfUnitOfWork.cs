using Sdf.Domain.Db;
using Sdf.Domain.Uow;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.EF.Uow
{
    public class EfUnitOfWork: IUnitOfWork
    {
        private EFDbContext _dbContext;
        public EfUnitOfWork(IDbContext dbContext)
        {
            _dbContext = dbContext as EFDbContext;
        }

        private IUnitOfWork outer;
        public IUnitOfWork Outer
        {
            get
            {
                return outer;
            }
            set
            {
                outer = value;
            }
        }
        private bool isDisposed;
        public bool IsDisposed
        {
            get
            {
                return isDisposed;
            }
        }

        public event UowEventHandler Completed;

        public DbChangeResult Complete()
         {
            DbChangeResult dbChangeResult= _dbContext.SaveChage();
            isDisposed = true;
            Completed?.Invoke(this);
            return dbChangeResult;
        }
        public IDbContext GetDbContext()
        {
            if (isDisposed)
                return null;
            return _dbContext;
        }

        public void Dispose()
        {
            isDisposed = true;
            _dbContext.Dispose();
        }

        public void Rollback()
        {
            Dispose();
        }

        public T GetDbContext<T>() where T : IDbContext
        {
            throw new NotImplementedException();
        }
    }
}
