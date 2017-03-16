using Microsoft.AspNetCore.Identity;
using Promact.Trappist.Web.Models;
using System.Threading.Tasks;
using System;
using System.Data.SqlClient;
using Promact.Trappist.DomainModel.ApplicationClasses;
using Promact.Trappist.Utility.EmailServices;
using Microsoft.Extensions.Configuration;
using System.IO;
using Promact.Trappist.DomainModel.ApplicationClasses.BasicSetup;

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
        public async Task<ServiceResponse> RegisterUser(DomainModel.ApplicationClasses.BasicSetup.BasicSetup model)
        {
            var response = new ServiceResponse();
            try
            {
                var user = new ApplicationUser();
                user.Name = model.Name;
                user.UserName = model.Email;
                user.Email = model.Email;
                user.CreateDateTime = DateTime.Now;
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    response.Response = true;
                    return response;
                }
                else
                {
                    response.Response = false;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.Response = false;
                return response;

            }
        }

        //Method for validate conncetion string connection string
        public async Task<ServiceResponse> ValidateConnectionString(ConnectionStringParamters model)
        {
            var response = new ServiceResponse();
            try
            {
                model.ConnectionString = model.ConnectionString.Replace("\\\\", "\\");
                model.ConnectionString = model.ConnectionString.Replace("\"", "");
                var builder = new SqlConnectionStringBuilder(model.ConnectionString);
                using (var conn = new SqlConnection(GetConnectionString(builder)))
                {
                    try
                    {
                        conn.Open();
                        response.Response = true;
                        return response;
                    }
                    catch (Exception ex)
                    {
                        response.Response = false;
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                response.Response = false;
                return response;
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
