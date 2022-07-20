using AutoMapper;

namespace Sdf.Mapper
{
    public class MapperManager
    {

        private static IMapper _mapper;
        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    using var resolver = Bootstrapper.Instance.IocManager.GetResolver();
                    _mapper = resolver.Resolve<IMapper>();
                }

                return _mapper;
            }
            
        }
    }
}
