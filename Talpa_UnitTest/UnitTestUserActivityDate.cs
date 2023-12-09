using ModelLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talpa_BLL.Models;
using Talpa_BLL.Services;
using Talpa_UnitTest.StubRepositories;

namespace Talpa_UnitTest
{
    public class UnitTestUserActivityDate
    {
        private UserActivityDateService _userActivityDateService;
        private UserActivityDateTestRepository _userActivityDateTestRepository;

        [SetUp]
        public void Setup()
        {
            _userActivityDateTestRepository = new UserActivityDateTestRepository();
            _userActivityDateService = new UserActivityDateService(_userActivityDateTestRepository);
        }

        [Test]
        public async Task CreateUserActivityDateAsync_ShouldAddUserActivityDateToRepository()
        {
            // Arrange
            UserActivityDate userActivityDateDto = new UserActivityDate
            {
                UserId = "123456",
                ActivityDateId = 1,
                IsAvailable = true
            };

            // Act
            UserActivityDate userActivityDateAdded = await _userActivityDateService.AddUserActivityDateAsync(userActivityDateDto);

            // Assert
            Assert.That(userActivityDateAdded, Is.Not.Null);
        }
    }
}
