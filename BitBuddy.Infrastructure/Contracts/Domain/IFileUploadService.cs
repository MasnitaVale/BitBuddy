using BitBuddy.Core.Dtos;
using Microsoft.AspNetCore.Components.Forms;

namespace BitBuddy.Core.Contracts.Domain
{
    public interface IFileUploadService
    {
        Task<string> UploadUserProfilePicture(IBrowserFile file, string userId);
        Task<MessageDto> SendMessageWithPicture(IBrowserFile file, string userId, int chatId, string sender);
        Task<string> ShowPicture(IBrowserFile fileEntry);

    }
}
