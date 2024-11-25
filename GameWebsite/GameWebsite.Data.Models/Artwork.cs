using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameWebsite.Common.EntityValidationConstants.Artwork;

namespace GameWebsite.Data.Models
{
    public class Artwork
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(ArtworkTitleMinLength)]
        [MaxLength(ArtworkTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(ArtworkURLMinLength)]
        [MaxLength(ArtworkTitleMaxLength)]
        public string ArtworkURL { get; set; } = null!;

        [Required]
        public DateTime AddedOn { get; set; } = DateTime.Today;
    }
}
