using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameWebsite.Common.EntityValidationConstants.Genre;

namespace GameWebsite.Data.Models
{
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(GenreNameMinLength)]
        [MaxLength(GenreNameMaxLength)]
        public string GenreName { get; set; } = null!;

        public virtual IList<GameGenre> Games { get; set; } = new List<GameGenre>();
    }
}
