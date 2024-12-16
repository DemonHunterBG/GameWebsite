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
    public class GenreServiceTests
    {
        private IEnumerable<Genre> genreData = new List<Genre>()
        {
            new Genre()
            {
                Id = 1,
                GenreName = "Action",
            },
            new Genre()
            {
                Id = 2,
                GenreName = "Strategy",
            },
        };

        private Mock<IRepository<Genre, int>> genreRepository;
        private Mock<IRepository<GameGenre, object>> gameGenreRepository;

        [SetUp]
        public void Setup()
        {
            this.genreRepository = new Mock<IRepository<Genre, int>>();
            this.gameGenreRepository = new Mock<IRepository<GameGenre, object>>();
        }

        [Test]
        public async Task GetAllGenres()
        {
            IQueryable<Genre> genreMockQueryable = genreData.BuildMock();

            this.genreRepository
                .Setup(r => r.GetAllAttached())
                .Returns(genreMockQueryable);

            IGenreService genreService = new GenreService(genreRepository.Object, gameGenreRepository.Object);


            var genres = await genreService.GetAllAsync();


            Assert.AreEqual(genreData.Count(), genres.Count());
            Assert.AreEqual(genreData.First().Id, genres.First().Id);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetArtworkById(int id)
        {
            Genre result = genreData.First(x => x.Id == id);

            this.genreRepository
                .Setup(r => r.GetByIDAsync(id))
                .ReturnsAsync(result);

            IGenreService genreService = new GenreService(genreRepository.Object, gameGenreRepository.Object);


            var genre = await genreService.GetByIdAsync(id);


            Assert.AreEqual(id, genre.Id);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetArtworkByIdForEdit(int id)
        {
            IQueryable<Genre> genreMockQueryable = genreData.BuildMock();

            this.genreRepository
                .Setup(r => r.GetAllAttached())
                .Returns(genreMockQueryable);

            IGenreService genreService = new GenreService(genreRepository.Object, gameGenreRepository.Object);


            var genre = await genreService.GetByIdForEditAsync(id);


            Assert.AreEqual(genreMockQueryable.First(g => g.Id == id).GenreName, genre.GenreName);
        }
    }
}