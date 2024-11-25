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
            public const int GameURLMinLength = 8;
            public const int GameURLMaxLength = 2048;
        }

        public static class Comment
        {
            public const int CommentMinLength = 2;
            public const int CommentMaxLength = 500;
        }

        public static class Post
        {
            public const int PostTitleMinLength = 2;
            public const int PostTitleMaxLength = 128;
            public const int PostMessageMinLength = 2;
            public const int PostMessageMaxLength = 2000;
        }

        public static class Artwork
        {
            public const int ArtworkTitleMinLength = 2;
            public const int ArtworkTitleMaxLength = 128;
            public const int ArtworkURLMinLength = 8;
            public const int ArtworkURLMaxLength = 2048;
        }
    }
}
