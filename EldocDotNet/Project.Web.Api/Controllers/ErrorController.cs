using Microsoft.AspNetCore.Mvc;

namespace Project.Web.Api.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NotFoundPage()
        {
            return File("/404.html", "text/html");
        }
    }
}
