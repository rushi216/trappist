using Microsoft.AspNetCore.Mvc;
using Promact.Trappist.DomainModel.Models.Test;
using Promact.Trappist.Repository.Tests;
using Promact.Trappist.Utility.Constants;
using System.Threading.Tasks;

namespace Promact.Trappist.Core.Controllers
{
    [Produces("application/json")]
    [Route("api/tests")]
    public class TestsController : Controller
    {
        private readonly ITestsRepository _testRepository;
        private readonly IStringConstants _stringConstant;
  
        public TestsController(ITestsRepository testRepository, IStringConstants stringConstant)
        {
            _testRepository = testRepository;
            _stringConstant = stringConstant;
        } 
        /// <summary>
        /// this method is to verify unique name of a test
        /// </summary>
        /// <param name="test"></param>
        /// <returns>boolean</returns>
        [HttpGet("{testName}")]   
        public async Task<Response> IsUniqueTestName([FromRoute] string testName)
        {
            Response response = new Response();
            // verifying the test name is unique or not
            response = await _testRepository.UniqueTestName(testName);
            if (response.ResponseValue)
            {
                return (response);
            }
            else
            {
                response.ResponseValue = false;
                return (response);
            }
        }
        /// <summary>
        /// this method is used to add a new test 
        /// </summary>
        /// <param name="test">object of the Test</param>
        /// <returns>string</returns>
        [HttpPost]
        public ActionResult CreateTest([FromBody] Test test)
        {                                                
                _testRepository.RandomLinkString(test, 10);
                _testRepository.CreateTest(test);
                return Ok(test);                                                         
        }
        /// <summary>
        /// Get All Tests
        /// </summary>
        /// <returns>List of Tests</returns>
        [HttpGet]
        public IActionResult GetAllTest()
        {
            var tests = _testRepository.GetAllTests();
            return Ok(tests);
        }
    }
}