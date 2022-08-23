using BitBuddy.Core.Contracts.Data;
using BitBuddy.Core.Contracts.Domain;
using BitBuddy.Core.Dtos;

namespace BitBuddy.Core.Services
{
    public class MessageService : IMessageService
    {
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository;

        public MessageService(IMessageRepository messageRepository, IUserRepository userRepository, IChatRepository chatRepository )
        {
            _userRepository = userRepository;
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
        }

        public async Task<MessageDto> CreateMessageInChat(string userId, int chatId, string text, string sender)
        {
            return await _messageRepository.CreateMessageInChat(userId, chatId, text, sender);
        }

        public void DeleteMessage(int messageId)
        {
            _messageRepository.DeleteMessage(messageId);
        }

        public async Task<List<MessageDto>> GetMessagesFromChat(string userId, int chatId)
        {
            return await _messageRepository.GetMessagesFromChat(userId, chatId);
        }
    }
}
