using Microsoft.AspNetCore.Builder;
using Sdf.Core;
using Sdf.Mapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Modules
{
    public class ModuleBase
    {
        public virtual void Initialize(IRegister register, MapperProvider mapperProvider)
        {

        }
        public virtual void Initialized(IResolver resolve, IApplicationBuilder app)
        {

        }

    }
}
