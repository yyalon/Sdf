using Microsoft.AspNetCore.Builder;

namespace Sdf.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseDevFramework(this IApplicationBuilder app)
        {
            using (var resolver = Bootstrapper.Instance.IocManager.GetResolver())
            {
                Bootstrapper.Instance.ModuleManager.Initialized(resolver, app);
            }
        }
    }
}
