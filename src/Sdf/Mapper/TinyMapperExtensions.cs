using Nelibur.ObjectMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Mapper
{
    public static class ConventionsTinyMapperExtensions
    {
        public static TTarget MapTo<TSource, TTarget>(this TSource source, TTarget target = default(TTarget), Action<TSource, TTarget> filter = null) where TSource:IUseMapper
        {
            return MapperManager.MapTo<TSource, TTarget>(source, target, filter);
        }
        //public static TTarget MapTo<TTarget>(this IUseMapper source, Action<IUseMapper, TTarget> filter = null)
        //{
        //    return MapperManager.MapTo<,TTarget>(source, filter);
        //}
    }
}
