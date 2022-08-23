using BitBuddy.Core.Dtos;
using BitBuddy.Core.Entities;

namespace BitBuddy.Core.Contracts.Common
{
    public interface IChatHub
    {
        Task SendMessage(MessageDto message, string groupName);
        Task JoinGroup(string groupName);
    }
}
