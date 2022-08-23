using BitBuddy.Core.Contracts.Data;
using BitBuddy.Core.Entities;
using BitBuddy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBuddy.Infrastructure.Repositories
{
    public class InterestRepository : IInterestRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public InterestRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Interest> AddInterest(string category)
        {
            var addedInterest = new Interest { Name = category };
            await _dbContext.Interests.AddAsync(addedInterest);
            await _dbContext.SaveChangesAsync();

            return addedInterest;
        }

        public void DeleteInterest(string category)
        {
            var interestToDelete = _dbContext.Interests.FirstOrDefault(i => i.Name == category);
            _dbContext.Interests.Remove(interestToDelete);
            _dbContext.SaveChangesAsync();
        }

        public async Task<Interest> GetInterestByName(string category)
        {
            return await _dbContext.Interests.FirstOrDefaultAsync(i => i.Name == category);
        }

        public async Task<List<Interest>> GetInterests()
        {
            return await _dbContext.Interests.ToListAsync();
        }

        public async Task<List<Interest>> GetUserInterests(string userId)
        {
            var interestList = await _dbContext.UserInterests
                            .Include(i => i.Interest)
                            .Where(x => x.UserId == userId)
                            .Select(x => x.Interest)
                            .ToListAsync();

            return interestList;
        }

        public async Task<int[]> GetUserInterestsById(string userId)
        {
            var interestArray = await _dbContext.UserInterests
                .Include(i => i.Interest)
                .Where(x => x.UserId == userId)
                .Select(x => x.InterestId)
                .ToArrayAsync();

            return interestArray;
        }
    }
}
