using BitBuddy.Core.Dtos;
using BitBuddy.Core.Entities;

namespace BitBuddy.Core.Contracts.Data
{
    public interface IChatRepository
    {
        Task<Chat> CreateChat(string user1Id, string user2Id);
        void DeleteChat(int chatId);
        Task<List<ChatDto>> GetAllChatsOf(string userId);
        Task<Chat> GetChatById(int chatId);
        Task<Chat> EditChat(string chatName, int chatId);
    }
}
