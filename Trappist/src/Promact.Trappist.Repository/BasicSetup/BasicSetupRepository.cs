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
        private readonly IEmailService _emailService;
        private readonly IHostingEnvironment _environment;
        private readonly TrappistDbContext _trappistDbContext;
        private readonly IStringConstants _stringConstants;
        #endregion
        #endregion

        #region Constructor
        public BasicSetupRepository(UserManager<ApplicationUser> userManager, IEmailService emailService, IHostingEnvironment environment, TrappistDbContext trappistDbContext, IStringConstants stringConstants)
        {
            _userManager = userManager;
            _emailService = emailService;
            _environment = environment;
            _trappistDbContext = trappistDbContext;
            _stringConstants = stringConstants;
        }
        #endregion

        #region IBasicSetupRepository methods
        /// <summary>
        /// This method used for creating the user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>if a user created then return true else return false</returns>
        public async Task<ServiceResponse> RegisterUser(BasicSetupModel model)
        {
            var response = new ServiceResponse();
            try
            {
                var user = new ApplicationUser();
                user.Name = model.RegistrationFields.Name;
                user.UserName = model.RegistrationFields.Email;
                user.Email = model.RegistrationFields.Email;
                user.CreateDateTime = DateTime.Now;
                response = SaveSetupParameter(model);
                if (response.Response == true)
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

        /// <summary>
        /// This method used for validating connection string
        /// </summary>
        /// <param name="model"></param>
        /// <returns>If valid then return true else return false</returns>
        public ServiceResponse ValidateConnectionString(BasicSetupModel model)
        {
            var response = new ServiceResponse();
            try
            {
                model.ConnectionString.Value = model.ConnectionString.Value.Replace("\\\\", "\\");
                model.ConnectionString.Value = model.ConnectionString.Value.Replace("\"", "");
                var builder = new SqlConnectionStringBuilder(model.ConnectionString.Value);
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

        /// <summary>
        /// This method used for removing database parameter from the connection string.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns>It returns the connection string without database </returns>
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

        /// <summary>
        /// This method used for verifying Email settings
        /// </summary>
        /// <param name="model"></param>
        /// <returns>if valid email settings then return true else false</returns>
        public bool ValidateEmailSetting(BasicSetupModel model)
        {
            return _emailService.SendMail(model.EmailSettings.UserName, model.EmailSettings.Password, model.EmailSettings.Server, model.EmailSettings.Port, "", "");
        }

        /// <summary>
        /// This method used for saving setup parameter in SetupConfig.json file
        /// </summary>
        /// <param name="model"></param>
        /// <returns>It return response object which have value true or false</returns>
        private ServiceResponse SaveSetupParameter(BasicSetupModel model)
        {
            var response = new ServiceResponse();
            model.ConnectionString.Value = model.ConnectionString.Value.Replace("\\\\", "\\");
            model.ConnectionString.Value = model.ConnectionString.Value.Replace("\"", "");
            var tempModel = new SetupConfig();
            tempModel.ConnectionString = model.ConnectionString;
            tempModel.EmailSettings = model.EmailSettings;
            string jsonData = JsonConvert.SerializeObject(tempModel, Formatting.Indented);
            string path = Path.Combine(_environment.ContentRootPath.ToString(), _stringConstants.ConfigFolderName, _stringConstants.SetupConfigFileName);
            if (FileExist())
            {
                File.Create(path).Dispose();
            }
            File.AppendAllText(path, jsonData);
            response.Response = true;
            return response;
        }

        /// <summary>
        /// This method used for checking SetupConfig.json file exist or not
        /// </summary>
        /// <returns>If file exist then return true or return false</returns>
        public bool FileExist()
        {
            string path = Path.Combine(_environment.ContentRootPath.ToString(), _stringConstants.ConfigFolderName, _stringConstants.SetupConfigFileName);
            return File.Exists(path);
        }
        #endregion
    }
}
