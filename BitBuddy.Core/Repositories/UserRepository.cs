using BitBuddy.Core.Contracts.Data;
using BitBuddy.Core.Dtos;
using BitBuddy.Core.Entities;
using BitBuddy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BitBuddy.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ApplicationUser> EditUser(ApplicationUser user)
        {
            var userToEdit = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
            userToEdit = user;
            await _dbContext.SaveChangesAsync();

            return userToEdit;
        }

        public async Task<ApplicationUser> GetById(string id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<ApplicationUser> GetByUsername(string username)
        {
            return await _dbContext.Users
                .Include(u => u.UserInterests).ThenInclude(ui => ui.Interest)
                .FirstOrDefaultAsync(u => u.UserName == username);
        }

        public async Task<List<ApplicationUser>> GetFriends(string id)
        {
            return await _dbContext.UserFriends
                .Include(x => x.User2)
                .Where(x => x.User1Id == id)
                .Select(x => x.User2)
                .ToListAsync();
        }

        public async Task<List<AppUserSuggestionDto>> GetMatchingSuggestions(string id, int[] interestIds)
        {
            var friendsIds = await _dbContext.UserFriends
                .Where(uf => uf.User1Id == id)
                .Select(uf => uf.User2Id)
                .ToListAsync();

            var existingMatchesIds = await _dbContext.UserMatchings
                .Where(uf => uf.User1Id == id)
                .Select(uf => uf.User2Id)
                .ToListAsync();

            var reportedUsersIds = await _dbContext.Reports
                .Where(ru => ru.User.Id != id)
                .Select(ru => ru.User.Id)
                .ToListAsync();

            var suggestions = await _dbContext.UserInterests
                .Include(ui => ui.User).ThenInclude(i => i.UserInterests).ThenInclude(x => x.Interest)
                .Where(ui => interestIds.Any(x => x == ui.InterestId) && ui.UserId != id)
                .Select(ui => ui.User)
                .ToListAsync();

            var filteredSuggestions = suggestions.Where(u =>
                !friendsIds.Any(x => x == u.Id) &&
                !existingMatchesIds.Any(x => x == u.Id) &&
                !reportedUsersIds.Any(x => x == u.Id)
                );

            return filteredSuggestions.Distinct().Select(x => new AppUserSuggestionDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                ProfilePicturePath = x.ProfilePicturePath,
                NickName = x.NickName,
                Age = x.Age,
                PhoneNumber = x.PhoneNumber,
                Introduction = x.Introduction,
                UserInterests = x.UserInterests,
                Location = x.Location,
                Studies = x.Studies
            }).ToList();
        }

        public async Task<string> GetUserIdByUsername(string username)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
            return user?.Id;
        }

        public void RemoveFriend(string userId, string friendId)
        {
            var userChatToDelete = _dbContext.UserChats.FirstOrDefault(u => u.UserId == friendId);
            _dbContext.UserChats.Remove(userChatToDelete);

            var friendshipToDelete1 = _dbContext.UserFriends.FirstOrDefault(u => u.User2Id == friendId && u.User1Id == userId);
            _dbContext.UserFriends.Remove(friendshipToDelete1);
            // reciproc
            var friendshipToDelete2 = _dbContext.UserFriends.FirstOrDefault(u => u.User1Id == friendId && u.User2Id == userId);
            _dbContext.UserFriends.Remove(friendshipToDelete2);

            var matchingToDelete = _dbContext.UserMatchings.FirstOrDefault(u => u.User1Id == friendId && u.User2Id == userId);
            _dbContext.UserMatchings.Remove(matchingToDelete);

            _dbContext.SaveChanges();
        }

        public async Task<bool> SendMatchingResponse(string userId, string friendId, MatchingStatus status)
        {
            var existingMatching = _dbContext.UserMatchings.FirstOrDefault(u => u.User1Id == friendId && u.User2Id == userId);
            await _dbContext.UserMatchings.AddAsync(new UserMatching { User1Id = userId, User2Id = friendId, Status = status });
            await _dbContext.SaveChangesAsync();
            bool isMatch = false;

            if (existingMatching != null)
            {
                if (status == MatchingStatus.Accepted && existingMatching.Status == MatchingStatus.Accepted)
                {
                    await CreateFriendship(userId, friendId);
                    isMatch = true;
                }
            }
            return isMatch;
        }

        public async Task CreateFriendship(string userId, string friendId)
        {
            await _dbContext.UserFriends.AddAsync(new UserFriend { User1Id = userId, User2Id = friendId });
            await _dbContext.UserFriends.AddAsync(new UserFriend { User1Id = friendId, User2Id = userId });
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string> GetUserFirstNameLastNameByUsername(string username)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
            return user?.FirstName + " " + user?.LastName;
        }

        public async Task ReportUser(string userId, string description)
        {
            var reportedUser = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            await _dbContext.Reports.AddAsync(new Report { User = reportedUser , ReasonDescription = description });
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteUser(string userId)
        {
            var userToDelete = await _dbContext.Users.FirstOrDefaultAsync(i => i.Id == userId);

            //var userchatsToDelete = await _dbContext.UserChats
            //.Where(uc => uc.UserId == userId)
            //.ToListAsync();

            var friendsToDelete = await _dbContext.UserFriends
            .Where(uf => uf.User1Id == userId || uf.User2Id == userId)
            .ToListAsync();

            var matchingsToDelete = await _dbContext.UserMatchings
                .Where(uf => uf.User1Id == userId || uf.User2Id == userId)
                .ToListAsync();

            //_dbContext.UserChats.RemoveRange(userchatsToDelete);
            _dbContext.UserFriends.RemoveRange(friendsToDelete);
            _dbContext.UserMatchings.RemoveRange(matchingsToDelete);
            _dbContext.Users.Remove(userToDelete);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<ApplicationUser>> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<string> UpdateUserProfilePicturePath(string userId, string picturePath)
        {
            var user =  await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            user.ProfilePicturePath = picturePath;
            await _dbContext.SaveChangesAsync();
            return user.ProfilePicturePath;
        }
    }
}
