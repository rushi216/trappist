using Microsoft.AspNetCore.Identity;
using Promact.Trappist.Web.Models;
using System.Threading.Tasks;
using System;
using System.Data.SqlClient;
using Promact.Trappist.Utility.EmailServices;
using System.IO;
using Promact.Trappist.DomainModel.ApplicationClasses.BasicSetup;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Promact.Trappist.DomainModel.DbContext;
using Promact.Trappist.Utility.Constants;

namespace Promact.Trappist.Repository.BasicSetup
{
    public class BasicSetupRepository : IBasicSetupRepository
    {
        #region Private Variables
        #region Dependencies
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;
        private readonly IHostingEnvironment _environment;
        private readonly TrappistDbContext _trappistDbContext;
        private readonly IStringConstants _stringConstants;
        #endregion
        #endregion

        #region Constructor
        public BasicSetupRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailService emailService, IHostingEnvironment environment, TrappistDbContext trappistDbContext, IStringConstants stringConstants)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _environment = environment;
            _trappistDbContext = trappistDbContext;
            _stringConstants = stringConstants;
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
                user.Name = model.RegistrationFields.Name;
                user.UserName = model.RegistrationFields.Email;
                user.Email = model.RegistrationFields.Email;
                user.CreateDateTime = DateTime.Now;
                var res = new ServiceResponse();
                res = SaveSetupParameter(model);
                if (res.Response == true)
                {
                    _trappistDbContext.Database.EnsureCreated();
                    var result = await _userManager.CreateAsync(user, model.RegistrationFields.Password);
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
                return response;
            }
            catch (Exception)
            {
                response.Response = false;
                return response;
            }
        }

        //Method for validate conncetion string
        public ServiceResponse ValidateConnectionString(DomainModel.ApplicationClasses.BasicSetup.BasicSetup model)
        {
            var response = new ServiceResponse();
            try
            {
                model.ConnectionStringParameters.ConnectionString = model.ConnectionStringParameters.ConnectionString.Replace("\\\\", "\\");
                model.ConnectionStringParameters.ConnectionString = model.ConnectionStringParameters.ConnectionString.Replace("\"", "");
                var builder = new SqlConnectionStringBuilder(model.ConnectionStringParameters.ConnectionString);
                using (var conn = new SqlConnection(GetConnectionString(builder)))
                {
                    try
                    {
                        conn.Open();
                        response.Response = true;
                        return response;
                    }
                    catch (Exception)
                    {
                        response.Response = false;
                        return response;
                    }
                }
            }
            catch (Exception)
            {
                response.Response = false;
                return response;
            }
        }

        //Method for build connection string without database.(For validate connection string purpose)
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
        public bool ValidateEmailSetting(DomainModel.ApplicationClasses.BasicSetup.BasicSetup model)
        {
            return _emailService.SendMail(model.EmailSettings.UserName, model.EmailSettings.Password, model.EmailSettings.Server, model.EmailSettings.Port, "", "");
        }

        //Method for save setup parameter in SetupConfig.json file
        private ServiceResponse SaveSetupParameter(DomainModel.ApplicationClasses.BasicSetup.BasicSetup model)
        {
            var response = new ServiceResponse();
            model.ConnectionStringParameters.ConnectionString = model.ConnectionStringParameters.ConnectionString.Replace("\\\\", "\\");
            model.ConnectionStringParameters.ConnectionString = model.ConnectionStringParameters.ConnectionString.Replace("\"", "");
            var tempModel = new SetupConfig();
            tempModel.ConnectionStringParameters = model.ConnectionStringParameters;
            tempModel.EmailSettings = model.EmailSettings;
            string path = Path.Combine(_environment.ContentRootPath.ToString(), _stringConstants.ConfigFolderName, _stringConstants.SetupConfigFileName);
            string jsonData = JsonConvert.SerializeObject(tempModel, Formatting.Indented);
            if (!File.Exists(path))
            {
                File.Create(path).Dispose();
            }
            File.AppendAllText(path, jsonData);
            response.Response = true;
            return response;
        }

        //Method for checking file exist or not
        public bool FileExist()
        {
            if (File.Exists(Path.Combine(_environment.ContentRootPath.ToString(), _stringConstants.ConfigFolderName, _stringConstants.SetupConfigFileName)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion
    }
}
