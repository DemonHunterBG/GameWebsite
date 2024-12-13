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
    public interface IGameService
    {
        Task<bool> CheckIfGameExists(int id);

        Task<IEnumerable<GameListViewModel>> GetAllWithQueryAsync(string currentUserId, string? searchQuery = null, string? genre = null);

        Task<IEnumerable<GameListViewModel>> GetAllFavoritesAsync(string currentUserId);

        Task<IEnumerable<GameManagementViewModel>> GetAllManagementAsync();

        Task<Game> GetByIdAsync(int id);

        Task<Game> GetByIdFavoritesAsync(int id);

        Task<GameViewModel> GetGameForPageByIdAsync(int id, string currentUserId);

        Task<AddGameViewModel> GetByIdForEditAsync(int id);

        Task AddAsync(AddGameViewModel model);

        Task AssignGenre(int gameId, int genreId);

        Task RemoveGenre(int gameId, int genreId);

        Task AddToFavoritesAsync(string UserId, int gameId);

        Task RemoveFromFavoritesAsync(string UserId, int gameId);

        Task UpdateAsync(Game entity, AddGameViewModel model);

        Task DeleteAsync(int id);
    }
}
