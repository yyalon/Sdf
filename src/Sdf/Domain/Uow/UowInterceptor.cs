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
        private bool IsOperationResultType(Type type)
        {
            if (type.IsGenericType)
            {
                var gt = type.GetGenericTypeDefinition();
                return gt == typeof(OperationResult<>);
            }
            return type == typeof(OperationResult);
        }
        public object GetDefaultValue(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }
        private object CreateFaildOperationResult(Type type,List<object> errList)
        {
            if (type.IsGenericType)
            {
                Type[] typeParameters = type.GetGenericArguments();
                return Activator.CreateInstance(type, "数据库异常", false, GetDefaultValue(typeParameters.FirstOrDefault()), errList);
            }
            return Activator.CreateInstance(type, "数据库异常", false, null, errList);
        }
        public void Intercept(IInvocation invocation)
        {
            var uow = uowManager.Begin();
            invocation.Proceed();
            var returnValue = invocation.ReturnValue;
            if (uow != null)
            {
                var dbRes = uow.Complete();
                if (returnValue != null && dbRes != null)
                {
                    if (!dbRes.State)
                    {
                        if (IsOperationResultType(returnValue.GetType()))
                        {
                            List<object> errList = new List<object>();
                            if (dbRes.ErrorList != null)
                            {
                                foreach (var item in dbRes.ErrorList)
                                {
                                    errList.Add(item.Message);
                                }
                            }
                            invocation.ReturnValue = CreateFaildOperationResult(returnValue.GetType(), errList);
                        }
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
