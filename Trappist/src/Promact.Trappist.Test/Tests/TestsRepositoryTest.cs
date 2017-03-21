﻿using System;
using Promact.Trappist.DomainModel.DbContext;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Promact.Trappist.Repository.Tests;
using System.Linq;
using Promact.Trappist.DomainModel.Models.Test;

namespace Promact.Trappist.Test.Tests
{
    [Collection("Register Dependency")]
    public class TestsRepositoryTest
    {
        private readonly Bootstrap _bootstrap;
        private readonly ITestsRepository _testRepository;
        private readonly TrappistDbContext _trappistDbContext;

        public TestsRepositoryTest(Bootstrap bootstrap)
        {
            _bootstrap = bootstrap;
            //resolve dependency to be used in tests
            _trappistDbContext = _bootstrap.ServiceProvider.GetService<TrappistDbContext>();
            _testRepository = _bootstrap.ServiceProvider.GetService<ITestsRepository>();
            ClearDatabase.ClearDatabaseAndSeed(_trappistDbContext);

        }
        /// <summary>
        /// Test Case For Not Empty Test Model
        /// </summary>
        [Fact]
        public void GetAllTest()
        {
            AddTests();
            var list = _testRepository.GetAllTests();
            Assert.NotNull(list);
            Assert.Equal(3, list.Count);
        }
        /// <summary>
        /// Test Case For Emtpty Test Model
        /// </summary>
        [Fact]
        public void GetAllTestEmpty()
        {
            var list = _testRepository.GetAllTests();
            Assert.Equal(0, list.Count);
        }
        private void AddTests()
        {
            _trappistDbContext.Test.Add(new DomainModel.Models.Test.Test() { TestName = "BBIT 123" });
            _trappistDbContext.Test.Add(new DomainModel.Models.Test.Test() { TestName = "MCKV 123" });
            _trappistDbContext.Test.Add(new DomainModel.Models.Test.Test() { TestName = "CU 123" });
            _trappistDbContext.SaveChanges();
        }
        [Fact]
        private void AddTest()
        {
            var test = CreateTest();
            _testRepository.CreateTest(test);
            Assert.True(_trappistDbContext.Test.Count() == 1);          
        }

        [Fact]
        public void UniqueNameTest()
        {
            var test = CreateTest();
            _testRepository.CreateTest(test);
            var name = "abc";
            Response response = new Response();
            _testRepository.UniqueTestName(name);
            _testRepository.CreateTest(test);
            Assert.True(_trappistDbContext.Test.Count() == 2);
        }
        [Fact]
        public void IsNotUniqueNameTest()
        {
            var test = CreateTest();
            _testRepository.CreateTest(test);
            Response response = new Response();
            var name = "test name";
            _testRepository.UniqueTestName(name);
            _testRepository.CreateTest(test);
            Assert.True(_trappistDbContext.Test.Count() == 1);
        }
        [Fact]
        public void RandomLinkStringTest()
        {
            var test = CreateTest();
            _testRepository.CreateTest(test);
            _testRepository.RandomLinkString(test, 10);
            Assert.True(_trappistDbContext.Test.Count() == 1);
        }
        private DomainModel.Models.Test.Test CreateTest()
        {
            var test = new DomainModel.Models.Test.Test
            {
                TestName = "test name",
            };
            return test;
        }
    }
}