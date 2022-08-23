using BitBuddy.Core.Contracts.Data;
using BitBuddy.Core.Contracts.Domain;
using BitBuddy.Core.Dtos;
using BitBuddy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBuddy.Core.Services
{
    public class FriendFeedService : IFriendFeedService
    {
        private readonly IUserRepository _userRepository;
        private readonly IInterestRepository _interestRepository;
        private readonly IChatRepository _chatRepository;

        public FriendFeedService(IUserRepository userRepository, IInterestRepository interestRepository, IChatRepository chatRepository)
        {
            _userRepository = userRepository;
            _interestRepository = interestRepository;
            _chatRepository = chatRepository;
        }

        public async Task<List<ApplicationUser>> GetFriends(string id)
        {
            return await _userRepository.GetFriends(id);
        }
        public async Task<string> GetUserIdByUsername(string username)
        {
            return await _userRepository.GetUserIdByUsername(username);
        }

        public async Task<List<AppUserSuggestionDto>> GetMatchingSuggestions(string id)
        {
            var interests  = await _interestRepository.GetUserInterestsById(id);

            return await _userRepository.GetMatchingSuggestions(id, interests);
        }

        public void RemoveFriend(string userId, string friendId)
        {
            _userRepository.RemoveFriend(userId, friendId);
        }

        public async Task ReportUser(string userId, string description)
        {
            await _userRepository.ReportUser(userId, description);
        }

        public async Task<bool> SendMatchingResponse(string userId, string friendId, MatchingStatus status)
        {
            var matchingResult = await _userRepository.SendMatchingResponse(userId, friendId, status);
            if (matchingResult)
            {
                await _chatRepository.CreateChat(userId, friendId);
            }
            return matchingResult;
        }
    }
}
