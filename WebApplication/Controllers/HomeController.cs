using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private const string DefaultExceptionMessage = "There is no error";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var handler = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exceptionMessage = DefaultExceptionMessage;
            if (handler != null)
            {
                exceptionMessage = handler.Error.Message;
            }
            
            return View(new ErrorViewModel
            {
                RequestId = HttpContext.TraceIdentifier,
                Message = exceptionMessage
            });
        }
    }
}