using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Core
{
    public class SdfConfigManager
    {
        public IRegister Register { get; private set; }
        public IServiceCollection Services { get; private set; }
        public SdfConfigManager(IRegister register, IServiceCollection services)
        {
            Register = register;
            Services = services;
        }
        public SdfConfigManager(IRegister register)
        {
            Register = register;
        }
        public SdfConfigManager RegisterModules(params Type[] modules)
        {
            foreach (var item in modules)
            {
                Bootstrapper.Instance.ModuleManager.RegisterModule(item);
            }
            return this;
        }
    }
}
