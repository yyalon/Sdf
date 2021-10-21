using Sdf.Core;
using Sdf.Fundamentals;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Domain.Entities
{
    public static class SdfConfigManagerExtensions
    {
        /// <summary>
        /// 实体自动添加当前时间和更新时间，是否用UTC时间
        /// </summary>
        /// <param name="sdfConfig"></param>
        /// <returns></returns>
        //public static SdfConfigManager UseUtcTime(this SdfConfigManager sdfConfig)
        //{
        //    sdfConfig.Register.RegisterSingleton<IDateTimeProvider>(new DateTimeProvider(true));
        //    return sdfConfig;
        //}
    }
}
