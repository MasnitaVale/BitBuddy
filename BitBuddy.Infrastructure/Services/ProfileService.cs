using BitBuddy.Core.Contracts.Data;
using BitBuddy.Core.Contracts.Domain;
using BitBuddy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBuddy.Core.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;
        private readonly IInterestRepository _interestRepository;

        public ProfileService(IInterestRepository interestRepository, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _interestRepository = interestRepository;
        }

        public Task<ApplicationUser> ChangeProfilePicture(ApplicationUser user)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationUser> EditUser(ApplicationUser user)
        {
            return _userRepository.EditUser(user);
        }

        public async Task<ApplicationUser> GetCurrentUser(string username)
        {
            return await _userRepository.GetByUsername(username);
        }

        public async Task<List<Interest>> GetInterests()
        {
            return await _interestRepository.GetInterests();
        }
    }
}
