using Microsoft.AspNetCore.SignalR;
using Shared.Models;

namespace Backend.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(ChatMessage chatMessage)
        {
            chatMessage.Timestamp = DateTime.UtcNow;
            await Clients.All.SendAsync("ReceiveMessage", chatMessage);
        }
    }
}