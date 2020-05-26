using Sdf.Domain.Db;
using Sdf.Domain.Uow;

namespace Sdf.Dapper
{
    public class DapperUnitOfWork : IUnitOfWork
    {
        private readonly DapperDbContext dapperDbContext;
        public DapperUnitOfWork(IDbContext dbContext)
        {
            dapperDbContext = dbContext as DapperDbContext;
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
            DbChangeResult dbChangeResult = dapperDbContext.SaveChage();
            isDisposed = true;
            Completed?.Invoke(this);
            return dbChangeResult;
        }
        public IDbContext GetDbContext()
        {
            if (isDisposed)
                return null;
            return dapperDbContext;
        }

        public void Dispose()
        {
            isDisposed = true;
            dapperDbContext.Dispose();
        }

        public void Rollback()
        {
            if (dapperDbContext.Transaction != null)
            {
                dapperDbContext.Transaction.Rollback();
            }
            
        }
    }
}
