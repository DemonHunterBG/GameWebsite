using GameWebsite.Data.Models;
using GameWebsite.Web.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWebsite.Services.Data.Interfaces
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreViewModel>> GetAllAsync();

        Task<AddGenreViewModel> GetByIdForEditAsync(int id);

        Task<Genre> GetByIdAsync(int id);

        Task AddAsync(AddGenreViewModel model);

        Task UpdateAsync(Genre entity, AddGenreViewModel model);

        Task DeleteAsync(int id);
    }
}
