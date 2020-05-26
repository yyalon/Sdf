﻿using Sdf.Core;
using Sdf.Domain.Db;
using Sdf.Domain.Uow;
using Sdf.EF.Uow;
using Sdf.Fundamentals.Cache;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.EF
{
    public static class SdfConfigManagerExtensions
    {
        public static SdfConfigManager UseEF(this SdfConfigManager sdfConfig)
        {
            sdfConfig.Register.RegisterTransient<IDbContext, EFDbContext>();
            sdfConfig.Register.RegisterTransient<IUnitOfWork, EfUnitOfWork>();
            return sdfConfig;
        }
    }
    
}
