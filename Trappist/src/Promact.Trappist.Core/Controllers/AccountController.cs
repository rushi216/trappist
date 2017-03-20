using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Promact.Trappist.DomainModel.ApplicationClasses.Account;
using Promact.Trappist.Repository.Account;
using Promact.Trappist.Utility.Constants;
using Promact.Trappist.Utility.EmailServices;
using Promact.Trappist.Web.Controllers;
using Promact.Trappist.Web.Models;
using System.Threading.Tasks;

namespace Promact.Trappist.Core.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IStringConstants _stringConstant;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailServices _emailServices;
        public AccountController(IAccountRepository accountRepository, IStringConstants stringConstant,UserManager<ApplicationUser> userManager,IEmailServices emailServices)
        {
            _accountRepository = accountRepository;
            _stringConstant = stringConstant;
            _userManager = userManager;
            _emailServices = emailServices;
        }
        /// <summary>
        /// this method is used to see the view of login
        /// </summary>
        /// <returns>Login form view</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
        /// <summary>
        ///  This method will be called with credentials to validate user
        /// </summary>
        /// <returns>succesful login-checking for user and redirect to testdashboard page</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Login loginModel, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                if (await _accountRepository.SignIn(loginModel))
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ViewBag.Error = _stringConstant.InavalidLoginError;
                    return View(loginModel);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, _stringConstant.InavalidModelError);
                return View(loginModel);
            }
        }
        /// <summary>
        /// this method is used to redirect to any local url link
        /// </summary>
        /// <param name="returnUrl">string type of url</param>
        /// <returns></returns>
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }
        /// <summary>
        /// this method is used to see the view of forgot password
        /// </summary>
        /// <returns>forgot password form view</returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {         
            return View();
        }
        /// <summary>
        /// this method will be called to validate the given email and send a link to the perticular email id to reset the password
        /// </summary>
        /// <param name="forgotPasswordModel"></param>
        /// <returns>redirect to forgot password conformation view</returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPassword forgotPasswordModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
                if (user == null)
                {
                    return View("ForgotPassword");
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: HttpContext.Request.Scheme);
                await _emailServices.SendEmailAsync(forgotPasswordModel.Email, "Reset Password",
                $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");
                return View("ForgotPasswordConfirmation");
            }
            return View(forgotPasswordModel);
        }
        /// <summary>
        /// this method is used to display confirmation message after sending email to reset password
        /// </summary>
        /// <returns>message on successful mail send</returns>
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        /// <summary>
        /// this method is called when link is clicked through mail
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }
        /// <summary>
        /// this method is used to take the new password and update with the existing password 
        /// </summary>
        /// <param name="resetPasswordModel"></param>
        /// <returns>redirect to reset password confirmation page</returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPasswordModel)
        {
            if (!ModelState.IsValid)
            {
                if (await _accountRepository.ResetPassowrd(resetPasswordModel))
                {
                    return RedirectToAction(nameof(AccountController.ResetPasswordConfirmation), "Account");
                }
                else
                {
                    return View(resetPasswordModel);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, _stringConstant.InavalidModelError);
                return View(resetPasswordModel);
            }
        }
        /// <summary>
        /// this method is used to return the reset password confirmation message page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
