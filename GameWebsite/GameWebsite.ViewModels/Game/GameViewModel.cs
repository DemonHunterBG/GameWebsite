using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWebsite.Web.ViewModels.Game
{
    public class GameViewModel
    {
        public required int Id { get; set; }

        public required string Name { get; set; }

        public required string GameURL { get; set; }

        public required bool IsGameURLWorking { get; set; }

        public string? Description { get; set; }

        public required DateTime AddedOn { get; set; }

        public required List<string> Genres { get; set; }

        public required List<GameCommentViewModel> Comments { get; set; }
    }
}
