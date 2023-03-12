using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace SBAT.Web.Controllers 
{
    public class SBATBaseController<T> : ControllerBase
    {
        private ILogger<T>? _logger;
        private IMapper? _mapper;

        protected ILogger<T> Logger 
            => _logger ??= HttpContext.RequestServices.GetRequiredService<ILogger<T>>();
        protected IMapper Mapper 
            => _mapper ??= HttpContext.RequestServices.GetRequiredService<IMapper>();
    }
}