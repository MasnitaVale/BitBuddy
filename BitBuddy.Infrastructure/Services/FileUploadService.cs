using BitBuddy.Core.Contracts.Data;
using BitBuddy.Core.Contracts.Domain;
using BitBuddy.Core.Dtos;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Hosting;

namespace BitBuddy.Core.Services
{
    public class FileUploadService : IFileUploadService
    {
        private readonly IHostingEnvironment _environment;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly int _maxFileSize = 1024 * 10240;

        public FileUploadService(IHostingEnvironment environment, IUserRepository userRepository, IMessageRepository messageRepository)
        {
            _environment = environment;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        public async Task<MessageDto> SendMessageWithPicture(IBrowserFile file, string userId, int chatId, string sender)
        {
            var newMessageId = await _messageRepository.CreateEmptyMessage(userId, chatId);
            var rootPath = Path.Combine(_environment.ContentRootPath, $"wwwroot\\");
            var filePath = $"Uploads\\{newMessageId}\\{file.Name}";
            var parentDirPath = Path.Combine(rootPath, $"Uploads\\{newMessageId}");
            var path = Path.Combine(rootPath, filePath);

            if (!Directory.Exists(parentDirPath))
            {
                Directory.CreateDirectory(parentDirPath);
            }
            try
            {
                await using FileStream fs = new(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                await file.OpenReadStream(_maxFileSize).CopyToAsync(fs);
                return await _messageRepository.UpdateMessagePicture(newMessageId, sender, filePath);
                
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> UploadUserProfilePicture(IBrowserFile fileEntry, string userId)
        {
            var rootPath = Path.Combine(_environment.ContentRootPath, $"wwwroot\\");
            var filePath = $"Uploads\\{userId}\\{fileEntry.Name}";
            var parentDirPath = Path.Combine(rootPath, $"Uploads\\{userId}");
            var path = Path.Combine(rootPath, filePath);

            if (!Directory.Exists(parentDirPath))
            {
                Directory.CreateDirectory(parentDirPath);
            }
            try
            {
                await using FileStream fs = new(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                await fileEntry.OpenReadStream(_maxFileSize).CopyToAsync(fs);
                return await _userRepository.UpdateUserProfilePicturePath(userId, filePath);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> ShowPicture(IBrowserFile fileEntry)
        {
            var rootPath = Path.Combine(_environment.ContentRootPath, $"wwwroot\\");
            var filePath = $"Uploads{fileEntry.Name}";
            var path = Path.Combine(rootPath, filePath);

            await using FileStream fs = new(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            await fileEntry.OpenReadStream(_maxFileSize).CopyToAsync(fs);

            return path;
        }

    }
}
