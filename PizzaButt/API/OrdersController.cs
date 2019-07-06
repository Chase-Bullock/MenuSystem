using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CathedralKitchen.NewModels;
using CathedralKitchen.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CathedralKitchen.API
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly CathedralKitchenContext _ctx;
        private readonly ICathedralKitchenRepository _cathedralKitchenRepository;

        public OrdersController(ICathedralKitchenRepository cathedralKitchenRepository, CathedralKitchenContext ctx)
        {
            _ctx = ctx;
            _cathedralKitchenRepository = cathedralKitchenRepository;
        }

        
        [HttpGet]
        public async Task<IActionResult> GetStatusOfAllOrders()
        {
            var orders = _ctx.Order.Where(x => x.OrderStatusId != 20002 && x.OrderStatusId != 2)
                .Include(y => y.OrderItem).ThenInclude(z => z.OrderItemTopping).ThenInclude(v => v.Topping)
                .Include(c => c.OrderItem).ThenInclude(w => w.MenuItem)
                .Include(y => y.OrderStatus).Include(c => c.Community);

            //var orderItems = _ctx.OrderItem.Include(y => y.MenuItem).Include(y => y.OrderItemTopping).ThenInclude(y => y.Topping).ToList();
            var orderItemsViewModel = new Dictionary<long, List<OrderItemViewModel>>();
            var ordersViewModel = new List<OrderViewModel>();

            foreach (var order in orders)
            {
                foreach (var orderItem in order.OrderItem)
                {
                    var orderItemViewModel = new OrderItemViewModel
                    {
                        MenuItem = new MenuItemViewModel
                        {
                            Id = orderItem.MenuItem.Id,
                            Name = orderItem.MenuItem.Name
                        },
                        OrderItemTopping = orderItem.OrderItemTopping.ToList(),
                        Quantity = orderItem.Quantity,
                        Size = _ctx.SystemReference.FirstOrDefault(x => x.Id == orderItem.SizeId)
                    };
                    if (!orderItemsViewModel.ContainsKey(order.Id))
                    {
                        orderItemsViewModel[order.Id] = new List<OrderItemViewModel>();
                        orderItemsViewModel[order.Id].Add(orderItemViewModel);
                    }
                    else
                    {
                        orderItemsViewModel[order.Id].Add(orderItemViewModel);
                    };
                }

                var communityViewModel = new CommunityViewModel
                {
                    Id = order.Community.Id,
                    Name = order.Community.Name,
                    Active = order.Community.Active
                };

                var orderViewModel = new OrderViewModel
                {
                    Id = order.Id,
                    Status = order.OrderStatus.Status,
                    Note = order.Note,
                    Community = communityViewModel,
                    OrderItems = orderItemsViewModel[order.Id] != null ? orderItemsViewModel.First(x => x.Key == order.Id).Value : new List<OrderItemViewModel>(),
                    Name = order.CustomerFirstName + " " + order.CustomerLastName,
                    City = order.City,
                    Zipcode = order.ZipCode,
                    Address = order.AddressLine1 + " " + order.AddressLine2,
                    CreateTime = order.CreateTime,
                    CompleteTime = order.CompleteTime
                };
                ordersViewModel.Add(orderViewModel);
            }
                return Ok(ordersViewModel);
        }


    }
}
