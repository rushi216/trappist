using Microsoft.AspNetCore.Mvc;
using Promact.Trappist.Repository.BasicSetup;

namespace Promact.Trappist.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Private variables
        #region Dependencies
        private readonly IBasicSetupRepository _basicSetup;
        #endregion
        #endregion

        #region Constructor
        public HomeController(IBasicSetupRepository basicSetup)
        {
            _basicSetup = basicSetup;
        }
        #endregion

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
            if (_basicSetup.FileExist())
            {
                return RedirectToAction("Login", "Account");
            }
            else
            {
                return View();
            }
        }

    }
}
