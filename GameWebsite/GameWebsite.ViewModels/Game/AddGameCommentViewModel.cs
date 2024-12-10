using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWebsite.Web.ViewModels.Game
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Comment;
    using static Common.EntityValidationMessages.Comment;

    public class AddGameCommentViewModel
    {
        [Required(ErrorMessage = CommentTextRequired)]
        [MinLength(CommentMinLength)]
        [MaxLength(CommentMaxLength)]
        public string Text { get; set; } = string.Empty;

        public int GameId;
    }
}
