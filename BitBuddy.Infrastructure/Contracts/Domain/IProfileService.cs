using BitBuddy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBuddy.Core.Contracts.Domain
{
    public interface IProfileService
    {
        Task<ApplicationUser> GetCurrentUser(string username);
        Task<ApplicationUser> EditUser(ApplicationUser user);
        Task<ApplicationUser> ChangeProfilePicture(ApplicationUser user);
        Task<List<Interest>> GetInterests();
    }
}
