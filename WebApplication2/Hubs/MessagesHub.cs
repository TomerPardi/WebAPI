using Microsoft.AspNetCore.SignalR;

namespace WebAPI.Hubs
{

    public class MessagesHub : Hub
    {
        public async Task Changed(string user)
        {
            await Clients.Group(user).SendAsync("Changed");
        }

        public async Task Join(string user)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, user);
        }

        public async Task Unjoin(string user)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, user);
        }
    }
}
