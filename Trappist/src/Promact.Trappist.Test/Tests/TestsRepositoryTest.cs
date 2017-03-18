using System;
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
            AddTest();
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
        private void AddTest()
        {
            _trappistDbContext.Test.Add(new DomainModel.Models.Test.Test() { TestName = "BBIT 123" });
            _trappistDbContext.Test.Add(new DomainModel.Models.Test.Test() { TestName = "MCKV 123" });
            _trappistDbContext.Test.Add(new DomainModel.Models.Test.Test() { TestName = "CU 123" });
            _trappistDbContext.SaveChanges();
        }

        [Fact]
        public bool UniqueNameTest()
        {
            var test = _trappistDbContext.Test.ToList().FirstOrDefault(x => x.TestName == "Fresh");
            _testRepository.UniqueTestName(test);
            Assert.True(_trappistDbContext.Test.Count()==0);
            return true;
        }
        [Fact]
        public void RandomLinkStringTest()
        {
            var test = new DomainModel.Models.Test.Test();
            _testRepository.RandomLinkString(test, 10);
            Assert.True(_trappistDbContext.Test.Count() == 0);
        }
        
    }
}