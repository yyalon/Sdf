﻿namespace Sdf.Mapper
{
    public static class ConventionsTinyMapperExtensions
    {
        public static TTarget MapTo<TSource, TTarget>(this TSource source) where TSource:IUseMapper
        {
            return MapperManager.Mapper.Map<TTarget>(source);
        }

        public static TTarget MapTo<TSource, TTarget>(this TSource source, TTarget destination) where TSource : IUseMapper
        {
            return MapperManager.Mapper.Map<TSource,TTarget>(source, destination);
        }
    }
}
