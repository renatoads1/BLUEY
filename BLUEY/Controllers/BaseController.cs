using Microsoft.AspNetCore.Mvc;

namespace BLUEY.Controllers
{
    public class BaseController : Controller
    {
        protected readonly ILogger<BaseController> _logger;

        public BaseController(ILogger<BaseController> logger)
        {
            _logger = logger;
        }

    }
}
