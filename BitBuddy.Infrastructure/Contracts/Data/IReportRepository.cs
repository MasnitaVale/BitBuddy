using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitBuddy.Core.Entities;

namespace BitBuddy.Core.Contracts.Data
{
    public interface IReportRepository
    {
        Task<ApplicationUser> GetById(string id);
        Task<List<Report>> GetReports();
        Task<List<Report>> GetUserReports(string userId);
    }
}
