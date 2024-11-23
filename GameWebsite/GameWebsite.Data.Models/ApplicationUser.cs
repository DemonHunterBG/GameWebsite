﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWebsite.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public virtual IList<ApplicationUserGame> FavoriteGames { get; set; } = new List<ApplicationUserGame>();

        public virtual IList<GameComment> GameComments { get; set; } = new List<GameComment>();

    }
}
