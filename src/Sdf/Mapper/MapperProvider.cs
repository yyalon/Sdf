using System;
using System.Collections.Generic;
using System.Text;

namespace Sdf.Mapper
{
    public class MapperProvider
    {
        public  void MapperConfig<TSource, TTarget>(Action<IMapperConfig<TSource, TTarget>> config=null)
        {
            MapperManager.MapperConfig<TSource, TTarget>(config);
        }
    }
}
