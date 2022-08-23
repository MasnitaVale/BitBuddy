using BitBuddy.Core.Contracts.Common;
using BitBuddy.Core.Dtos;
using BitBuddy.Core.Entities;
using Microsoft.AspNetCore.SignalR;

namespace BitBuddy.Core.Services
{
    public class ChatHub : Hub, IChatHub
    {
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessage(MessageDto message, string groupName)
        {
            await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
        }
    }
}
