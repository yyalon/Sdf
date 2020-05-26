using Nelibur.ObjectMapper;
using Nelibur.ObjectMapper.Bindings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Sdf.Common;

namespace Sdf.Mapper
{
    public class MapperManager
    {
        //private static Dictionary<string, Action<object, object>> filterDictionary = new Dictionary<string, Action<object, object>>();
        private static Dictionary<string, object> filterDictionary = new Dictionary<string, object>();
        private static Dictionary<string, object> constructUsingDictionary = new Dictionary<string, object>();
        public static void MapperConfig<TSource, TTarget>(Action<IMapperConfig<TSource, TTarget>> config)
        {
 
            MapperConfig<TSource, TTarget> mapperConfig =new MapperConfig<TSource, TTarget>();
            config?.Invoke(mapperConfig);
            if (!TinyMapper.BindingExists<TSource, TTarget>())
            {
                if(config==null)
                    TinyMapper.Bind<TSource, TTarget>();
                else
                    TinyMapper.Bind<TSource, TTarget>(mapperConfig.CreateTinyMapperConfig());
            }
            if (mapperConfig.GetFilter() != null)
            {
                string filterKey =String.Format("{0}-{1}", typeof(TSource).GetHashCode(), typeof(TTarget).GetHashCode());
                filterDictionary[filterKey] = mapperConfig.GetFilter();
            }
            if (mapperConfig.ConstructUsing() != null)
            {
                string key = String.Format("{0}-{1}", typeof(TSource).GetHashCode(), typeof(TTarget).GetHashCode());
                if(!constructUsingDictionary.Keys.Any(m=>m==key))
                    constructUsingDictionary[key] = mapperConfig.ConstructUsing();
            }
        }
        public static TTarget MapTo<TSource, TTarget>(TSource source, TTarget target = default(TTarget), Action<TSource, TTarget> filter = null)
        {
            if (source == null)
                return default(TTarget);
            string filterKey = String.Format("{0}-{1}", typeof(TSource).GetHashCode(), typeof(TTarget).GetHashCode());

            object constructUsing = null;
            TTarget resTarget = default(TTarget);

            if (target == null && constructUsingDictionary.TryGetValue(filterKey, out constructUsing))
            {
                var constructUsingFunc = constructUsing as Func<TSource, TTarget>;
                resTarget = constructUsingFunc(source);
            }
            else
            {
                resTarget = target;
            }

            if (resTarget == null)
            {
                if (ReflectionHelper.IsHaveNoparameterConstructor(typeof(TTarget)))
                {
                    resTarget = ReflectionHelper.CreateInastanceByPrivateConstructor<TTarget>(typeof(TTarget));
                }
                else
                {
                    throw new Exception($"类型:{typeof(TTarget).ToString()}不含有无参构造函数");
                }
            }
            resTarget = TinyMapper.Map<TSource, TTarget>(source, resTarget);


            filter?.Invoke(source, resTarget);
            
            object gfilter = null;
            if (filterDictionary.TryGetValue(filterKey,out gfilter))
            {
                var filterFunc = gfilter as Action<TSource, TTarget>;
                filterFunc(source, resTarget);
            }
            
            return resTarget;
        }
        //public static TTarget MapTo<TTarget>(IUseMapper source, Action<IUseMapper, TTarget> filter = null)
        //{
        //    if (source == null)
        //        return default(TTarget);
        //    TTarget target = TinyMapper.Map<TTarget>(source);
        //    filter?.Invoke(source, target);
        //    string filterKey = String.Format("{0}-{1}", source.GetType().GetHashCode(), typeof(TTarget).GetHashCode());
        //    object gfilter = null;
        //    if (filterDictionary.TryGetValue(filterKey, out gfilter))
        //    {
        //        var filterFunc = gfilter as Action<IUseMapper, TTarget>;
        //        filterFunc(source, target);
        //    }
        //    return target;
        //}
    }
}
