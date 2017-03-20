using Microsoft.AspNetCore.Mvc;
using Promact.Trappist.DomainModel.ApplicationClasses.BasicSetup;
using Promact.Trappist.Repository.BasicSetup;
using System;
using System.Threading.Tasks;

namespace Promact.Trappist.Core.Controllers
{
    [Route("api/BasicSetup")]
    public class BasicSetupController : Controller
    {
        #region Private Variables
        #region Dependencies
        private readonly IBasicSetupRepository _basicSetup;
        #endregion
        #endregion

        #region Constructor
        public BasicSetupController(IBasicSetupRepository basicSetup)
        {
            _basicSetup = basicSetup;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// This method validate connection string
        /// </summary>
        /// <param name="model"></param>
        /// <returns>If valid than true else false</returns>
        [Route("ConnectionString")]
        [HttpPost]
        public IActionResult ValidateConnectionString([FromBody] BasicSetup model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_basicSetup.ValidateConnectionString(model));
            }
            return Ok(false);
        }

        /// <summary>
        /// This method will verify Email settings
        /// </summary>
        /// <param name="model"></param>
        /// <returns>if valid email settings than return true
        ///          else false
        /// </returns>
        [Route("MailSettings")]
        [HttpPost]
        public  IActionResult ValidateEmailSettings([FromBody] BasicSetup model)
        {
            if (ModelState.IsValid)
            {
                var response = new EmailResponse();
                if (_basicSetup.ValidateEmailSetting(model))
                {
                    response.IsMailSent = true;
                    return Ok(response);
                }
                response.IsMailSent = false;
                return Ok(response);
            }
            return Ok(false);
        }

        /// <summary>
        /// This method will create user if valid user credential
        /// </summary>
        /// <param name="model"></param>
        /// <returns>if user created than return true else false</returns>
        [Route("Validateuser")]
        [HttpPost]
        public async Task<IActionResult> ValidateUser([FromBody] BasicSetup model)
        {
            if (ModelState.IsValid)
            {
                return Ok(await _basicSetup.RegisterUser(model));
            }
            return Ok(false);
        }
        #endregion
    }
}
