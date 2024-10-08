using BLUEY.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace BLUEY.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(ILogger<BaseController> logger) : base(logger)
        {

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogInformation("esta é uma pagina privada");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
