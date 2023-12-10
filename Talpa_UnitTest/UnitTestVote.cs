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
    public class UnitTestVote
    {
        private VoteService _voteService;
        private VoteTestRepository _voteTestRepository;

        [SetUp]
        public void Setup()
        {
            _voteTestRepository = new VoteTestRepository();
            _voteService = new VoteService(_voteTestRepository);
        }

        [Test]
        public async Task CreateVote_NewVote_ReturnsTrue()
        {
            // Arrange
            var vote = new Vote { Id = 1, UserId = "user1", SuggestionId = 1 };

            // Act
            Vote newVote = await _voteService.CreateVoteAsync(vote);

            // Assert
            Assert.That(newVote.ErrorMessage, Is.EqualTo(null));
        }

        [Test]
        public async Task CreateVote_DuplicateVote_ReturnsFalse()
        {
            // Arrange
            var vote = new Vote { Id = 1, UserId = "user1", SuggestionId = 1 };

            var voteDto = new VoteDto { Id = 1, UserId = "user1", SuggestionId = 1 };
            _voteTestRepository.InitializeVotes(new List<VoteDto> { voteDto }); // Initialize repository with existing vote

            // Act
            Vote newVote = await _voteService.CreateVoteAsync(vote);

            // Assert
            Assert.That(newVote.ErrorMessage, Is.EqualTo(null));
        }

        [Test]
        public async Task GetAllVotesBySuggestionId_ValidSuggestionId_ReturnsVotesList()
        {
            // Arrange
            var votes = new List<VoteDto>
            {
                new VoteDto { Id = 1, UserId = "user1", SuggestionId = 1 },
                new VoteDto { Id = 2, UserId = "user2", SuggestionId = 1 },
                new VoteDto { Id = 3, UserId = "user1", SuggestionId = 2 }
            };
            _voteTestRepository.InitializeVotes(votes);

            // Act
            var result = await _voteService.GetAllVotesBySuggestionAsync(1);

            // Assert
            Assert.That(result, Has.Count.EqualTo(2));
        }

        [Test]
        public async Task GetVoteCountBySuggestionId_ValidSuggestionId_ReturnsVoteCount()
        {
            // Arrange
            var votes = new List<VoteDto>
            {
                new VoteDto { Id = 1, UserId = "user1", SuggestionId = 1 },
                new VoteDto { Id = 2, UserId = "user2", SuggestionId = 1 },
                new VoteDto { Id = 3, UserId = "user1", SuggestionId = 2 }
            };
            _voteTestRepository.InitializeVotes(votes);

            // Act
            int voteCount = _voteService.GetVoteCountBySuggestionAsync(votes[0].SuggestionId);

            // Assert
            Assert.That(voteCount, Is.EqualTo(2));
        }

        [Test]
        public async Task GetVoteBySuggestionId_ValidVote_ReturnsVote()
        {
            // Arrange
            var vote = new VoteDto { Id = 1, UserId = "user1", SuggestionId = 1 };
            _voteTestRepository.InitializeVotes(new List<VoteDto> { vote });

            // Act
            var votes = await _voteService.GetAllVotesBySuggestionAsync(vote.SuggestionId);

            // Assert
            Assert.That(votes, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(votes[0].Id, Is.EqualTo(vote.Id));
                Assert.That(votes[0].UserId, Is.EqualTo(vote.UserId));
                Assert.That(votes[0].SuggestionId, Is.EqualTo(vote.SuggestionId));
            });
        }
    }
}
