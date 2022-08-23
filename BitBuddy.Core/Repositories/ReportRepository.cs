using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitBuddy.Core.Entities;
using BitBuddy.Core.Contracts.Data;
using BitBuddy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BitBuddy.Infrastructure.Repositories
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ReportRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<Report>> GetReports()
        {
            var reports = await _dbContext.Reports
                .Include(u => u.User)
                .ToListAsync();
            return reports;
        }

        public async Task<List<Report>> GetUserReports(string userId)
        {
            var reports = await _dbContext.Reports
                .Where(x => x.User.Id == userId)
                .ToListAsync();
            return reports.ToList();
        }
    }
}
