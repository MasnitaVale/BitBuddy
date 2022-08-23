using BitBuddy.Core.Dtos;

namespace BitBuddy.Core.Contracts.Domain
{
    public interface IMessageService
    {
        void DeleteMessage(int messageId);
        Task<List<MessageDto>> GetMessagesFromChat(string userId, int chatId);
        Task<MessageDto> CreateMessageInChat(string userId, int chatId, string text, string sender);
    }
}
