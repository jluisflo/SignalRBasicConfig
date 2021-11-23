namespace SignalR_Started
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.SignalR;

    public class Chat : Hub
    {
        private readonly ILogger<Chat> _logger;

        public Chat(ILogger<Chat> logger)
        {
            _logger = logger;
        }

        public async Task UserConnected(NotifyMessage message)
        {
            await SendMessage(new NotifyMessage()
            {
                Author = message.Author,
                Message =$"{message.Author} has joined.",
                Avatar = message.Avatar,
            });
        }

        public async Task UserWrite(NotifyMessage message)
        {
            await SendMessage(new NotifyMessage()
            {
                Author = message.Author,
                Message = message.Message,
                Avatar = message.Avatar,
            });
        }

        private async Task SendMessage(NotifyMessage message)
        {
            _logger.LogInformation("publish message");
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
