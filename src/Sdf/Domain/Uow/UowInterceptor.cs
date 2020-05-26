using Castle.DynamicProxy;
using Sdf.Application;
using Sdf.Common;
using Sdf.Domain.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sdf.Domain.Uow
{
    public class UowInterceptor:IInterceptor
    {
        private IUowManager uowManager;
        public UowInterceptor(IUowManager uowManager)
        {
            this.uowManager = uowManager;
        }
        public void Intercept(IInvocation invocation)
        {
            var uow = uowManager.Begin();
            invocation.Proceed();
            var returnValue = invocation.ReturnValue;
            OperationResult operationResult = null;
            if (returnValue != null && ReflectionHelper.TypeIsSame(returnValue.GetType(), typeof(OperationResult)))
            {
                operationResult = returnValue as OperationResult;
            }
            if (uow != null)
            {
                var dbRes = uow.Complete();
                if (operationResult != null && dbRes != null)
                {
                    //如果数据库操作发生异常，同时业务操作返回操作成功
                    if (!dbRes.State && operationResult.State)
                    {
                        invocation.ReturnValue = new OperationResult("发生异常", false, dbRes.ErrorList != null ? dbRes.ErrorList.Select(m => m.Message).ToList() : null);
                    }
                }
                while (!uowManager.CompletedHandles.IsEmpty)
                {
                    Action<DbChangeResult> handle = null;
                    if (uowManager.CompletedHandles.TryDequeue(out handle))
                    {
                        handle.Invoke(dbRes);
                    }
                }
                uow.Dispose();
            }
        }
    }
}
