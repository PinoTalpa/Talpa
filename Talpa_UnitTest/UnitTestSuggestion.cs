using ModelLayer.Models;
using Talpa_BLL.Models;
using Talpa_BLL.Services;
using Talpa_UnitTest.StubRepositories;

namespace Talpa_UnitTest
{
    public class SuggestionTests
    {
        private SuggestionService _suggestionService;
        private SuggestionTestRepository _suggestionTestRepository;

        [SetUp]
        public void Setup()
        {
            _suggestionTestRepository = new SuggestionTestRepository();
            _suggestionService = new SuggestionService(_suggestionTestRepository);
        }

        [Test]
        public async Task GetPendingSuggestions_ShouldGetPendingSuggestionsFromRepository()
        {
            // Arrange
            List<SuggestionDto> suggestionDtos = new()
            {
                new SuggestionDto { Id = 1, UserId = "1029734", Name = "Bowlen", Description = "Bowlen om 8 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new SuggestionDto { Id = 2, UserId = "1029734", Name = "Padellen", Description = "Padellen om 3 uur in de middag", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new SuggestionDto { Id = 3, UserId = "1029734", Name = "Film", Description = "Film kijken om 9 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Rejected },
            };

            _suggestionTestRepository.InitializeSuggestions(suggestionDtos);

            // Act
            List<Suggestion> suggestions = await _suggestionService.GetPendingSuggestionsAsync("");

            // Assert
            Assert.That(suggestions, Has.Count.EqualTo(2));
        }

        [Test]
        public async Task GetSuggestions_ShouldGetAllSuggestionsFromRepository()
        {
            // Arrange
            List<SuggestionDto> suggestionDtos = new()
            {
                new SuggestionDto { Id = 1, UserId = "1029734", Name = "Bowlen", Description = "Bowlen om 8 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new SuggestionDto { Id = 2, UserId = "1029734", Name = "Padellen", Description = "Padellen om 3 uur in de middag", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new SuggestionDto { Id = 3, UserId = "1029734", Name = "Film", Description = "Film kijken om 9 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Rejected },
                new SuggestionDto { Id = 4, UserId = "1029734", Name = "Karten", Description = "Karten om 4 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted },
            };

            _suggestionTestRepository.InitializeSuggestions(suggestionDtos);

            // Act
            List<Suggestion> suggestions = await _suggestionService.GetSuggestionsAsync("");

            // Assert
            Assert.That(suggestions, Has.Count.EqualTo(4));
        }

        [Test]
        public async Task GetSuggestionById_ShouldGetSuggestionByIdFromRepository()
        {
            // Arrange
            List<SuggestionDto> suggestionDtos = new()
            {
                new SuggestionDto { Id = 1, UserId = "1029734", Name = "Bowlen", Description = "Bowlen om 8 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new SuggestionDto { Id = 2, UserId = "1029734", Name = "Padellen", Description = "Padellen om 3 uur in de middag", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new SuggestionDto { Id = 3, UserId = "1029734", Name = "Film", Description = "Film kijken om 9 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Rejected },
                new SuggestionDto { Id = 4, UserId = "1029734", Name = "Karten", Description = "Karten om 4 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted },
            };

            _suggestionTestRepository.InitializeSuggestions(suggestionDtos);

            // Act
            Suggestion suggestion = await _suggestionService.GetSuggestionByIdAsync(1);

            // Assert
            Assert.That(suggestion.Id, Is.EqualTo(1));
        }

        [Test]
        public async Task CreateSuggestion_ShouldAddSuggestionToRepository()
        {
            // Arrange
            List<SuggestionDto> suggestionDtos = new()
            {
                new SuggestionDto { Id = 1, UserId = "1029734", Name = "Bowlen", Description = "Bowlen om 8 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new SuggestionDto { Id = 2, UserId = "1029734", Name = "Padellen", Description = "Padellen om 3 uur in de middag", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new SuggestionDto { Id = 3, UserId = "1029734", Name = "Film", Description = "Film kijken om 9 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Rejected },
                new SuggestionDto { Id = 4, UserId = "1029734", Name = "Karten", Description = "Karten om 4 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted },
            };

            _suggestionTestRepository.InitializeSuggestions(suggestionDtos);

            Suggestion suggestion = new()
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
            Suggestion suggestionAdded = await _suggestionService.CreateSuggestionAsync(suggestion);

            // Assert
            Assert.That(suggestionAdded.ErrorMessage, Is.EqualTo(null));
        }

        [Test]
        public async Task DeclineSuggestion_ShouldDeclineSuggestionFromRepository()
        {
            // Arrange
            List<SuggestionDto> suggestionDtos = new()
            {
                new SuggestionDto { Id = 1, UserId = "1029734", Name = "Bowlen", Description = "Bowlen om 8 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new SuggestionDto { Id = 2, UserId = "1029734", Name = "Padellen", Description = "Padellen om 3 uur in de middag", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new SuggestionDto { Id = 3, UserId = "1029734", Name = "Film", Description = "Film kijken om 9 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Rejected },
                new SuggestionDto { Id = 4, UserId = "1029734", Name = "Karten", Description = "Karten om 4 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted },
            };

            _suggestionTestRepository.InitializeSuggestions(suggestionDtos);

            Suggestion suggestion = new()
            {
                Id = 2,
                UserId = "1029734",
                Name = "Padellen",
                Description = "Padellen om 3 uur in de middag",
                ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg",
                Date = null,
                ActivityState = (Talpa_DAL.Enums.ActivityState)ModelLayer.Enums.ActivityState.Pending
            };

            // Act
            Suggestion suggestionDeclined = await _suggestionService.DeclineSuggestionAsync(suggestion);

            // Assert
            Assert.That(suggestionDeclined.ErrorMessage, Is.EqualTo(null));
        }

        [Test]
        public async Task SuggestionName_ShouldDeclineDuplicatedSuggestionFromRepository()
        {
            // Arrange
            List<SuggestionDto> suggestionDtos = new()
            {
                new SuggestionDto { Id = 1, UserId = "1029734", Name = "Bowlen", Description = "Bowlen om 8 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new SuggestionDto { Id = 2, UserId = "1029734", Name = "Padellen", Description = "Padellen om 3 uur in de middag", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new SuggestionDto { Id = 3, UserId = "1029734", Name = "Film", Description = "Film kijken om 9 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Rejected },
                new SuggestionDto { Id = 4, UserId = "1029734", Name = "Karten", Description = "Karten om 4 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted },
            };

            _suggestionTestRepository.InitializeSuggestions(suggestionDtos);

            Suggestion suggestion = new()
            {
                Id = 5,
                UserId = "1029734",
                Name = "bowlen",
                Description = "Padellen om 3 uur in de middag",
                ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg",
                Date = null,
                ActivityState = (Talpa_DAL.Enums.ActivityState)ModelLayer.Enums.ActivityState.Pending
            };

            // Act
            bool isDuplicateName = await _suggestionService.SuggestionNameExistsAsync(suggestion.Name);

            // Assert
            Assert.That(isDuplicateName, Is.EqualTo(true));
        }

        [Test]
        public async Task SuggestionName_ShouldAcceptSuggestionFromRepository()
        {
            // Arrange
            List<SuggestionDto> suggestionDtos = new()
            {
                new SuggestionDto { Id = 1, UserId = "1029734", Name = "Bowlen", Description = "Bowlen om 8 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new SuggestionDto { Id = 2, UserId = "1029734", Name = "Padellen", Description = "Padellen om 3 uur in de middag", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Pending  },
                new SuggestionDto { Id = 3, UserId = "1029734", Name = "Film", Description = "Film kijken om 9 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Rejected },
                new SuggestionDto { Id = 4, UserId = "1029734", Name = "Karten", Description = "Karten om 4 uur in de avond", ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg", Date = null, ActivityState = ModelLayer.Enums.ActivityState.Accepted },
            };

            _suggestionTestRepository.InitializeSuggestions(suggestionDtos);

            Suggestion suggestion = new()
            {
                Id = 5,
                UserId = "1029734",
                Name = "tennis",
                Description = "Tennis om 3 uur in de middag",
                ImageUrl = "ef7837c1-02c6-4b76-83e1-84f00355d8b4.jpg",
                Date = null,
                ActivityState = (Talpa_DAL.Enums.ActivityState)ModelLayer.Enums.ActivityState.Pending
            };

            // Act
            bool isDuplicateName = await _suggestionService.SuggestionNameExistsAsync(suggestion.Name);

            // Assert
            Assert.That(isDuplicateName, Is.EqualTo(false));
        }
    }
}