using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitBuddy.Core.Entities;

namespace BitBuddy.Core.Contracts.Domain
{
    public interface IAdminFeedService
    {
        Task<List<Report>> GetReports();
        Task<ApplicationUser> GetById(string id);
        Task<Interest> AddInterest(string category);
        void DeleteInterest(string category);
        Task<List<Interest>> GetInterests();
        Task<Interest> GetInterestByName(string category);
        Task DeleteUser(string userId);
        Task<string> GetUserIdByUsername(string username);
        Task<List<ApplicationUser>> GetAll();
        Task<List<Report>> GetUserReports(string userId);

    }
}
