using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CathedralKitchen.NewModels;
using CathedralKitchen.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CathedralKitchen.Service
{
    public class OrderService : IOrderService
    {
        private readonly CathedralKitchenContext _ctx;

        public OrderService(CathedralKitchenContext ctx)
        {
            _ctx = ctx;
        }

        public List<OrderViewModel> GetStatusOfAllOrders()
        {
            var orderStatusesToIgnore = _ctx.OrderStatus.Where(x => x.Status == "Canceled" || x.Status == "InProgress").Select(x => x.Id);
            var orders = _ctx.Order.Where(x => !orderStatusesToIgnore.Contains(x.OrderStatusId))
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
            return ordersViewModel;
        }

        public OrderViewModel GetOrderInfoForCustomer(long orderId)
        {
            var order = _ctx.Order.Include(y => y.OrderItem).ThenInclude(z => z.OrderItemTopping).ThenInclude(v => v.Topping).Include(c => c.OrderItem).ThenInclude(w => w.MenuItem).Include(y => y.OrderStatus).Include(c => c.Community).FirstOrDefault(x => x.Id == orderId);
            var orderItemsViewModel = new List<OrderItemViewModel>();

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
                orderItemsViewModel.Add(orderItemViewModel);
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
                Community = communityViewModel,
                Note = order.Note,
                OrderItems = orderItemsViewModel,
                Name = order.CustomerFirstName + " " + order.CustomerLastName,
                CreateTime = order.CreateTime,
                CompleteTime = order.CompleteTime
            };

            return orderViewModel;
        }

    }
}
