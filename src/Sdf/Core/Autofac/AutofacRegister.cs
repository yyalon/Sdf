using Autofac;
using Autofac.Builder;
using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Core.Autofac
{
    public class AutofacRegister : IRegister
    {
        private ContainerBuilder _builder;
        private IocManager _iocManager;
        public AutofacRegister(IocManager iocManager)
        {
            _iocManager = iocManager;
            _builder = iocManager.AutofacBuilder;
        }
        /// <summary>
        /// 判断是否要启用类代理
        /// </summary>
        /// <param name="tImpl"></param>
        /// <param name="tService"></param>
        /// <returns></returns>
        private bool IsClassInterceptors(Type tImpl, Type tService = null)
        {
            if (tService == null)
                return tImpl.IsClass;
            return tImpl.IsClass && tService.IsClass;
        }
        

        #region PreWebRequest
        public void RegisterPreWebRequest<TService, TImpl>(string name) where TService : class where TImpl : class, TService
        {
            var registration = _builder.RegisterType<TImpl>().As<TService>().EnableClassInterceptors().InstancePerLifetimeScope();
            if (!String.IsNullOrEmpty(name))
            {
                registration.Named(name, typeof(TImpl));
            }
            Add(registration, IsClassInterceptors(typeof(TImpl), typeof(TService)), typeof(TService), typeof(TImpl), name);
        }
        public void RegisterPreWebRequest<TImpl>(TImpl instance, string name = null) where TImpl : class
        {
            var registration = _builder.RegisterInstance(instance).InstancePerLifetimeScope();
            if (!String.IsNullOrEmpty(name))
            {
                registration.Named(name, typeof(TImpl));
            }
            Add(registration, IsClassInterceptors(typeof(TImpl)), typeof(TImpl), typeof(TImpl), name);
        }
        public void RegisterPreWebRequest(Type serviceType, Type impType, string name = null)
        {
            var registration = _builder.RegisterType(impType).As(serviceType).InstancePerLifetimeScope();
            if (!String.IsNullOrEmpty(name))
            {
                registration.Named(name, impType);
            }
            Add(registration, IsClassInterceptors(impType, serviceType), serviceType, impType, name);
        }
        public void RegisterPreWebRequest(Type impType, string name = null)
        {
            var registration = _builder.RegisterType(impType).InstancePerLifetimeScope();
            if (!String.IsNullOrEmpty(name))
            {
                registration.Named(name, impType);
            }
            Add(registration, IsClassInterceptors(impType), impType, impType, name);
        }

        public void RegisterPreWebRequest<TImpl>(string name = null) where TImpl : class
        {
            var registration = _builder.RegisterType<TImpl>().InstancePerLifetimeScope();
            if (!String.IsNullOrEmpty(name))
            {
                registration.Named(name, typeof(TImpl));
            }
            Add(registration, IsClassInterceptors(typeof(TImpl)), typeof(TImpl), typeof(TImpl), name);
        }

        public void RegisterPreWebRequest<TService, TImpl>(Func<IResolver, TImpl> func, string name = null) where TImpl : class
        {
            var registration = _builder.Register((c, p) =>
            {
                using (var resolver = _iocManager.GetResolver())
                {
                    var tServer = func.Invoke(resolver);
                    return tServer;
                }
            }).As<TService>().InstancePerLifetimeScope();
            if (!String.IsNullOrEmpty(name))
            {
                registration.Named(name, typeof(TImpl));
            }
            Add(registration, IsClassInterceptors(typeof(TImpl), typeof(TService)), typeof(TService), typeof(TImpl), name);
        }
        public void RegisterPreWebRequest<TImpl>(Func<IResolver, TImpl> func, string name = null) where TImpl : class
        {
            var registration = _builder.Register((c, p) =>
            {
                using (var resolver = _iocManager.GetResolver())
                {
                    var tServer = func.Invoke(resolver);
                    return tServer;
                }
            }).InstancePerLifetimeScope();
            if (!String.IsNullOrEmpty(name))
            {
                registration.Named(name, typeof(TImpl));
            }
            Add(registration, IsClassInterceptors(typeof(TImpl)), typeof(TImpl), typeof(TImpl), name);
        }
        public void RegisterGenericPreWebRequest(Type serviceType, Type impType, string name = null)
        {
            var registration = _builder.RegisterGeneric(impType).As(serviceType).InstancePerLifetimeScope();
            if (!String.IsNullOrEmpty(name))
            {
                registration.Named(name, impType);
            }
            Add(registration, IsClassInterceptors(impType, serviceType), serviceType, impType, name);
        }



        #endregion

        #region Singleton
        public void RegisterSingleton<TService, TImpl>(string name) where TService : class where TImpl : class, TService
        {
            var registration = _builder.RegisterType<TImpl>().As<TService>().SingleInstance();
            if (!String.IsNullOrEmpty(name))
            {
                registration.Named(name, typeof(TImpl));
            }
            Add(registration, IsClassInterceptors(typeof(TImpl), typeof(TService)), typeof(TService), typeof(TImpl), name);
        }
        public void RegisterSingleton<TImpl>(string name = null) where TImpl : class
        {
            var registration = _builder.RegisterType<TImpl>().SingleInstance();
            if (!String.IsNullOrEmpty(name))
            {
                registration.Named(name, typeof(TImpl));
            }
            Add(registration, IsClassInterceptors(typeof(TImpl)), typeof(TImpl), typeof(TImpl), name);
        }
        public void RegisterSingleton<TImpl>(TImpl instance, string name = null) where TImpl : class
        {
            var registration = _builder.RegisterInstance(instance).SingleInstance();
            if (!String.IsNullOrEmpty(name))
            {
                registration.Named(name, typeof(TImpl));
            }
            Add(registration, IsClassInterceptors(typeof(TImpl)), typeof(TImpl), typeof(TImpl), name);
        }
        public void RegisterSingleton(Type impType, string name = null)
        {
            var registration = _builder.RegisterInstance(impType).SingleInstance();
            Add(registration, IsClassInterceptors(impType), impType, impType, name);
        }
        public void RegisterSingleton<TService, TImpl>(Func<IResolver, TImpl> func, string name = null) where TImpl : class
        {
            var registration = _builder.Register((c, p) =>
            {
                using (var resolver = _iocManager.GetResolver())
                {
                    var tServer = func.Invoke(resolver);
                    return tServer;
                }
            }).As<TService>().SingleInstance();
            if (!String.IsNullOrEmpty(name))
            {
                registration.Named(name, typeof(TImpl));
            }
            Add(registration, IsClassInterceptors(typeof(TImpl), typeof(TService)), typeof(TService), typeof(TImpl), name);
        }
        public void RegisterSingleton<TImpl>(Func<IResolver, TImpl> func, string name = null) where TImpl : class
        {
            var registration = _builder.Register((c, p) =>
            {
                using (var resolver = _iocManager.GetResolver())
                {
                    var tServer = func.Invoke(resolver);
                    return tServer;
                }
            }).SingleInstance();
            if (!String.IsNullOrEmpty(name))
            {
                registration.Named(name, typeof(TImpl));
            }
            Add(registration, IsClassInterceptors(typeof(TImpl)), typeof(TImpl), typeof(TImpl), name);
        }

        public void RegisterSingleton(Type serviceType, Type impType, string name = null)
        {
            var registration = _builder.RegisterType(impType).As(serviceType).SingleInstance();
            if (!String.IsNullOrEmpty(name))
            {
                registration.Named(name, impType);
            }
            Add(registration, IsClassInterceptors(impType, serviceType), serviceType, impType, name);
        }
        public void RegisterGenericSingleton(Type serviceType, Type impType, string name = null)
        {
            var registration = _builder.RegisterGeneric(impType).As(serviceType).SingleInstance();
            if (!String.IsNullOrEmpty(name))
            {
                registration.Named(name, impType);
            }
            Add(registration, IsClassInterceptors(impType, serviceType), serviceType, impType, name);
        }
        #endregion

        #region Transient
        public void RegisterTransient<TService, TImpl>(string name) where TService : class where TImpl : class, TService
        {
            var registration = _builder.RegisterType<TImpl>().As<TService>().InstancePerDependency();

            Add(registration, IsClassInterceptors(typeof(TImpl), typeof(TService)), typeof(TService), typeof(TImpl), name);
        }
        public void RegisterTransient<TImpl>(string name = null) where TImpl : class
        {
            var registration = _builder.RegisterType<TImpl>().InstancePerDependency();

            if (!String.IsNullOrEmpty(name))
            {
                registration.Named(name, typeof(TImpl));
            }
            Add(registration, IsClassInterceptors(typeof(TImpl)), typeof(TImpl), typeof(TImpl), name);
        }
        public void RegisterTransient<TImpl>(TImpl instance, string name = null) where TImpl : class
        {
            var registration = _builder.RegisterInstance(instance).InstancePerDependency();
            Add(registration, IsClassInterceptors(typeof(TImpl)), typeof(TImpl), typeof(TImpl), name);

        }
        public void RegisterTransient(Type tImpl, string name = null)
        {
            var registration = _builder.RegisterType(tImpl).InstancePerDependency();
            if (!String.IsNullOrEmpty(name))
            {
                registration.Named(name, tImpl);
            }
            Add(registration, IsClassInterceptors(tImpl), tImpl, tImpl, name);

        }
        public void RegisterTransient<TService, TImpl>(Func<IResolver, TImpl> func, string name = null) where TImpl : class
        {
            var registration = _builder.Register((c, p) =>
            {
                using (var resolver = _iocManager.GetResolver())
                {
                    var tServer = func.Invoke(resolver);
                    return tServer;
                }
            }).As<TService>().InstancePerDependency();
            if (!String.IsNullOrEmpty(name))
            {
                registration.Named(name, typeof(TImpl));
            }
            Add(registration, IsClassInterceptors(typeof(TImpl), typeof(TService)), typeof(TService), typeof(TImpl), name);
        }
        public void RegisterTransient<TImpl>(Func<IResolver, TImpl> func, string name = null) where TImpl : class
        {
            var registration = _builder.Register((c, p) =>
            {
                using (var resolver = _iocManager.GetResolver())
                {
                    var tServer = func.Invoke(resolver);
                    return tServer;
                }
            }).InstancePerDependency();
            if (!String.IsNullOrEmpty(name))
            {
                registration.Named(name, typeof(TImpl));
            }
            Add(registration, IsClassInterceptors(typeof(TImpl)), typeof(TImpl), typeof(TImpl), name);
        }

        public void RegisterTransient(Type serviceType, Type impType, string name = null)
        {
            var registration = _builder.RegisterType(impType).As(serviceType).InstancePerDependency();
            if (!String.IsNullOrEmpty(name))
            {
                registration.Named(name, impType);
            }
            Add(registration, IsClassInterceptors(impType, serviceType), serviceType, impType, name);
        }
        public void RegisterGenericTransient(Type serviceType, Type impType, string name = null)
        {
            var registration = _builder.RegisterGeneric(impType).As(serviceType).InstancePerDependency();
            if (!String.IsNullOrEmpty(name))
            {
                registration.Named(name, impType);
            }
            Add(registration, IsClassInterceptors(impType, serviceType), serviceType, impType, name);
        }
        #endregion


        public void Add<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle>(IRegistrationBuilder<TLimit, TConcreteReflectionActivatorData, TRegistrationStyle> registration, bool isCanClassInterceptors, Type tServer, Type tImp, string name)
        {

            _iocManager.RegistrationBuilders.Add(new RegistrationStore() { IsCanClassInterceptors = isCanClassInterceptors, Registration = registration, Name = name, TServer = tServer, TImp = tImp });
        }

        public bool IsRegisted<TService>(string name = null) where TService : class
        {
           return _iocManager.IsRegisted<TService>(name);
        }
    }
}
