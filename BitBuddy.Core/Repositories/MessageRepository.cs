using BitBuddy.Core.Entities;
using BitBuddy.Core.Contracts.Data;
using BitBuddy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BitBuddy.Core.Dtos;

namespace BitBuddy.Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public MessageRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MessageDto> CreateMessageInChat(string userId, int chatId, string text, string sender)
        {
            var message = new Message { CreationDate = DateTime.UtcNow, UserId = userId, ChatId = chatId, Text = text };
            await _dbContext.Messages.AddAsync(message);
            await _dbContext.SaveChangesAsync();

            return new MessageDto { Date = message.CreationDate, UserId = message.UserId, Text = message.Text, Name = sender, ChatId = chatId };
        }

        public void DeleteMessage(int messageId)
        {
            var messageToDelete = _dbContext.Messages.FirstOrDefault(m => m.Id == messageId);
            if (messageToDelete != null)
                _dbContext.Messages.Remove(messageToDelete);
            _dbContext.SaveChangesAsync();
        }

        public async Task<List<MessageDto>> GetMessagesFromChat(string userId, int chatId)
        {
            var chatMessages = _dbContext.Messages
                .Where(m => m.ChatId == chatId)
                .Include(m => m.User)
                .Select(message =>
                new MessageDto
                {
                    Date = message.CreationDate,
                    UserId = message.UserId,
                    Text = message.Text,
                    Name = $"{message.User.FirstName} {message.User.LastName}",
                    PicturePath = message.PicturePath,
                    ChatId = chatId
                })
                .ToListAsync();

            return await chatMessages;
        }

        public async Task<MessageDto> UpdateMessagePicture(int messageId,  string sender, string picturePath)
        {
            var message = await _dbContext.Messages.FirstOrDefaultAsync(m => m.Id == messageId);
            message.PicturePath = picturePath;
            await _dbContext.SaveChangesAsync();
            return new MessageDto { Date = message.CreationDate, UserId = message.UserId,  Name = sender, PicturePath = picturePath, ChatId = message.ChatId };
        }

        public async Task<int> CreateEmptyMessage(string userId, int chatId)
        {
            var message = new Message { CreationDate = DateTime.UtcNow, UserId = userId, ChatId = chatId };
            await _dbContext.Messages.AddAsync(message);
            await _dbContext.SaveChangesAsync();
            return message.Id;
        }

    }
}
