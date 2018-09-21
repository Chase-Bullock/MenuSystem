using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using PizzaButt.Models;

namespace PizzaButt.Hubs
{
    public class ChatHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("SendAction", "connected");
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            await Clients.All.SendAsync("SendAction", "disconnected");
        }

        public async Task Send(OrderModel order)
        {
            await Clients.All.SendAsync("SendMessage", order.Name, order);
        }
    }
}