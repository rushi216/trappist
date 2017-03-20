using System.Threading.Tasks;
using Promact.Trappist.DomainModel.ApplicationClasses.Account;
using Microsoft.AspNetCore.Identity;
using Promact.Trappist.Web.Models;
using Promact.Trappist.DomainModel.DbContext;
using System;

namespace Promact.Trappist.Repository.Account
{
    public class AccountRepository:IAccountRepository
    {
        private readonly TrappistDbContext _dbcontext;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountRepository(SignInManager<ApplicationUser> signInManager, TrappistDbContext dbcontext, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _dbcontext = dbcontext;
            _userManager = userManager;
   
        }
        /// <summary>
        /// this method is used to validate user credentials
        /// </summary>
        /// <param name="loginModel">object of Login</param>
        /// <returns>true if matched</returns>
        public async Task<bool> SignIn(Login loginModel)
        {
            var result = await _signInManager.PasswordSignInAsync(loginModel.Email, loginModel.Password, isPersistent: true, lockoutOnFailure: true);
            return result.Succeeded;
        }
        /// <summary>
        /// this method is used to update the forgotten old password with new password
        /// </summary>
        /// <param name="resetPassowrdModel"></param>
        /// <returns>true if password successfully updated</returns>
        public async Task<bool> ResetPassowrd(ResetPassword resetPassowrdModel)
        {
            ApplicationUser user = new ApplicationUser();
            var result = await _userManager.ResetPasswordAsync(user,resetPassowrdModel.Code,resetPassowrdModel.Password);
            return result.Succeeded;
        }
    }
}