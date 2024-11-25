using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static GameWebsite.Common.EntityValidationConstants.Comment;

namespace GameWebsite.Data.Models
{
    public class PostComment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(CommentMinLength)]
        [MaxLength(CommentMaxLength)]
        public string Text { get; set; } = null!;

        [Required]
        public DateTime AddedOn { get; set; } = DateTime.Today;

        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        public int PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; } = null!;
    }
}