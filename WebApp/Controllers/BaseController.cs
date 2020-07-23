using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IMapper Mapper;

        public BaseController(IMapper mapper)
        {
            Mapper = mapper;
        }
    }
}