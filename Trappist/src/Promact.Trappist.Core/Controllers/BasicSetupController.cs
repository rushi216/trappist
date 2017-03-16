using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Promact.Trappist.DomainModel.ApplicationClasses;
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
        /// <returns>If valid than true else error message</returns>
        [Route("connection")]
        [HttpPost]
        public async Task<IActionResult> ValidateConnectionString([FromBody] ConnectionStringParamters model)
        {

            if (ModelState.IsValid)
            {
                return Ok(await _basicSetup.ValidateConnectionString(model));
            }
            return BadRequest();
        }

        /// <summary>
        /// This method will verify Email settings
        /// </summary>
        /// <param name="model"></param>
        /// <returns>if valid email settings than return true
        ///          else Error Message
        /// </returns>
        [Route("mail")]
        [HttpPost]
        public async Task<IActionResult> ValidateEmailSettings([FromBody] EmailSettings model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _basicSetup.ValidateEmailSetting(model));
                }
            }
            catch (Exception ex)
            {

            }
            return BadRequest();
        }

        /// <summary>
        /// This method will create user
        /// </summary>
        /// <param name="model"></param>
        /// <returns>if user created than return true else error message</returns>
        //Post: /api/BasicSetup
        [Route("validateuser")]
        [HttpPost]
        public async Task<IActionResult> ValidateUser([FromBody] BasicSetup model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return Ok(await _basicSetup.RegisterUser(model));
                }
            }
            catch (Exception ex)
            {
                return Ok(false);
            }
            return BadRequest();
        }

        /// <summary>
        /// This method will store parameter
        /// </summary>
        /// <returns>It return true if it store connection string and email settings  parameters in json file</returns>
        //[Route("json")]
        // [HttpGet]
        public async Task<IActionResult> SaveSetupParameter()
        {
            try
            {
                if (await _basicSetup.SaveSetupParameter())
                {
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return BadRequest();
        }
        #endregion
    }
}
