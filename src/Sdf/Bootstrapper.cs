using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Sdf.Core;
using Sdf.Core.Autofac;
using Sdf.Modules;
using System;

namespace Sdf
{
    public class Bootstrapper
    {
        private readonly static object lockObject = new object();
        private static Bootstrapper instance;
        public static Bootstrapper Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new Bootstrapper();
                        }
                    }
                }
                return instance;
            }
        }
        public SdfConfigManager configManager;
        public IocManager IocManager;
        public ModuleManager ModuleManager { get; }
        private Bootstrapper()
        {
            IocManager = new IocManager();
            ModuleManager = new ModuleManager(new AutofacRegister(IocManager));
        }
       
        public void Initialize(Action<SdfConfigManager> action)
        {
            configManager = new SdfConfigManager(new AutofacRegister(IocManager));
            action(configManager);
            if (ModuleManager.ModuleList.Count == 0)
                throw new Exception("最少注册一个模块");
            ModuleManager.LoadModule();
            ModuleManager.Initialize();
            IocManager.RegisteCore();
            IocManager.RegisteCompleted();

        }
        public IServiceProvider Initialize(IServiceCollection services, Action<SdfConfigManager> action)
        {
            configManager = new SdfConfigManager(new AutofacRegister(IocManager), services);
            action(configManager);

            //if (ModuleManager.ModuleList.Count == 0)
            //    throw new Exception("最少注册一个模块");
            ModuleManager.LoadModule();
            ModuleManager.Initialize();
            IocManager.AutofacBuilder.Populate(services);
            IocManager.RegisteCore();
            IocManager.RegisteCompleted();
            return new AutofacServiceProvider(IocManager.Container);
        }
    }
}
