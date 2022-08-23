using BitBuddy.Core.Dtos;
using BitBuddy.Core.Entities;

namespace BitBuddy.Core.Contracts.Domain
{
    public interface IChatService
    {
        void DeleteChat(int chatId);
        Task<List<ChatDto>> GetAllChatsOf(string userId);
        Task<Chat> GetChatById(int chatId);
        Task<string> GetUserFirstNameLastNameByUsername(string username);
        Task<string> GetUserIdByUsername(string username);
        Task<Chat> EditChat(string chatName, int chatId);
    }
}
