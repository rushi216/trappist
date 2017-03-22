using Promact.Trappist.DomainModel.DbContext;
using System.Linq;
using Promact.Trappist.DomainModel.Models.Test;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Promact.Trappist.Repository.Tests
{
    public class TestsRepository : ITestsRepository
    {
        private static Random random = new Random();
        private readonly TrappistDbContext _dbContext;
        public TestsRepository(TrappistDbContext dbContext)
        {
            _dbContext = dbContext;
           
        }
        /// <summary>
        /// this method is used to create a new test
        /// </summary>
        /// <param name="test">object of Test</param>
        public void CreateTest(Test test)
        {
            _dbContext.Test.Add(test);
            _dbContext.SaveChanges();
        }
        /// <summary>
        /// Fetch all the tests from Test Model,Convert it into List
        /// </summary>
        /// <returns>List of Tests</returns>   
        public List<Test> GetAllTests()
        {
            var tests = _dbContext.Test.ToList();
            return tests;
        }

        /// <summary>
        ///  this method is used to generate a random string for every test at the time of test creation
        /// <param name="test">object of Test</param>
        /// <param name="length">length of the random string</param>
        /// </summary>
        public void RandomLinkString(Test test, int length)
        {
            const string charactersForRandomString = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
           test.Link= new string(Enumerable.Repeat(charactersForRandomString, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        /// <summary>
        /// this method is used to check whether test name is unique or not
        /// </summary>
        /// <param name="test">object of Test</param>
        /// <returns>boolean</returns>
        public async Task<Response> UniqueTestName(string testName)
        {
            Response response = new Response();
            response.ResponseValue = false;
            var testnameCheck = _dbContext.Test.FirstOrDefault(x => x.TestName == testName);            
            if (testnameCheck != null) 
                return response;                         
            else
            {
                response.ResponseValue = true;
                return response;
            }
            }               
        }   
    }
