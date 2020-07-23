using AutoMapper;

namespace Business.Services
{
    public class BaseService
    {
        protected IMapper Mapper;

        public BaseService(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}