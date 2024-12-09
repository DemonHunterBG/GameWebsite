using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWebsite.Web.ViewModels.Game
{
    public class GameListViewModel
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public required string ImageURL { get; set; }

        public required bool HasFavored { get; set; }
    }
}
