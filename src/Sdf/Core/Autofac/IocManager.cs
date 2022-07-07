using Autofac;
using Autofac.Extras.DynamicProxy;
using Sdf.Domain.Application;
using Sdf.Domain.Db;
using Sdf.Domain.Uow;
using Sdf.Fundamentals;
using Sdf.IdProvider;
using Sdf.IdProvider.Default;
using Sdf.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sdf.Core.Autofac
{
    public class IocManager
    {
        internal ContainerBuilder AutofacBuilder;
        public IContainer Container { get; private set; }
        internal List<RegistrationStore> RegistrationBuilders;

        public IocManager()
        {
            AutofacBuilder = new ContainerBuilder();
            RegistrationBuilders = new List<RegistrationStore>();
        }

        public void Dispose()
        {
            Container.Dispose();
            RegistrationBuilders = null;
        }

        public IResolver GetResolver()
        {
            var scope = Container.BeginLifetimeScope();
            return new AutofacResolver(scope);
        }

        public void RegisteCompleted()
        {
            UnitOfWorkRegistrar.Initialize(RegistrationBuilders);
            Container = AutofacBuilder.Build();
        }

        public void RegisteCore()
        {
            AutofacBuilder.RegisterInstance(this).SingleInstance();
            AutofacBuilder.RegisterType<AutofacRegister>().As<IRegister>().SingleInstance();
            AutofacBuilder.RegisterType<UowManager>().As<IUowManager>().InstancePerLifetimeScope();
            AutofacBuilder.RegisterType<UowInterceptorAsync>();
            AutofacBuilder.RegisterType<UowInterceptor>().InstancePerDependency();
            AutofacBuilder.RegisterType<ModuleManager>().SingleInstance();
            AutofacBuilder.RegisterType<DefaultLongIdProvider>().As<ILongIdProvider>();
            AutofacBuilder.RegisterType<DefaultGuidIdProvider>().As<IGuidIdProvider>();
            if (!IsRegisted<IDbChangeEventHandler>())
            {
                AutofacBuilder.RegisterType<DefaultDbChangeEventHandler>().As<IDbChangeEventHandler>().SingleInstance();
            }
            //if (!IsRegisted<IDateTimeProvider>())
            //{
            //    AutofacBuilder.RegisterInstance(new DateTimeProvider()).As<IDateTimeProvider>().SingleInstance();
            //}
            RegisteApplicationService();
        }
        internal bool IsRegisted<TService>(string name = null) where TService : class
        {
            if (String.IsNullOrEmpty(name))
            {
                return RegistrationBuilders.Any(m => m.TServer.FullName.ToLower() == typeof(TService).FullName.ToLower());
            }
            else
            {
                return RegistrationBuilders.Any(m => m.TServer.FullName.ToLower() == typeof(TService).FullName.ToLower() && m.Name == name);
            }
        }
        private void RegisteApplicationService()
        {
            //注册所有Application
            var assemblys = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var dataAccess in assemblys)
            {
                var registions = AutofacBuilder.RegisterAssemblyTypes(dataAccess)
                       .Where(t => typeof(IApplicationService).IsAssignableFrom(t))
                       .AsImplementedInterfaces().EnableInterfaceInterceptors().InterceptedBy(typeof(UowInterceptor));
            }
        }
    }
}
