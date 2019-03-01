using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaButt.Helpers;
using PizzaButt.NewModels;
using PizzaButt.ViewModels;

namespace PizzaButt.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ICathedralKitchenRepository _cathedralKitchenRepository;
        private readonly CathedralKitchenContext _ctx;

        public OrdersController(ICathedralKitchenRepository cathedralKitchenRepository, CathedralKitchenContext ctx)
        {
            _cathedralKitchenRepository = cathedralKitchenRepository;
            _ctx = ctx;
        }
        [Authorize]
        public IActionResult Status()
        {
            var orders = _cathedralKitchenRepository.GetOrders();
            return View(orders);
        }

        public IActionResult OrderInfo()
        {
            var orderId = SessionHelper.GetObjectFromJson<long>(HttpContext.Session, "orderId");

            var order = _ctx.Order.Include(y => y.OrderOrderItem).Include(y => y.OrderStatus).FirstOrDefault(x => x.Id == orderId);
            var orderItems = _ctx.OrderItem.Include(y => y.MenuItem).Include(y => y.OrderItemTopping).ThenInclude(y => y.Topping).Where(x => x.OrderOrderItem.Any(z => z.OrderId == order.Id)).ToList();
            var orderItemsViewModel = new List<OrderItemViewModel>();

            foreach(var orderItem in orderItems)
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

            var orderViewModel = new OrderViewModel
            {
                Id = order.Id,
                Status = order.OrderStatus.Status,
                Note = order.Note,
                OrderItems = orderItemsViewModel,
                Name = order.CustomerName,
                CreateTime = order.CreateTime
            };

            return View(orderViewModel);
        }

        [Authorize]
        public IActionResult Menu()
        {
            var orders = _cathedralKitchenRepository.GetMenuItems();
            var Menu = new MenuViewModel
            {
                MenuItems = orders
            };
            return View(Menu);
        }

        [HttpPost]
        public IActionResult Menu(MenuViewModel selectedItems)
        {
            var items = _cathedralKitchenRepository.GetMenuItems();
            List<MenuItem> allItems = items.ToList();

            allItems.Where(x => selectedItems.MenuItemNames.Contains(x.Name) ? x.Active == true : x.Active == false).ToList();

            foreach (var x in allItems)
            {
                if (selectedItems.MenuItemNames.Contains(x.Name))
                {
                    x.Active = true;
                }
                else
                {
                    x.Active = false;
                }
            };

            _cathedralKitchenRepository.UpdateMenu(allItems);
            return Redirect("Status");
        }

        [Authorize]
        [HttpGet]
        public IActionResult CreateItem()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateItem(CreateMenuItemViewModel newItem)
        {
            var newCreatedItem = new MenuItem
            {
                Active = newItem.Active == "true" ? true : false,
                Name = newItem.Name
            };
            var success = _cathedralKitchenRepository.CreateMenuItem(newCreatedItem);
            ViewBag.Create = success;
            return View();
        }

        public IActionResult Complete([FromQuery] long orderId)
        {
            var order = _cathedralKitchenRepository.GetOrder(orderId);
            _cathedralKitchenRepository.CompleteOrder(order);
            return Redirect("Status");
        }

        public IActionResult Start([FromQuery] long orderId)
        {
            _cathedralKitchenRepository.StartOrder(orderId);
            return Redirect("Status");
        }

        public IActionResult ReOpen([FromQuery] long orderId)
        {
            _cathedralKitchenRepository.StartOrder(orderId);
            return Redirect("Status");
        }


        public IActionResult Cancel([FromQuery] long orderId)
        {
            _cathedralKitchenRepository.CancelOrder(orderId);
            SessionHelper.Clear(HttpContext.Session);
            return RedirectToAction("Index", "Home");
        }
    }
}