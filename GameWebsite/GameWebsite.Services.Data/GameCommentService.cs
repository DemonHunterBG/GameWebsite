using GameWebsite.Data.Models;
using GameWebsite.Data.Repository.Interfaces;
using GameWebsite.Services.Data.Interfaces;
using GameWebsite.Web.ViewModels.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWebsite.Services.Data
{
    public class GameCommentService : IGameCommentService
    {
        private readonly IRepository<GameComment, int> gameCommentsRepository;

        public GameCommentService(
            IRepository<GameComment, int> gameCommentsRepository)
        {
            this.gameCommentsRepository = gameCommentsRepository;
        }

        public async Task<GameComment> GetCommentByIdAsync(int id)
        {
            var model = await gameCommentsRepository.GetByIDAsync(id);

            return model;
        }

        public async Task AddCommentAsync(AddGameCommentViewModel model, int gameId, string userId)
        {
            GameComment comment = new GameComment()
            {
                Text = model.Text,
                UserId = userId,
                GameId = gameId,
            };

            await gameCommentsRepository.AddAsync(comment);
        }

        public async Task DeleteCommentAsync(int id)
        {
            await gameCommentsRepository.DeleteAsync(id);
        }
    }
}
