using Microsoft.Extensions.DependencyInjection;
using System;

namespace Sdf.Extensions
{
    public class SdfServiceProviderFactory : IServiceProviderFactory<SdfServiceContainer>
    {
        public SdfServiceContainer CreateBuilder(IServiceCollection services)
        {
            return new SdfServiceContainer() { Services = services };
        }

        public IServiceProvider CreateServiceProvider(SdfServiceContainer containerBuilder)
        {
            return containerBuilder.serviceProvider;
        }
    }
}
