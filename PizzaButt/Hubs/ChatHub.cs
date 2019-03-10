using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CathedralKitchen.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task UpdateOrder()
        {
            await Clients.All.SendAsync("ReceiveOrder");
        }

        public async Task UpdateStatus()
        {
            await Clients.All.SendAsync("ReceiveStatus");
        }

    }
}
