using GameWebsite.Web.ViewModels.AdminViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameWebsite.Services.Data.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllAsync();

        Task AssignRoleAsync(string userId, string role);

        Task RemoveRoleAsync(string userId, string role);

        Task DeleteUserAsync(string userId);

    }
}
