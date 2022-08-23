using BitBuddy.Core.Dtos;
using BitBuddy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBuddy.Core.Contracts.Domain
{
    public interface IFriendFeedService
    {
        Task<List<ApplicationUser>> GetFriends(string id);
        Task<string> GetUserIdByUsername(string username);
        void RemoveFriend(string userId, string friendId);
        Task<List<AppUserSuggestionDto>> GetMatchingSuggestions(string id);
        Task<bool> SendMatchingResponse(string userId, string friendId, MatchingStatus status);
        Task ReportUser(string userId, string description);
    }
}
