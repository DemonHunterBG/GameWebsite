using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWebsite.Web.ViewModels.Game
{
    public class GameCommentViewModel
    {
        public required int Id { get; set; }

        public required string Text { get; set; }

        public required DateTime AddedOn { get; set; }

        public required string CreatorName { get; set; }

        public required bool IsCreator { get; set; }
    }
}
