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
       
        private bool IsOperationResult(Type objectType)
        {
            if (objectType == typeof(OperationResult) || objectType.IsSubclassOf(typeof(OperationResult)))
            {
                return true;
            }
            if (objectType.IsGenericType && objectType.GetGenericTypeDefinition() == typeof(OperationResult<>))
            {
                return true;
            }
            return false;
        }
        private bool? ParseOperationResultState(object obj)
        {
            if (obj != null && IsOperationResult(obj.GetType()))
            {
                var pState = obj.GetType().GetProperties().Where(m => m.Name == nameof(OperationResult.State)).FirstOrDefault();
                var stateValue = (bool)pState.GetValue(obj);
                return stateValue;
            }
            return null;
        }
        private object CreateFailedOperationResult(Type type, string msg, List<object> errList)
        {
            var obj = Activator.CreateInstance(type);
            if (obj != null)
            {
                var operationResult = obj as OperationResult;
                operationResult.CreateDefaultFailedResult(msg, errList);
            }
            return obj;
        }
        public void Intercept(IInvocation invocation)
        {
            var uow = uowManager.Begin();
            invocation.Proceed();

            if (uow != null)
            {
                var returnValue = invocation.ReturnValue;
                
                bool? state = ParseOperationResultState(returnValue);
               
                if (state == null || state.Value)
                {
                    var dbRes = uow.Complete();
                    if (dbRes != null && !dbRes.State)
                    {
                        if (state.HasValue)
                        {
                            List<object> errList = new List<object>();
                            if (dbRes.ErrorList != null)
                            {
                                foreach (var item in dbRes.ErrorList)
                                {
                                    errList.Add(item.Message);
                                }
                            }
                            invocation.ReturnValue = CreateFailedOperationResult(returnValue.GetType(), "数据库异常", errList);
                            //invocation.ReturnValue = OperationResult.CreateFailedResult("数据库异常", errList);
                            //SetOperationResultMsg(returnValue, "数据库异常", errList);
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
                }
                uow.Dispose();
            }
        }
    }
}
