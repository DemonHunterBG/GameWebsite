using GameWebsite.Data.Models;
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
    public class GameServiceTests
    {
        private IEnumerable<Game> gameData = new List<Game>()
        {
            new Game()
            {
                Id = 1,
                Name = "Demonicas",
                GameURL = "http://127.0.0.1/DemonicaS",
                ImageURL = "http://127.0.0.1/DemonicaS/Icon.png",
                Description = "You are a demon. The last of your kind. Cursed to fight to the bloody end again and again. Humanity on one side and abominations on the other.\r\n\r\n\r\nFIGHT ON!!!\r\n\r\nMade for GameDev.tv Game Jam 2024\r\n\r\nTheme: Last Stand\r\n\r\nLeave a comment with the wave you reached!\r\n\r\nIf you like this game, consider checking out my other ones!",
                Genres =
                {
                    new GameGenre()
                    {
                        GameId = 1,
                        GenreId = 1,
                        Genre = new Genre()
                        {
                            Id = 1,
                            GenreName = "Action"
                        }
                    },
                    new GameGenre()
                    {
                        GameId = 1,
                        GenreId = 2,
                        Genre = new Genre()
                        {
                            Id = 2,
                            GenreName = "Strategy"
                        }
                    },
                },
                Favorites =
                {
                    new ApplicationUserGame()
                    {
                        UserId = "user1",
                        GameId = 1,
                    },
                    new ApplicationUserGame()
                    {
                        UserId = "user2",
                        GameId = 1,
                    },
                }
            },
            new Game()
            {
                Id = 2,
                Name = "Oil Tycoon",
                GameURL = "http://127.0.0.1/Oil Tycoon Webgl Uncompressed/",
                ImageURL = "http://127.0.0.1/Oil Tycoon Webgl Uncompressed/Icon.png",
                Description = "Made for Fireside Jam 2024\r\n\r\nBuild and expand your oil business to 3 different locations!\r\n\r\nIncludes:\r\n\r\n3 Different locations\r\n13 Structures to build\r\n16 Challenges to complete\r\nMusic - public domain verion of the 1718-1720 'Winter' by Antonio Vivaldi",
                Genres =
                {
                    new GameGenre()
                    {
                        GameId = 2,
                        GenreId = 2,
                        Genre = new Genre()
                        {
                            Id = 2,
                            GenreName = "Strategy"
                        }
                    },
                },
                Favorites =
                {
                    new ApplicationUserGame()
                    {
                        UserId = "user1",
                        GameId = 2,
                    },
                }
            },
        };

        private Mock<IRepository<Game, int>> gameRepository;
        private Mock<IRepository<Genre, int>> genreRepository;
        private Mock<IRepository<GameGenre, object>> gameGenreRepository;
        private Mock<IRepository<ApplicationUserGame, object>> applicationUserGamesRepository;
        private Mock<IRepository<GameComment, int>> gameCommentsRepository;

        [SetUp]
        public void Setup()
        {
            this.gameRepository = new Mock<IRepository<Game, int>>();
            this.genreRepository = new Mock<IRepository<Genre, int>>();
            this.gameGenreRepository = new Mock<IRepository<GameGenre, object>>();
            this.applicationUserGamesRepository = new Mock<IRepository<ApplicationUserGame, object>>();
            this.gameCommentsRepository = new Mock<IRepository<GameComment, int>>();
        }

        [Test]
        public async Task GetAllGamesWithNoQuery()
        {
            int expectedCount = 2;

            IQueryable<Game> gameMockQueryable = gameData.BuildMock();

            this.gameRepository
                .Setup(r => r.GetAllAttached())
                .Returns(gameMockQueryable);

            IGameService gameService = new GameService(gameRepository.Object, genreRepository.Object, gameGenreRepository.Object, applicationUserGamesRepository.Object, gameCommentsRepository.Object);


            var gamesResult = await gameService.GetAllWithQueryAsync();

            Assert.AreEqual(expectedCount, gamesResult.Count());
        }

        [Test]
        [TestCase(0, "Alabala")]
        [TestCase(1, "Dem")]
        [TestCase(1, "dem")]
        [TestCase(2, "o")]
        public async Task GetAllGamesWithSearchQuery(int expectedCount, string searchQuery)
        {
            IQueryable<Game> gameMockQueryable = gameData.BuildMock();

            this.gameRepository
                .Setup(r => r.GetAllAttached())
                .Returns(gameMockQueryable);

            IGameService gameService = new GameService(gameRepository.Object, genreRepository.Object, gameGenreRepository.Object, applicationUserGamesRepository.Object, gameCommentsRepository.Object);


            var gamesResult = await gameService.GetAllWithQueryAsync(searchQuery: searchQuery);

            if (expectedCount == 0 && gamesResult.Count() == 0)
            {
                Assert.Pass();
            }
            else
            {
                Assert.AreEqual(expectedCount, gamesResult.Count());
            }
        }

        [Test]
        [TestCase(0, "MMO")]
        [TestCase(1, "Action")]
        [TestCase(2, "Strategy")]
        public async Task GetAllGamesWithGenre(int expectedCount, string genre)
        {

            IQueryable<Game> gameMockQueryable = gameData.BuildMock();

            this.gameRepository
                .Setup(r => r.GetAllAttached())
                .Returns(gameMockQueryable);

            IGameService gameService = new GameService(gameRepository.Object, genreRepository.Object, gameGenreRepository.Object, applicationUserGamesRepository.Object, gameCommentsRepository.Object);


            var gamesResult = await gameService.GetAllWithQueryAsync(genre: genre);


            if (expectedCount == 0 && gamesResult.Count() == 0)
            {
                Assert.Pass();
            }
            else
            {
                Assert.AreEqual(expectedCount, gamesResult.Count());
            }
        }

        [Test]
        [TestCase(0, "Alabala", "MMO")]
        [TestCase(0, "Dem", "MMO")]
        [TestCase(1, "Dem", "Action")]
        [TestCase(2, "o", "Strategy")]
        public async Task GetAllGamesWithFullSearch(int expectedCount, string searchQuery , string genre)
        {

            IQueryable<Game> gameMockQueryable = gameData.BuildMock();

            this.gameRepository
                .Setup(r => r.GetAllAttached())
                .Returns(gameMockQueryable);

            IGameService gameService = new GameService(gameRepository.Object, genreRepository.Object, gameGenreRepository.Object, applicationUserGamesRepository.Object, gameCommentsRepository.Object);


            var gamesResult = await gameService.GetAllWithQueryAsync(searchQuery: searchQuery, genre: genre);


            if (expectedCount == 0 && gamesResult.Count() == 0)
            {
                Assert.Pass();
            }
            else
            {
                Assert.AreEqual(expectedCount, gamesResult.Count());
            }
        }

        [Test]
        [TestCase(1, "user2")]
        [TestCase(2, "user1")]
        public async Task GetAllFavoriteGames(int expectedCount, string userId)
        {
            IQueryable<Game> gameMockQueryable = gameData.BuildMock();

            this.gameRepository
                .Setup(r => r.GetAllAttached())
                .Returns(gameMockQueryable);

            IGameService gameService = new GameService(gameRepository.Object, genreRepository.Object, gameGenreRepository.Object, applicationUserGamesRepository.Object, gameCommentsRepository.Object);


            var gamesResult = await gameService.GetAllFavoritesAsync(userId);

            Assert.AreEqual(expectedCount, gamesResult.Count());
        }

        [Test]
        public async Task GetAllGamesManagement()
        {
            int expectedCount = 2;

            IQueryable<Game> gameMockQueryable = gameData.BuildMock();

            this.gameRepository
                .Setup(r => r.GetAllAttached())
                .Returns(gameMockQueryable);

            IGameService gameService = new GameService(gameRepository.Object, genreRepository.Object, gameGenreRepository.Object, applicationUserGamesRepository.Object, gameCommentsRepository.Object);


            var gamesResult = await gameService.GetAllManagementAsync();

            Assert.AreEqual(expectedCount, gamesResult.Count());
        }


        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetGameById(int gameId)
        {
            Game expected = gameData.First(x => x.Id == gameId);

            this.gameRepository
                .Setup(r => r.GetByIDAsync(gameId))
                .ReturnsAsync(expected);

            IGameService gameService = new GameService(gameRepository.Object, genreRepository.Object, gameGenreRepository.Object, applicationUserGamesRepository.Object, gameCommentsRepository.Object);


            var gameResult = await gameService.GetByIdAsync(gameId);

            Assert.AreEqual(expected, gameResult);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetGameForPageById(int gameId)
        {
            Game expected = gameData.First(x => x.Id == gameId);

            IQueryable<Game> gameMockQueryable = gameData.BuildMock();

            this.gameRepository
                .Setup(r => r.GetAllAttached())
                .Returns(gameMockQueryable);

            IGameService gameService = new GameService(gameRepository.Object, genreRepository.Object, gameGenreRepository.Object, applicationUserGamesRepository.Object, gameCommentsRepository.Object);


            var gameResult = await gameService.GetGameForPageByIdAsync(gameId, "userId");

            Assert.AreEqual(expected.Id, gameResult.Id);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetGameForEditById(int gameId)
        {
            Game expected = gameData.First(x => x.Id == gameId);

            IQueryable<Game> gameMockQueryable = gameData.BuildMock();

            this.gameRepository
                .Setup(r => r.GetAllAttached())
                .Returns(gameMockQueryable);

            IGameService gameService = new GameService(gameRepository.Object, genreRepository.Object, gameGenreRepository.Object, applicationUserGamesRepository.Object, gameCommentsRepository.Object);


            var gameResult = await gameService.GetByIdForEditAsync(gameId);

            Assert.AreEqual(expected.Name, gameResult.Name);
        }
    }
}