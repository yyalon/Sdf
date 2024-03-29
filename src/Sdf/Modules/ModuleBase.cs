using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Sdf.Core;

namespace Sdf.Modules
{
    public class ModuleBase
    {
        public IServiceCollection Services { get;private set; }

        internal void SetServiceCollection(IServiceCollection services)
        {
            Services = services;
        }

        public virtual void Initialize(IRegister register)
        {

        }
        public virtual void Initialized(IResolver resolve, IApplicationBuilder app)
        {
            
        }

    }
}
