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
    public class UnitTestSettings
    {
        private SettingsService _settingsService;
        private SettingsTestRepository _settingsTestRepository;

        [SetUp]
        public void Setup()
        {
            _settingsTestRepository = new SettingsTestRepository();
            _settingsService = new SettingsService(_settingsTestRepository);
        }

        [Test]
        public async Task GetSettings_ShouldReturnSettingsFromRepository()
        {
            // Arrange
            SettingsDto settingsDto = new()
            { 
                Id = 1, 
                PrimaryColor = "Red", 
                SecondaryColor = "Blue", 
                BackgroundColor = "White" 
            };

            _settingsTestRepository.InitializeSettings(settingsDto);

            // Act
            Settings resultSettings = await _settingsService.GetSettings();

            // Assert
            Assert.That(resultSettings, Is.Not.Null);
        }

        [Test]
        public async Task UpdateSettings_ShouldUpdateSettingsInRepository()
        {
            // Arrange
            SettingsDto initialSettings = new SettingsDto
            {
                Id = 1,
                PrimaryColor = "Red",
                SecondaryColor = "Blue",
                BackgroundColor = "White"
            };

            Settings updatedSettings = new Settings
            {
                Id = 1,
                PrimaryColor = "Green",
                SecondaryColor = "Yellow",
                BackgroundColor = "Black"
            };

            _settingsTestRepository.InitializeSettings(initialSettings);

            // Act
            Settings resultSettings = await _settingsService.UpdateSettings(updatedSettings);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(resultSettings.PrimaryColor, Is.EqualTo("Green"));
                Assert.That(resultSettings.SecondaryColor, Is.EqualTo("Yellow"));
                Assert.That(resultSettings.BackgroundColor, Is.EqualTo("Black"));
            });
        }
    }
}
