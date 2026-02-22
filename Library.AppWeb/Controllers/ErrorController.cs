using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Library.AppWeb.Controllers
{
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("/Error/NotFound")]
        public IActionResult NotFoundPage()
        {
            Response.StatusCode = 404;
            return View("NotFound");
        }

        [Route("/Error/ServerError")]
        public IActionResult ServerError()
        {
            var exception =
                HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exception != null)
            {
                _logger.LogError(exception.Error,
                    "An unexpected error occurred. Our team has been notified. Path: {path}",
                    exception.Path
                );
            }

            ViewBag.RequestId = HttpContext.TraceIdentifier;

            Response.StatusCode = 500;
            return View("ServerError");
        }
    }
}
