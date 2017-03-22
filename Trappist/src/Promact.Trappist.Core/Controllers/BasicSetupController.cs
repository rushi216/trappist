using Microsoft.AspNetCore.Mvc;
using Promact.Trappist.DomainModel.ApplicationClasses.BasicSetup;
using Promact.Trappist.Repository.BasicSetup;
using System.Threading.Tasks;

namespace Promact.Trappist.Core.Controllers
{
    [Route("api/setup")]
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
        /// This method used for validating connection string
        /// </summary>
        /// <param name="model"></param>
        /// <returns>If valid then return true else return false</returns>
        [Route("connectionstring")]
        [HttpPost]
        public IActionResult ValidateConnectionString([FromBody] BasicSetupModel model)
        {
            if (ModelState.IsValid)
            {
                return Ok(_basicSetup.ValidateConnectionString(model));
            }
            return Ok(false);
        }

        /// <summary>
        /// This method used for verifying Email settings
        /// </summary>
        /// <param name="model"></param>
        /// <returns>if valid email settings then return true else false</returns>
        [Route("mailsettings")]
        [HttpPost]
        public IActionResult ValidateEmailSettings([FromBody] BasicSetupModel model)
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
        /// This method used for creating the user.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>if a user created then return true else return false</returns>
        [Route("validateuser")]
        [HttpPost]
        public async Task<IActionResult> ValidateUser([FromBody] BasicSetupModel model)
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
