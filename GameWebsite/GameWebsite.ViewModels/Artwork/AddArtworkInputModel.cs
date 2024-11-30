using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWebsite.Web.ViewModels.Artwork
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Artwork;
    using static Common.EntityValidationMessages.Artwork;

    public class AddArtworkInputModel
    {
        [Required(ErrorMessage = ArtworkTitleRequired)]
        [MinLength(ArtworkTitleMinLength)]
        [MaxLength(ArtworkTitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage =ArtworkURLRequired)]
        [MinLength(ArtworkURLMinLength)]
        [MaxLength(ArtworkTitleMaxLength)]
        public string ArtworkURL { get; set; } = null!;
    }
}
