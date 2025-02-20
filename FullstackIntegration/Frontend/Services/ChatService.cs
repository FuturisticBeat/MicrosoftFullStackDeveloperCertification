using Microsoft.AspNetCore.SignalR.Client;
using Shared.Models;

namespace Frontend.Services
{
    public class ChatService
    {
        private readonly HubConnection _hubConnection;
        
        public event Action<ChatMessage>? OnMessageReceived;
        
        public ChatService()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5083/chat")
                .WithAutomaticReconnect()
                .Build();
            
            _hubConnection.On<ChatMessage>("ReceiveMessage", message =>
            {
                OnMessageReceived?.Invoke(message);
            });

            _hubConnection.Closed += async error =>
            {
                // Reconnect after 5 seconds
                await Task.Delay(5000);
                await _hubConnection.StartAsync();
            };
        }
        
        public async Task StartAsync()
        {
            await _hubConnection.StartAsync();
        }
        
        public async Task SendMessageAsync(ChatMessage message)
        {
            await _hubConnection.SendAsync("SendMessage", message);
        }
    }
}