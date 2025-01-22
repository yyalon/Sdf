using Microsoft.Extensions.DependencyInjection;
using Sdf.Core;

namespace Sdf.Mapper
{
    public static class SdfConfigManagerExtensions
    {
        
        public static SdfConfigManager UseAutoMapper(this SdfConfigManager sdfConfig)
        {
            sdfConfig.Services.AddAutoMapper(Bootstrapper.Instance.ModuleManager.ModuleAssemblyList);

            return sdfConfig;
        }
       
    }
}
