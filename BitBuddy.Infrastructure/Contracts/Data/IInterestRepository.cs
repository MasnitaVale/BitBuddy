using BitBuddy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBuddy.Core.Contracts.Data
{
    public interface IInterestRepository
    {
        Task<List<Interest>> GetInterests();
        Task<Interest> GetInterestByName(string category);
        Task<int[]> GetUserInterestsById(string userId);
        Task<List<Interest>> GetUserInterests(string userId);
        Task<Interest> AddInterest(string category);
        void DeleteInterest(string category);
    }
}
