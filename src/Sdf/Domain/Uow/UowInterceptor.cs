using Castle.DynamicProxy;
using Sdf.Application;
using Sdf.Common;
using Sdf.Domain.Db;
using Sdf.Fundamentals.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sdf.Domain.Uow
{
    public class UowInterceptor:IInterceptor
    {
        private readonly UowInterceptorAsync _uowInterceptorAsync;
        public UowInterceptor(UowInterceptorAsync uowInterceptorAsync)
        {
            _uowInterceptorAsync = uowInterceptorAsync;
        }
       
       
        public void Intercept(IInvocation invocation)
        {
            _uowInterceptorAsync.ToInterceptor().Intercept(invocation);
        }
    }
}
