using GameWebsite.Data.Models;
using GameWebsite.Web.ViewModels.AdminViewModels;
using GameWebsite.Web.ViewModels.Artwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWebsite.Services.Data.Interfaces
{
    public interface IArtworkService
    {
        Task<IEnumerable<Artwork>> GetAllAsync();

        Task<Artwork> GetByIdAsync(int id);

        Task AddAsync(AddArtworkInputModel model);

        Task DeleteAsync(int id);
    }
}
