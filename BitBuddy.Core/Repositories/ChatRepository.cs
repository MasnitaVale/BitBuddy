using BitBuddy.Core.Entities;
using BitBuddy.Core.Contracts.Data;
using BitBuddy.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using BitBuddy.Core.Dtos;

namespace BitBuddy.Infrastructure.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ChatRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Chat> CreateChat(string user1Id, string user2Id)
        {
            var chat = new Chat { CreationDate = DateTime.UtcNow };
            await _dbContext.Chats.AddAsync(chat);
            await _dbContext.SaveChangesAsync();

            var userChat1 = new UserChat { ChatId = chat.Id, UserId = user1Id };
            var userChat2 = new UserChat { ChatId = chat.Id, UserId = user2Id };

            await _dbContext.UserChats.AddAsync(userChat1);
            await _dbContext.UserChats.AddAsync(userChat2);
            await _dbContext.SaveChangesAsync();

            return chat;
        }

        public void DeleteChat(int chatId)
        {
            var chatToDelete = _dbContext.Chats.FirstOrDefault(c => c.Id == chatId);
            _dbContext.Chats.Remove(chatToDelete);
            _dbContext.SaveChangesAsync();
        }

        public async Task<Chat> EditChat(string chatName, int chatId)
        {
            var chatToEdit = await _dbContext.Chats.FirstOrDefaultAsync(c => c.Id == chatId);
            chatToEdit.Name = chatName;
            await _dbContext.SaveChangesAsync();

            return chatToEdit;
        }

        public async Task<List<ChatDto>> GetAllChatsOf(string userId)
        {
            var userChat = _dbContext.UserChats
                .Include(x => x.Chat)
                .Where(c => c.UserId == userId)
                .Select(x => new ChatDto { Id = x.Chat.Id, Name = x.Chat.Name, CreationDate = x.Chat.CreationDate})
                .ToListAsync();

            return await userChat;
        }

        public async Task<Chat> GetChatById(int chatId)
        {
            return await _dbContext.Chats.FirstOrDefaultAsync(c => c.Id == chatId);
        }
    }
}
