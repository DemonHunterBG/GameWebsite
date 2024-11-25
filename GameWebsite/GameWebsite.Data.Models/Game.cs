using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameWebsite.Common.EntityValidationConstants.Game;
using static GameWebsite.Common.ApplicationConstants;

namespace GameWebsite.Data.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(GameNameMinLength)]
        [MaxLength(GameNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MinLength(GameURLMinLength)]
        [MaxLength(GameURLMaxLength)]
        public string GameURL { get; set; } = null!;

        [MaxLength(GameURLMaxLength)]
        public string? ImageURL { get; set; } = NoImageURL;

        [MaxLength(GameDescriptionMaxLength)]
        public string? Description { get; set; }

        [Required]
        public DateTime AddedOn { get; set; } = DateTime.Today;

        public virtual IList<GameGenre> Genres { get; set; } = new List<GameGenre>();

        public virtual IList<ApplicationUserGame> Favorites { get; set; } = new List<ApplicationUserGame>();

        public virtual IList<GameComment> Comments { get; set; } = new List<GameComment>();

    }
}
