using GameWebsite.Data.Models;
using GameWebsite.Data.Repository;
using GameWebsite.Data.Repository.Interfaces;
using GameWebsite.Services.Data;
using GameWebsite.Services.Data.Interfaces;
using GameWebsite.Web.ViewModels.AdminViewModels;
using GameWebsite.Web.ViewModels.Artwork;
using MockQueryable;
using Moq;
using System.Data;

namespace GameWebsite.Services.Tests
{
    [TestFixture]
    public class GameCommentServiceTests
    {
        private IEnumerable<GameComment> gameCommentData = new List<GameComment>()
        {
            new GameComment()
            {
                Id = 1,
                Text = "Hello",
            },
            new GameComment()
            {
                Id = 2,
                Text = "Very cool!",
            },
        };

        private Mock<IRepository<GameComment, int>> gameCommentRepository;

        [SetUp]
        public void Setup()
        {
            this.gameCommentRepository = new Mock<IRepository<GameComment, int>>();
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetGameCommentById(int commentId)
        {
            GameComment expected = gameCommentData.First(x => x.Id == commentId);

            this.gameCommentRepository
                .Setup(r => r.GetByIDAsync(commentId))
                .ReturnsAsync(expected);

            IGameCommentService gameCommentService = new GameCommentService(gameCommentRepository.Object);


            var gameCommentResult = await gameCommentService.GetCommentByIdAsync(commentId);

            Assert.AreEqual(expected, gameCommentResult);
        }
    }
}