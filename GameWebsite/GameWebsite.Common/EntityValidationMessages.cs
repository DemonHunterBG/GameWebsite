﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWebsite.Common
{
    public static class EntityValidationMessages
    {
        public static class Genre
        {
            public const string GenreNameRequired = "Genre name is required.";
        }

        public static class Game
        {
            public const string GameNameRequired = "Game name is required.";
            public const string GameUrlNameRequired = "Game Url is required.";
        }

        public static class Comment
        {
            public const string CommentTextRequired = "Comment message is required.";
        }
    }
}
