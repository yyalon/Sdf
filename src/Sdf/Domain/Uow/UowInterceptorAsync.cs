using Castle.DynamicProxy;
using Sdf.Application;
using Sdf.Domain.Db;
using Sdf.Fundamentals.Logs;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Sdf.Domain.Uow
{
    public class UowInterceptorAsync : IAsyncInterceptor
    {
        private readonly ILog _log;
        private readonly IUowManager _uowManager;

        public UowInterceptorAsync(IUowManager uowManager, ILog log)
        {
            _uowManager = uowManager;
            _log = log;
        }

        public void InterceptAsynchronous(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous(invocation);
        }

        public void InterceptAsynchronous<TResult>(IInvocation invocation)
        {
            invocation.ReturnValue = InternalInterceptAsynchronous<TResult>(invocation);
        }

        public void InterceptSynchronous(IInvocation invocation)
        {
            string methodName = "";
            object[] methodArguments = Array.Empty<object>();

            try
            {
                methodName = invocation.GetConcreteMethod().Name;
                methodArguments = invocation.Arguments;
                invocation.Proceed();
            }
            catch (Exception e)
            {
                _log.LogTrace(e, methodName, methodArguments);
                throw;
            }
        }

        private async Task InternalInterceptAsynchronous(IInvocation invocation)
        {
            string methodName = "";
            object[] methodArguments = Array.Empty<object>();
            try
            {
                var uow = _uowManager.Begin();
                methodName = invocation.GetConcreteMethod().Name;
                methodArguments = invocation.Arguments;

                invocation.Proceed();

                var task = (Task)invocation.ReturnValue;
                await task;

                if (uow != null)
                {
                    await uow.CompleteAsync();
                    uow.Dispose();
                }
            }
            catch (Exception e)
            {
                _log.LogTrace(e,methodName,methodArguments);
                throw;
            }
        }

        private async Task<TResult> InternalInterceptAsynchronous<TResult>(IInvocation invocation)
        {
            string methodName = "";
            object[] methodArguments = Array.Empty<object>();

            try
            {
                var uow = _uowManager.Begin();
                methodName = invocation.GetConcreteMethod().Name;
                methodArguments = invocation.Arguments;

                invocation.Proceed();

                var task = (Task<TResult>)invocation.ReturnValue;
                TResult returnValue = await task;

                if (uow != null)
                {
                    bool? state = ParseOperationResultState(returnValue);

                    if (state == null || state.Value)
                    {
                        var dbRes =await uow.CompleteAsync();
                        if (dbRes != null && !dbRes.State)
                        {
                            if (state.HasValue)
                            {
                                returnValue = (TResult)CreateFailedOperationResult(returnValue.GetType(), "数据库异常");
                            }
                        }
                        while (!_uowManager.CompletedHandles.IsEmpty)
                        {
                            if (_uowManager.CompletedHandles.TryDequeue(out Action<DbChangeResult> handle))
                            {
                                handle.Invoke(dbRes);
                            }
                        }
                    }
                    uow.Dispose();
                }

                return returnValue;
            }
            catch (Exception e)
            {
                _log.LogTrace(e, methodName, methodArguments);
                throw;
            }
        }

        private static bool IsOperationResult(Type objectType)
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
        private static bool? ParseOperationResultState(object obj)
        {
            if (obj != null && IsOperationResult(obj.GetType()))
            {
                var pState = obj.GetType().GetProperties().Where(m => m.Name == nameof(OperationResult.Success)).FirstOrDefault();
                var stateValue = (bool)pState.GetValue(obj);
                return stateValue;
            }
            return null;
        }
        private static object CreateFailedOperationResult(Type type, string msg)
        {
            var obj = Activator.CreateInstance(type);
            if (obj != null)
            {
                var operationResult = obj as OperationResult;
                operationResult.CreateDefaultFailedResult(msg, null);
            }
            return obj;
        }
    }
}
