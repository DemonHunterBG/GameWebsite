using GameWebsite.Data.Models;
using GameWebsite.Web.ViewModels.AdminViewModels;
using GameWebsite.Web.ViewModels.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWebsite.Services.Data.Interfaces
{
    public interface IGameCommentService
    {
        Task<GameComment> GetCommentByIdAsync(int id);

        Task AddCommentAsync(AddGameCommentViewModel model, int gameId, string userId);

        Task DeleteCommentAsync(int id);
    }
}
