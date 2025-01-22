using Sdf.Domain.Db;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Domain.Uow
{
    public interface IUowManager
    {
        IUnitOfWork Currnet { get; }
        IUnitOfWork Begin(UnitOfWorkOption option = null);
        ConcurrentQueue<Action<DbChangeResult>> CompletedHandles { get; }
        void RegisterUowCompleted(Action<DbChangeResult> completedHandle);
    }
}
