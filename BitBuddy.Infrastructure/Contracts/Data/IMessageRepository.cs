using BitBuddy.Core.Dtos;

namespace BitBuddy.Core.Contracts.Data
{
    public interface IMessageRepository
    {
        Task<MessageDto> CreateMessageInChat(string userId, int chatId, string text, string sender);
        void DeleteMessage(int messageId);
        Task<List<MessageDto>> GetMessagesFromChat(string userId, int chatId);
        Task<MessageDto> UpdateMessagePicture(int messageId, string sender, string picturePath);
        Task<int> CreateEmptyMessage(string userId, int chatId);
    }
}
