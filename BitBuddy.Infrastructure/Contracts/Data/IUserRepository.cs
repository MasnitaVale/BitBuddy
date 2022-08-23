using BitBuddy.Core.Dtos;
using BitBuddy.Core.Entities;

namespace BitBuddy.Core.Contracts.Data
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetById(string id);
        Task<List<ApplicationUser>> GetAll();
        Task<string> GetUserFirstNameLastNameByUsername(string username);
        Task<ApplicationUser> GetByUsername(string username);
        Task<ApplicationUser> EditUser(ApplicationUser user);
        Task<string> GetUserIdByUsername(string username);
        Task<List<ApplicationUser>> GetFriends(string id);
        void RemoveFriend(string userId, string friendId);
        Task<bool> SendMatchingResponse(string userId, string friendId, MatchingStatus status);
        Task CreateFriendship(string userId, string friendId);
        Task<List<AppUserSuggestionDto>> GetMatchingSuggestions(string id, int[] interestIds);
        Task ReportUser(string userId, string description);
        Task DeleteUser(string userId);
        Task<string> UpdateUserProfilePicturePath(string userId, string picturePath);
    }
}
