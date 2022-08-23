using BitBuddy.Core.Contracts.Data;
using BitBuddy.Core.Contracts.Domain;
using BitBuddy.Core.Dtos;
using BitBuddy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitBuddy.Core.Services
{
    public class ChatService : IChatService
    {
        private readonly IUserRepository _userRepository;
        private readonly IChatRepository _chatRepository;
        public ChatService(IUserRepository userRepository, IChatRepository chatRepository)
        {
            _userRepository = userRepository;
            _chatRepository = chatRepository;
        }
        public void DeleteChat(int chatId)
        {
            _chatRepository.DeleteChat(chatId);
        }

        public async Task<Chat> EditChat(string chatName, int chatId)
        {
            return await  _chatRepository.EditChat(chatName, chatId);
        }

        public async Task<List<ChatDto>> GetAllChatsOf(string userId)
        {
            return await _chatRepository.GetAllChatsOf(userId);
        }

        public async Task<Chat> GetChatById(int chatId)
        {
            return await _chatRepository.GetChatById(chatId);
        }

        public async Task<string> GetUserFirstNameLastNameByUsername(string username)
        {
            return await _userRepository.GetUserFirstNameLastNameByUsername(username);
        }

        public async Task<string> GetUserIdByUsername(string username)
        {
            return await _userRepository.GetUserIdByUsername(username);
        }
    }
}
