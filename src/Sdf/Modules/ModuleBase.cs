using Microsoft.AspNetCore.Builder;
using Sdf.Core;

namespace Sdf.Modules
{
    public class ModuleBase
    {
        public virtual void Initialize(IRegister register)
        {

        }
        public virtual void Initialized(IResolver resolve, IApplicationBuilder app)
        {

        }

    }
}
