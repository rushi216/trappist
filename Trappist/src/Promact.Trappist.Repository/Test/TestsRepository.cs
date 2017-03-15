using Promact.Trappist.DomainModel.DbContext;
using System.Linq;
using Promact.Trappist.DomainModel.Models.Test;
using System;
using System.Collections.Generic;

namespace Promact.Trappist.Repository.Tests
{
    public class TestsRepository : ITestsRepository
    {
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
            _dbContext.SaveChangesAsync();
        }
        /// <summary>
        ///  this method is used to generate a random string for every test at the time of test creation
        /// <param name="test">object of Test</param>
        /// <param name="length">length of the random string</param>
        /// </summary>
        private static Random random = new Random();
        public void RandomLinkString(Test test, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
           test.Link= new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        /// <summary>
        /// this method is used to check whether test name is unique or not
        /// </summary>
        /// <param name="test">object of Test</param>
        /// <returns>boolean</returns>
        public bool UniqueTestName(Test test)
        {
            var testObj = (from s in _dbContext.Test
                          where s.TestName == test.TestName
                          select s).FirstOrDefault();
            if (testObj != null)
                return false;
            else
                return true;
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
    }
}