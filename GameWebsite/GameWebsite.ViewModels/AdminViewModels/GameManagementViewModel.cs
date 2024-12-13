using GameWebsite.Data.Models;

namespace GameWebsite.Web.ViewModels.AdminViewModels
{
    public class GameManagementViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public List<Genre> AllGenres { get; set; } = new List<Genre>();
        public List<Genre> Genres { get; set; } = new List<Genre>();
    }
}
