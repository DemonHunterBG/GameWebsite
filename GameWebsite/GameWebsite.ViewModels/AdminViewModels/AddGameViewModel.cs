namespace GameWebsite.Web.ViewModels.AdminViewModels
{
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.Game;
    using static Common.EntityValidationMessages.Game;
    using static GameWebsite.Common.ApplicationConstants;

    public class AddGameViewModel
    {
        [Required(ErrorMessage = GameNameRequired)]
        [MinLength(GameNameMinLength)]
        [MaxLength(GameNameMaxLength)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = GameUrlRequired)]
        [MinLength(GameURLMinLength)]
        [MaxLength(GameURLMaxLength)]
        public string GameURL { get; set; } = string.Empty;

        [MaxLength(GameURLMaxLength)]
        public string? ImageURL { get; set; } = NoImageURL;

        [MaxLength(GameDescriptionMaxLength)]
        public string? Description { get; set; }

    }
}
