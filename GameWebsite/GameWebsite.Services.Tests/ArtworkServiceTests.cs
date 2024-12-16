using GameWebsite.Data.Models;
using GameWebsite.Data.Repository.Interfaces;
using GameWebsite.Services.Data;
using GameWebsite.Services.Data.Interfaces;
using GameWebsite.Web.ViewModels.Artwork;
using MockQueryable;
using Moq;
using System.Data;

namespace GameWebsite.Services.Tests
{
    [TestFixture]
    public class ArtworkServiceTests
    {
        private IEnumerable<Artwork> artworksData = new List<Artwork>()
        {
            new Artwork()
            {
                Id = 1,
                Title = "Forest",
                AddedOn = DateTime.Now,
                ArtworkURL = "/images/artworks/Forest.png"
            },
            new Artwork()
            {
                Id = 2,
                Title = "Snel",
                AddedOn = DateTime.Now,
                ArtworkURL = "/images/artworks/Snel.png"
            }
        };

        private Mock<IRepository<Artwork, int>> artworkRepository;

        [SetUp]
        public void Setup()
        {
            this.artworkRepository = new Mock<IRepository<Artwork, int>>();
        }

        [Test]
        public async Task GetAllArtworks()
        {
            IQueryable<Artwork> artworksMockQueryable = artworksData.BuildMock();

            this.artworkRepository
                .Setup(r => r.GetAllAttached())
                .Returns(artworksMockQueryable);

            IArtworkService artworkService = new ArtworkService(artworkRepository.Object);


            IEnumerable<Artwork> allArtworks = await artworkService.GetAllAsync();


            Assert.AreEqual(artworksData, allArtworks);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public async Task GetArtworkById(int id)
        {
            Artwork result = artworksData.First(x => x.Id == id);

            this.artworkRepository
                .Setup(r => r.GetByIDAsync(id))
                .ReturnsAsync(result);

            IArtworkService artworkService = new ArtworkService(artworkRepository.Object);


            Artwork artwork = await artworkService.GetByIdAsync(id);


            Assert.AreEqual(id, artwork.Id);
        }
    }
}