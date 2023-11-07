using ModelLayer.Models;
using Talpa_BLL.Interfaces;
using Talpa_BLL.Models;
using Talpa_BLL.Services;
using Talpa_UnitTest.StubRepositories;

namespace Talpa_UnitTest
{
    public class LimitationTests
    {
        private LimitationService _limitationService;
        private LimitationTestRepository _limitationTestRepository;

        [SetUp]
        public void Setup()
        {
            _limitationTestRepository = new LimitationTestRepository();
            _limitationService = new LimitationService(_limitationTestRepository);
        }

        [Test]
        public async Task GetLimitationsBySuggestionId_ShouldGetLimitationsByIdFromRepository()
        {
            // Arrange
            List<LimitationDto> limitationDtos = new()
            {
                new LimitationDto { Id = 1, Name = "Niet vega" },
                new LimitationDto { Id = 2, Name = "Niet berijkbaar met rolstoel" },
            };

            List<ActivityLimitationDto> activityLimitationDtos = new()
            {
                new ActivityLimitationDto { Id = 1, SuggestionId = 1, LimitationId = 2 },
            };

            _limitationTestRepository.InitializeLimitations(limitationDtos);
            _limitationTestRepository.InitializeActivityLimitations(activityLimitationDtos);

            // Act
            List<Limitation> limitations = await _limitationService.GetLimitationsBySuggestionId(1);

            // Assert
            Assert.That(limitations, Has.Count.EqualTo(1));
        }

        [Test]
        public async Task CreateLimitation_ShouldAddLimitationToRepository()
        {
            // Arrange
            List<LimitationDto> limitationDtos = new()
            {
                new LimitationDto { Id = 1, Name = "Niet vega" },
                new LimitationDto { Id = 2, Name = "Niet berijkbaar met rolstoel" },
            };

            _limitationTestRepository.InitializeLimitations(limitationDtos);

            Limitation limitation = new()
            {
                Id = 5,
                Name = "Is te laat!"
            };

            // Act
            Limitation limitationAdded = await _limitationService.CreateLimitationAsync(limitation);

            // Assert
            Assert.That(limitationAdded, Is.Not.Null);
        }

        [Test]
        public async Task CreateActivityLimitation_ShouldAddActiivtyLimitationToRepository()
        {
            // Arrange
            List<LimitationDto> limitationDtos = new()
            {
                new LimitationDto { Id = 1, Name = "Niet vega" },
                new LimitationDto { Id = 2, Name = "Niet berijkbaar met rolstoel" },
            };

            List<ActivityLimitationDto> activityLimitationDtos = new()
            {
                new ActivityLimitationDto { Id = 1, SuggestionId = 1, LimitationId = 2 },
            };

            _limitationTestRepository.InitializeLimitations(limitationDtos);

            Limitation limitation = new()
            {
                Id = 3,
                Name = "Is te laat!"
            };

            // Act
            bool activityLimitationAdded = await _limitationService.CreateActivityLimitationAsync(limitation, 2);

            // Assert
            Assert.That(activityLimitationAdded, Is.EqualTo(true));
        }
    }
}