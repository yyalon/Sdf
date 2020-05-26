using Sdf.Domain.Db;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Domain.Uow
{
    public interface IUnitOfWork : IDisposable
    {

        event UowEventHandler Completed;
        DbChangeResult Complete();
        IUnitOfWork Outer { get; set; }
        bool IsDisposed { get; }
        IDbContext GetDbContext();
        void Rollback();
    }
    public delegate void UowEventHandler(IUnitOfWork unitOfWork);
}
