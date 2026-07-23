using Microsoft.AspNetCore.SignalR;

namespace ElectronicsStore.Server.Hubs
{
    public class StoreHub : Hub
    {
        // Clients can connect to this Hub to receive real-time notifications
        // We can add methods here if clients need to send messages to the Hub, 
        // but for now we only need Server-to-Client push notifications.
        
        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
