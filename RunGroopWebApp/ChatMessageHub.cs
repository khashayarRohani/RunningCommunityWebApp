using Microsoft.AspNetCore.SignalR;

namespace RunGroopWebApp
{
    public class ChatMessageHub : Hub
    {
        public async Task SendMessage(string senderUsername, string message)
        {
            await this.Clients.All.SendAsync("ReceiveMessage",
                senderUsername, message);
        }
    }
}
