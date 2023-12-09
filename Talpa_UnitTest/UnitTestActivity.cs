using ModelLayer.Enums;
using ModelLayer.Models;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;
using Talpa_BLL.Services;
using Talpa_DAL.Enums;
using Talpa_UnitTest.StubRepositories;

namespace Talpa_UnitTest
{
    public class ActivityTests
    {
        private ActivityService _activityService;
        private ActivityTestRepository _activityTestRepository;

        [SetUp]
        public void Setup()
        {
            _activityTestRepository = new ActivityTestRepository();
            _activityService = new ActivityService(_activityTestRepository);
        }

        [Test]
        public async Task GetActivities_ShouldGetAllActivitiesFromRepository()
        {
            // Arrange
            List<SuggestionDto> suggestionDtos = new()
            {
                new SuggestionDto { Id = 1, UserId = "1029734", Name = "Bowlen", Description = "Bowlen om 8 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted  },
                new SuggestionDto { Id = 2, UserId = "1029734", Name = "Padellen", Description = "Padellen om 3 uur in de middag", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted  },
                new SuggestionDto { Id = 3, UserId = "1029734", Name = "Film", Description = "Film kijken om 9 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted },
            };

            _activityTestRepository.InitializeActivities(suggestionDtos);

            // Act
            List<Suggestion> activities = await _activityService.GetActivitiesAsync("");

            // Assert
            Assert.That(activities, Has.Count.EqualTo(3));
        }

        [Test]
        public async Task GetActivitiesWithSuggestions_ShouldReturnFilteredActivities()
        {
            // Arrange
            List<ActivityDateDto> activityDateDtos = new()
            {
                new ActivityDateDto { Id = 1, SuggestionId = 1, Suggestion = new SuggestionDto { Id = 1, UserId = "1029734", Name = "Bowlen", Description = "Bowlen om 8 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted }, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) },
                new ActivityDateDto { Id = 2, SuggestionId = 2, Suggestion = new SuggestionDto { Id = 2, UserId = "1029734", Name = "Padellen", Description = "Padellen om 3 uur in de middag", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted }, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) },
                new ActivityDateDto { Id = 3, SuggestionId = 3, Suggestion = new SuggestionDto { Id = 3, UserId = "1029734", Name = "Film", Description = "Film kijken om 9 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted }, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) },
            };

            _activityTestRepository.InitializeActivitiesDates(activityDateDtos);

            // Act
            List<Activity> activitiesWithSuggestions = await _activityService.GetActivitiesWithSuggestionsAsync();

            // Assert
            Assert.That(activitiesWithSuggestions, Is.Not.Null);
            Assert.That(activitiesWithSuggestions, Has.Count.EqualTo(3));
        }

        [Test]
        public async Task GetActivityById_ShouldGetActivityByIdFromRepository()
        {
            // Arrange
            List<SuggestionDto> activityDtos = new()
            {
                new SuggestionDto { Id = 1, UserId = "1029734", Name = "Bowlen", Description = "Bowlen om 8 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new SuggestionDto { Id = 2, UserId = "1029734", Name = "Padellen", Description = "Padellen om 3 uur in de middag", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new SuggestionDto { Id = 3, UserId = "1029734", Name = "Film", Description = "Film kijken om 9 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Rejected },
                new SuggestionDto { Id = 4, UserId = "1029734", Name = "Karten", Description = "Karten om 4 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted },
            };

            _activityTestRepository.InitializeActivities(activityDtos);

            // Act
            List<Suggestion> activities = await _activityService.GetActivitiesAsync("");

            // Assert
            Assert.That(activities, Has.Count.EqualTo(4));
        }

        [Test]
        public async Task CreateSuggestion_ShouldAddSuggestionToRepository()
        {
            // Arrange
            List<SuggestionDto> activityDtos = new()
            {
                new SuggestionDto { Id = 1, UserId = "1029734", Name = "Bowlen", Description = "Bowlen om 8 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new SuggestionDto { Id = 2, UserId = "1029734", Name = "Padellen", Description = "Padellen om 3 uur in de middag", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new SuggestionDto { Id = 3, UserId = "1029734", Name = "Film", Description = "Film kijken om 9 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Rejected },
                new SuggestionDto { Id = 4, UserId = "1029734", Name = "Karten", Description = "Karten om 4 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted },
            };

            _activityTestRepository.InitializeActivities(activityDtos);

            Suggestion activity = new()
            {
                Id = 5,
                UserId = "1029734",
                Name = "Lasergamen",
                Description = "Lasergamen om 8 uur in de avond",
                ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg",
                Date = null,
                ActivityState = (Talpa_DAL.Enums.ActivityState)ModelLayer.Enums.ActivityState.Pending
            };

            // Act
            Suggestion activityAdded = await _activityService.CreateActivityAsync(activity);

            // Assert
            Assert.That(activityAdded.ErrorMessage, Is.EqualTo(null));
        }

        [Test]
        public async Task GetActivityDates_ShouldReturnDatesForGivenActivityId()
        {
            // Arrange
            int activityId = 1;

            List<ActivityDateDto> activityDateDtos = new()
            {
                new ActivityDateDto { Id = 1, SuggestionId = activityId, Suggestion = new SuggestionDto { Id = activityId, UserId = "1029734", Name = "Bowlen", Description = "Bowlen om 8 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted }, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) },
                new ActivityDateDto { Id = 2, SuggestionId = activityId, Suggestion = new SuggestionDto { Id = activityId, UserId = "1029734", Name = "Padellen", Description = "Padellen om 3 uur in de middag", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted }, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) },
                new ActivityDateDto { Id = 3, SuggestionId = activityId, Suggestion = new SuggestionDto { Id = activityId, UserId = "1029734", Name = "Film", Description = "Film kijken om 9 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted }, StartDate = DateTime.Now, EndDate = DateTime.Now.AddDays(1) },
            };

            _activityTestRepository.InitializeActivitiesDates(activityDateDtos);

            // Act
            List<ActivityDate> activityDates = await _activityService.GetActivityDates(activityId);

            // Assert
            Assert.That(activityDates, Is.Not.Null);
            Assert.That(activityDates, Has.Count.EqualTo(3));
        }
    }
}