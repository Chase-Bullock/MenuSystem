using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CathedralKitchen.NewModels;
using CathedralKitchen.Service;
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
        private readonly IOrderService _orderService;
        private readonly ICathedralKitchenRepository _cathedralKitchenRepository;

        public OrdersController(ICathedralKitchenRepository cathedralKitchenRepository, CathedralKitchenContext ctx, IOrderService orderService)
        {
            _ctx = ctx;
            _orderService = orderService;
            _cathedralKitchenRepository = cathedralKitchenRepository;
        }


        [HttpGet]
        public IActionResult GetStatusOfAllOrders()
        {

            var ordersViewModel = _orderService.GetStatusOfAllOrders();

            return Ok(ordersViewModel);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderInfoForCustomer(long orderId)
        {
            var orderViewModel = _orderService.GetOrderInfoForCustomer(orderId);

            return Ok(orderViewModel);
        }

        [HttpPost("")]
        public IActionResult SubmitOrder([FromBody]SubmitOrderViewModel submitOrderViewModel)
        {
            var order = new Order
            {
                OrderStatusId = _ctx.OrderStatus.SingleOrDefault(x => x.Status == "InProgress").Id,
                CustomerEmail = submitOrderViewModel.Order.Email,
                CustomerFirstName = submitOrderViewModel.Order.FirstName,
                CustomerLastName = submitOrderViewModel.Order.LastName,
                Note = submitOrderViewModel.Order.Note,
                CommunityId = submitOrderViewModel.Order.CommunityId > 0 ? submitOrderViewModel.Order.CommunityId : _ctx.Community.First(x => x.Name == "Cathedral").Id,
                AddressLine1 = submitOrderViewModel.Order.AddressLine1,
                AddressLine2 = submitOrderViewModel.Order.AddressLine2,
                City = submitOrderViewModel.Order.City,
                ZipCode = submitOrderViewModel.Order.Zipcode,
                CreateBy = 1,
                UpdateBy = 1,
                CreateTime = DateTime.UtcNow,
                UpdateTime = DateTime.UtcNow,
            };

            order.OrderStatusId = _ctx.OrderStatus.SingleOrDefault(x => x.Status == "Pending").Id;
            _ctx.Add(order);

            foreach (var item in submitOrderViewModel.OrderItems)
            {
                var orderItem = new OrderItem
                {
                    MenuItemId = item.MenuItem.Id,
                    Quantity = item.Quantity,
                    SizeId = item.SizeId,
                    CreateBy = 1,
                    UpdateBy = 1,
                    CreateTime = DateTime.UtcNow,
                    UpdateTime = DateTime.UtcNow,
                    Order = order

                };

                _ctx.OrderItem.Add(orderItem);

                foreach (var topping in item.Toppings)
                {
                    var toppingItem = new OrderItemTopping
                    {
                        OrderItem = orderItem,
                        ToppingId = topping.Id,
                        CreateBy = 1,
                        UpdateBy = 1,
                        CreateTime = DateTime.UtcNow,
                        UpdateTime = DateTime.UtcNow
                    };
                    _ctx.OrderItemTopping.Add(toppingItem);
                }
            }
            _ctx.SaveChanges();

            return Ok();
        }

    }
}
