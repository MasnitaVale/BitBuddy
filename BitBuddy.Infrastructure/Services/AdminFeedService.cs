using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitBuddy.Core.Contracts.Data;
using BitBuddy.Core.Contracts.Domain;
using BitBuddy.Core.Entities;

namespace BitBuddy.Core.Services
{
    public class AdminFeedService : IAdminFeedService
    {
        private readonly IUserRepository _userRepository;
        private readonly IReportRepository _reportRepository;
        private readonly IInterestRepository _interestRepository;

        public AdminFeedService(IUserRepository userRepository, IReportRepository reportRepository, IInterestRepository interestRepository)
        {
            _userRepository = userRepository;
            _reportRepository = reportRepository;
            _interestRepository = interestRepository;
        }

        public async Task<Interest> AddInterest(string category)
        {
            return await _interestRepository.AddInterest(category);
        }

        public void DeleteInterest(string category)
        {
            _interestRepository.DeleteInterest(category);
        }

        public async Task DeleteUser(string userId)
        {
            await _userRepository.DeleteUser(userId);
        }

        public async Task<List<ApplicationUser>> GetAll()
        {
            return await _userRepository.GetAll();
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            return await _userRepository.GetById(id);
        }

        public Task<Interest> GetInterestByName(string category)
        {
            return _interestRepository.GetInterestByName(category);
        }

        public async Task<List<Interest>> GetInterests()
        {
            return await _interestRepository.GetInterests();
        }

        public async Task<List<Report>> GetReports()
        {
            return await _reportRepository.GetReports();
        }

        public async Task<string> GetUserIdByUsername(string username)
        {
            return await _userRepository.GetUserIdByUsername(username);
        }

        public async Task<List<Report>> GetUserReports(string userId)
        {
            return await _reportRepository.GetUserReports(userId);
        }
    }
}
