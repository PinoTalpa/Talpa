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
    public class UnitTestActivityDate
    {
        private ActivityDateService _activityDateService;
        private ActivityDateTestRepository _activityDateTestRepository;

        [SetUp]
        public void Setup()
        {
            _activityDateTestRepository = new ActivityDateTestRepository();
            _activityDateService = new ActivityDateService(_activityDateTestRepository);
        }

        [Test]
        public async Task CreateActivityDates_ShouldAddActivityDatesToRepository()
        {
            // Arrange
            List<ActivityDate> activityDates = new List<ActivityDate>
            {
                new() { Id = 1, SuggestionId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) },
                new() { Id = 2, SuggestionId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) },
                new() { Id = 3, SuggestionId = 2, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) },
            };

            // Act
            await _activityDateService.CreateActivityDates(activityDates);

            // Assert
            Assert.That(activityDates, Has.Count.EqualTo(3));
        }

        [Test]
        public async Task GetActivitiesDateWithId_ShouldReturnFilteredActivities()
        {
            // Arrange
            int selectedSuggestionId = 1;
            List<ActivityDateDto> activityDates = new List<ActivityDateDto>
            {
                new ActivityDateDto { Id = 1, SuggestionId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) },
                new ActivityDateDto { Id = 2, SuggestionId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) },
                new ActivityDateDto { Id = 3, SuggestionId = 2, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) },
            };

            _activityDateTestRepository.InitializeActivityDates(activityDates);

            // Act
            List<ActivityDate> resultActivityDates = await _activityDateService.GetActivityDatesWithId(selectedSuggestionId);

            // Assert
            Assert.That(resultActivityDates, Has.Count.EqualTo(2));
        }
    }
}
