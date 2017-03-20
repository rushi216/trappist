using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Promact.Trappist.Repository.BasicSetup;

namespace Promact.Trappist.Web.Controllers
{
    public class HomeController : Controller
    {
        readonly ILogger<HomeController> _logger;
        readonly IHostingEnvironment _env;
        private readonly IBasicSetupRepository _basicSetup;
        public HomeController(ILogger<HomeController> logger, IHostingEnvironment env,IBasicSetupRepository basicSetup)
        {
            _logger = logger;
            _env = env;
            _basicSetup = basicSetup;
        }

        public IActionResult Index()
        {
            if (_basicSetup.FileExist())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Setup", "Home");
            }
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Setup(string returnUrl = null)
        {
            if (!_basicSetup.FileExist())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login","Account");
            }
        }

    }
}
