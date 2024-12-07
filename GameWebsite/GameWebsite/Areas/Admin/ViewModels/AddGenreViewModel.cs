namespace GameWebsite.Web.Areas.Admin.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Genre;
    using static Common.EntityValidationMessages.Genre;

    public class AddGenreViewModel
    {
        [Required(ErrorMessage = GenreNameRequired)]
        [MinLength(GenreNameMinLength)]
        [MaxLength(GenreNameMaxLength)]
        public string GenreName { get; set; } = string.Empty;
    }
}

