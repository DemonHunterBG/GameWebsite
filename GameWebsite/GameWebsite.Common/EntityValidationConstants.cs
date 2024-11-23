using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWebsite.Common
{
    public static class EntityValidationConstants
    {
        public static class Genre
        {
            public const int GenreNameMinLength = 2;
            public const int GenreNameMaxLength = 30;
        }

        public static class Game
        {
            public const int GameNameMinLength = 2;
            public const int GameNameMaxLength = 64;
            public const int GameDescriptionMaxLength = 2000;
            public const int URLMinLength = 10;
            public const int URLMaxLength = 2048;
        }

        public static class Comment
        {
            public const int CommentMinLength = 2;
            public const int CommentMaxLength = 500;
        }

    }
}
