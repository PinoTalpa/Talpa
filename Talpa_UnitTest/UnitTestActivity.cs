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
            List<ActivityDto> activityDtos = new()
            {
                new ActivityDto { Id = 1, UserId = "1029734", Name = "Bowlen", Description = "Bowlen om 8 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted  },
                new ActivityDto { Id = 2, UserId = "1029734", Name = "Padellen", Description = "Padellen om 3 uur in de middag", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted  },
                new ActivityDto { Id = 3, UserId = "1029734", Name = "Film", Description = "Film kijken om 9 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted },
            };

            _activityTestRepository.InitializeActivities(activityDtos);

            // Act
            List<Activity> activities = await _activityService.GetActivitiesAsync("");

            // Assert
            Assert.That(activities, Has.Count.EqualTo(3));
        }

        [Test]
        public async Task GetActivityDates_ShouldGetAllActivityDatesFromRepository()
        {
            // Arrange
            List<ActivityDto> activityDtos = new()
            {
                new ActivityDto { Id = 1, UserId = "1029734", Name = "Bowlen", Description = "Bowlen om 8 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted  },
                new ActivityDto { Id = 2, UserId = "1029734", Name = "Padellen", Description = "Padellen om 3 uur in de middag", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted  },
                new ActivityDto { Id = 3, UserId = "1029734", Name = "Film", Description = "Film kijken om 9 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted },
            };

            List<ActivityDateDto> activityDateDtos = new()
            {
                new ActivityDateDto { Id = 1, SuggestionId = 2, StartDate = DateTime.Now, EndDate = DateTime.Today},
                new ActivityDateDto { Id = 2, SuggestionId = 2, StartDate = DateTime.Now, EndDate = DateTime.Today},
            };

            _activityTestRepository.InitializeActivities(activityDtos);
            _activityTestRepository.InitializeActivityDates(activityDateDtos);

            // Act
            List<ActivityDate> activities = await _activityService.GetActivityDates(2);

            // Assert
            Assert.That(activities, Has.Count.EqualTo(2));
        }

        [Test]
        public async Task CreateActivity_ShouldAddActivityToRepository()
        {
            // Arrange
            List<SuggestionDto> suggestionDtos = new()
            {
                new SuggestionDto { Id = 1, UserId = "1029734", Name = "Bowlen", Description = "Bowlen om 8 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new SuggestionDto { Id = 2, UserId = "1029734", Name = "Padellen", Description = "Padellen om 3 uur in de middag", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new SuggestionDto { Id = 3, UserId = "1029734", Name = "Film", Description = "Film kijken om 9 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Rejected },
                new SuggestionDto { Id = 4, UserId = "1029734", Name = "Karten", Description = "Karten om 4 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted },
            };

            _activityTestRepository.InitializeSuggestions(suggestionDtos);

            Suggestion suggestion = new()
            {
                Id = 2,
                UserId = "1029734",
                Name = "Padellen",
                Description = "Padellen om 3 uur in de middag",
                ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg",
                Date = null,
                ActivityState = (Talpa_DAL.Enums.ActivityState)ModelLayer.Enums.ActivityState.Accepted
            };

            // Act
            Suggestion suggestionAdded = await _activityService.CreateActivityAsync(suggestion);

            // Assert
            Assert.That(suggestionAdded.ErrorMessage, Is.EqualTo(null));
        }

        [Test]
        public async Task GetActivityById_ShouldGetActivityByIdFromRepository()
        {
            // Arrange
            List<SuggestionDto> suggestionDtos = new()
            {
                new SuggestionDto { Id = 1, UserId = "1029734", Name = "Bowlen", Description = "Bowlen om 8 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new SuggestionDto { Id = 2, UserId = "1029734", Name = "Padellen", Description = "Padellen om 3 uur in de middag", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new SuggestionDto { Id = 3, UserId = "1029734", Name = "Film", Description = "Film kijken om 9 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Rejected },
                new SuggestionDto { Id = 4, UserId = "1029734", Name = "Karten", Description = "Karten om 4 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted },
            };

            _activityTestRepository.InitializeSuggestions(suggestionDtos);

            // Act
            Activity activity = await _activityService.GetActivityByIdAsync(1);

            // Assert
            Assert.That(activity.Id, Is.EqualTo(1));
        }

        [Test]
        public async Task RemoveActivity_ShouldRemoveActivityToRepository()
        {
            // Arrange
            List<ActivityDto> activityDtos = new()
            {
                new ActivityDto { Id = 1, UserId = "1029734", Name = "Bowlen", Description = "Bowlen om 8 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new ActivityDto { Id = 2, UserId = "1029734", Name = "Padellen", Description = "Padellen om 3 uur in de middag", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new ActivityDto { Id = 3, UserId = "1029734", Name = "Film", Description = "Film kijken om 9 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Rejected },
                new ActivityDto { Id = 4, UserId = "1029734", Name = "Karten", Description = "Karten om 4 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted },
            };

            _activityTestRepository.InitializeActivities(activityDtos);

            Activity activity = new()
            {
                Id = 2,
                UserId = "1029734",
                Name = "Padellen",
                Description = "Padellen om 3 uur in de middag",
                ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg",
                Date = null,
                ActivityState = (Talpa_DAL.Enums.ActivityState)ModelLayer.Enums.ActivityState.Rejected
            };

            // Act
            Activity activityRemoved = await _activityService.RemoveActivityAsync(activity);

            // Assert
            Assert.That(activityRemoved.ActivityState, Is.EqualTo(Talpa_DAL.Enums.ActivityState.Rejected));
        }
    }
}