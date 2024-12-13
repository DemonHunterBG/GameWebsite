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

        Task AddAsync(AddGenreViewModel model);

        Task<AddGenreViewModel> GetByIdAttachedAsync(int id);

        Task<Genre> GetByIdAsync(int id);

        Task UpdateAsync(Genre entity, AddGenreViewModel model);

        Task DeleteAsync(int id);
    }
}
