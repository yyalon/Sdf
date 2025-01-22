using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Core.Autofac
{
    public class AutofacResolver: IResolver
    {
        private ILifetimeScope _scope;

        public AutofacResolver(ILifetimeScope scope)
        {
            _scope = scope;
        }
        public void Dispose()
        {
            _scope.Dispose();
        }

        public T Resolve<T>(string name = null)
        {
            if (String.IsNullOrEmpty(name))
            {
                return _scope.Resolve<T>();

            }
            else
            {
                return _scope.ResolveNamed<T>(name);
            }
        }

        public object Resolve(Type serviceType, string name = null)
        {
            if (String.IsNullOrEmpty(name))
            {
                return _scope.Resolve(serviceType);

            }
            else
            {
                return _scope.ResolveNamed(name, serviceType);
            }
        }
    }
}
