using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult ErrorView(string errorMessage, string requestId)
        {
            return View("Error", new ErrorViewModel
            {
                Message = errorMessage,
                RequestId = requestId
            });
        }
    }
}