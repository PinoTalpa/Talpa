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
    public class UnitTestUser
    {
        private UserService _userService;
        private UserTestRepository _userTestRepository;

        [SetUp]
        public void Setup()
        {
            _userTestRepository = new UserTestRepository();
            _userService = new UserService(_userTestRepository);
        }

        [Test]
        public async Task GetUserAsync_ValidUserId_ReturnsUserDto()
        {
            // Arrange
            List<UserDto> users = new()
            {
                new UserDto { Id = "12345", Name = "TestUser", Email = "testuser@gmail.com" }
            };

            _userTestRepository.InitializeUsers(users);

            // Act
            var user = await _userService.GetUserAsync("12345");

            // Assert
            Assert.That(user, Is.Not.Null);
            Assert.That(user.Id, Is.EqualTo("12345"));
        }

        [Test]
        public async Task Login_ValidCredentials_ReturnsTaskCompletedSuccessfully()
        {
            // Arrange
            UserDto user = new()
            {
                Id = "12347",
                Name = "John Doe",
                Email = "john@example.com",
            };

            // Act
            await _userService.Login(user.Id, user.Name, user.Email);
            UserDto? newUser = _userTestRepository.GetUser();

            // Assert
            Assert.That(newUser, Is.Not.EqualTo(null));
        }

        [Test]
        public async Task UpdateUserAsync_ValidUserDto_ReturnsTrue()
        {
            // Arrange
            List<UserDto> users = new()
            {
                new UserDto { Id = "12345", Name = "TestUser", Email = "testuser@gmail.com" }
            };

            _userTestRepository.InitializeUsers(users);

            User userToUpdate = new()
            {
                Id = "12345",
                Name = "NewTestUser",
                Email = "testuser@gmail.com",
            };

            // Act
            User updatedUser = await _userService.UpdateUserAsync(userToUpdate);

            // Assert
            Assert.That(updatedUser.Id, Is.EqualTo(userToUpdate.Id)); ;
        }
    }
}
