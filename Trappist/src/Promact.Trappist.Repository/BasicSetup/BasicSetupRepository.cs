using Microsoft.AspNetCore.Identity;
using Promact.Trappist.Web.Models;
using System.Threading.Tasks;
using System;
using System.Data.SqlClient;
using Promact.Trappist.DomainModel.ApplicationClasses;
using Promact.Trappist.Utility.EmailServices;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Promact.Trappist.Repository.BasicSetup
{
    public class BasicSetupRepository : IBasicSetupRepository
    {
        #region Private Variables
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        #endregion
        #region Constructor
        public BasicSetupRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }
        #endregion
        #region IBasicSetupRepository methods
        //Method for register user
        public async Task<bool> RegisterUser(DomainModel.ApplicationClasses.BasicSetup.BasicSetup model)
        {
            try
            {
                var user = new ApplicationUser();
                user.UserName = model.Name;
                user.Email = model.Email;
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Method for validate conncetion string connection string
        public async Task<bool> ValidateConnectionString(ConnectionStringParamters model)
        {
            var builder = new SqlConnectionStringBuilder(model.ConnectionString);
            using (var conn = new SqlConnection(GetConnectionString(builder)))
            {
                try
                {
                    conn.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        }
        private string GetConnectionString(SqlConnectionStringBuilder connectionString)
        {
            if (connectionString.IntegratedSecurity)
            {
                return string.Format("Data Source={0};Trusted_Connection={1}", connectionString.DataSource, connectionString.IntegratedSecurity);
            }
            else
            {
                return string.Format("Data Source={0};User Id={1};Password={2}", connectionString.DataSource, connectionString.UserID, connectionString.Password);
            }

        }
        //Mehod for validate email settings and for sending mail
        public async Task<EmailResponse> ValidateEmailSetting(EmailSettings model)
        {
            return await _emailService.SendMail(model);
        }
        public async Task<bool> SaveSetupParameter()
        {
            using (FileStream filestream = File.Create(@"D:\test.json"))
            {
            }
            return true;
        }
        #endregion
    }
}
