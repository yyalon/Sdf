using Castle.DynamicProxy;

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
