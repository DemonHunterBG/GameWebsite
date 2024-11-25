using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameWebsite.Common.EntityValidationConstants.Post;

namespace GameWebsite.Data.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(PostTitleMinLength)]
        [MaxLength(PostTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(PostMessageMinLength)]
        [MaxLength(PostMessageMaxLength)]
        public string Text { get; set; } = null!;

        [Required]
        public DateTime AddedOn { get; set; } = DateTime.Today;

        public string UserId { get; set; } = null!;

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; } = null!;

        public virtual IList<PostComment> Comments { get; set; } = new List<PostComment>();
    }
}
