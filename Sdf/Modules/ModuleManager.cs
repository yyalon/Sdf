using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sdf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Sdf.Modules
{
    public class ModuleManager
    {
        private readonly IRegister _register;
        public ModuleManager(IRegister register)
        {
            _register = register;
            ModuleAssemblyList = new List<Assembly>();
            ModuleList = new List<Type>();
            ModuleInstanceList = new List<ModuleBase>();
        }
        internal List<Assembly> ModuleAssemblyList { get; }
        internal List<Type> ModuleList { get; }
        private List<ModuleBase> ModuleInstanceList { get; }
        internal void RegisterModule(Type module)
        {
            if (!ModuleHelper.IsModule(module, 0))
                throw new Exception($"type {module.FullName} is not ModuleBase");
            if (!ModuleList.Any(m => m == module))
                ModuleList.Add(module);
            var moduleAssesmbly = module.Assembly;
            if (!ModuleAssemblyList.Any(m => m.FullName == moduleAssesmbly.FullName))
            {
                ModuleAssemblyList.Add(moduleAssesmbly);
            }

        }
        internal void LoadModule(IServiceCollection services)
        {
            var moduleList = new List<object>();
            foreach (var assembly in ModuleAssemblyList)
            {
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (IsModule(type, 0))
                    {
                        moduleList.Add(assembly.CreateInstance(type.FullName));
                    }
                }

            }
            moduleList = moduleList.Distinct().ToList();
            foreach (var moduleItem in moduleList)
            {
                var module = (ModuleBase)moduleItem;
                module.SetServiceCollection(services);
                ModuleInstanceList.Add(module);
            }
        }
        internal void Initialize()
        {
            foreach (var module in ModuleInstanceList)
            {
                module.Initialize(_register);
            }
        }
       
        internal void Initialized(IResolver resolver, IApplicationBuilder app)
        {
            foreach (var module in ModuleInstanceList)
            {
                module.Initialized(resolver, app);
            }
        }
        private bool IsModule(Type type, int layer)
        {
            if (layer == 4)
                return false;
            if (type == null)
                return false;
            if (type.BaseType == null)
                return false;
            if (type.BaseType.FullName == typeof(ModuleBase).FullName)
            {
                return true;
            }
            else if (type.BaseType.Equals(typeof(Object)))
            {
                return false;
            }
            else
            {
                return IsModule(type.BaseType.GetType(), layer + 1);
            }
        }
    }
}
