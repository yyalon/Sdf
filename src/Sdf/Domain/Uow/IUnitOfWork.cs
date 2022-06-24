using Sdf.Domain.Db;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sdf.Domain.Uow
{
    public interface IUnitOfWork : IDisposable
    {

        event UowEventHandler Completed;
        Task<DbChangeResult> CompleteAsync(CancellationToken cancellationToken = default);
        IUnitOfWork Outer { get; set; }
        bool IsDisposed { get; }
        IDbContext GetDbContext();
        void Rollback();
    }
    public delegate void UowEventHandler(IUnitOfWork unitOfWork);
}
