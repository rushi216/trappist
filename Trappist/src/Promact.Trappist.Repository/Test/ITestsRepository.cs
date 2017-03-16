﻿using Promact.Trappist.DomainModel.Models.Test;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Promact.Trappist.Repository.Tests
{
    public interface ITestsRepository
    {
        /// <summary>
        /// this method is used to create a new test
        /// </summary>
        /// <param name="test">object of Test</param>
        void CreateTest(Test test);
        /// <summary>
        /// this method is used to verify Name of the test is unique or not
        /// </summary>
        /// <param name="test">object of Test</param>
        /// <returns>boolean</returns>
        bool UniqueTestName(Test test);
        /// <summary>
        /// this method is used to generate a random string which is unique for every test
        /// </summary>
        /// <param name="test">object of Test</param>
        /// <param name="length">length of the random string</param>
        void RandomLinkString(Test test, int length);
    }
    /// <summary>
    /// Method defined to fetch Tests from Test Model
    /// </summary>
    /// <returns>List Of Tests</returns>
    List<Test> GetAllTests();

    }

}