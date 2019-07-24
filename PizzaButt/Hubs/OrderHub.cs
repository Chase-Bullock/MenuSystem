using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using CathedralKitchen.Models;

namespace CathedralKitchen.Hubs
{
    public class OrderHub : Hub
    {
        public async Task UpdateOrder()
        {
            await Clients.All.SendAsync("RecieveOrder");
        }

        //public async Task ReceiveOrder()
        //{
        //    await Clients.All.SendAsync("RecieveOrder");
        //}
    }
}