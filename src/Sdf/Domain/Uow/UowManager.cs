using Sdf.Core.Autofac;
using Sdf.Domain.Db;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace Sdf.Domain.Uow
{
    public class UowManager : IUowManager
    {
        public ConcurrentQueue<Action<DbChangeResult>> CompletedHandles { get; }
        public IUnitOfWork Currnet
        {
            get; set;
        }
       
        private readonly IocManager _iocManager;
        public UowManager(IocManager iocManager)
        {
            CompletedHandles = new ConcurrentQueue<Action<DbChangeResult>>();
            _iocManager = iocManager;
        }

        public IUnitOfWork Begin(UnitOfWorkOption option = null)
        {
            IUnitOfWork current = null;
            if (option == null)
                option = new UnitOfWorkOption() { Scope = TransactionScopeOption.Required };

            if (option.Scope == TransactionScopeOption.Required && Currnet == null)
            {
                var resolver = _iocManager.GetResolver();
                Currnet = resolver.Resolve<IUnitOfWork>();
                Currnet.Completed += (IUnitOfWork unitOfWork) =>
                {
                    Currnet = unitOfWork.Outer;
                    resolver.Dispose();
                };
                current = Currnet;
            }

            if (option.Scope == TransactionScopeOption.RequiresNew)
            {
                var resolver = _iocManager.GetResolver();
                if (Currnet == null)
                {
                    Currnet = resolver.Resolve<IUnitOfWork>();
                    current = Currnet;
                }
                else
                {
                    var outer = Currnet;
                    Currnet = resolver.Resolve<IUnitOfWork>();
                    Currnet.Outer = outer;
                    current = Currnet;
                }
                Currnet.Completed += (IUnitOfWork unitOfWork) =>
                {
                    Currnet = unitOfWork.Outer;
                    resolver.Dispose();
                };
            }
            return current;
        }

        public void RegisterUowCompleted(Action<DbChangeResult> completedHandle)
        {
            CompletedHandles.Enqueue(completedHandle);
        }
    }
}
