using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaButt.NewModels;
using PizzaButt.ViewModels;

namespace PizzaButt.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ICathedralKitchenRepository _cathedralKitchenRepository;

        public OrdersController(ICathedralKitchenRepository cathedralKitchenRepository)
        {
            _cathedralKitchenRepository = cathedralKitchenRepository;
        }
        [Authorize]
        public IActionResult Status()
        {
            var orders = _cathedralKitchenRepository.GetOrders();
            return View(orders);
        }

        public IActionResult OrderInfo([FromQuery] long orderId)
        {
            var order = _cathedralKitchenRepository.GetOrder(orderId);
            return View(order);
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
            var order = _cathedralKitchenRepository.GetOrder(orderId);
            _cathedralKitchenRepository.StartOrder(order);
            return Redirect("Status");
        }

        public IActionResult ReOpen([FromQuery] long orderId)
        {
            var order = _cathedralKitchenRepository.GetOrder(orderId);
            _cathedralKitchenRepository.StartOrder(order);
            return Redirect("Status");
        }


        public IActionResult Cancel([FromQuery] long orderId)
        {
            var order = _cathedralKitchenRepository.GetOrder(orderId);
            _cathedralKitchenRepository.CancelOrder(order);
            return RedirectToAction("OrderInfo", "Orders", new { orderId });
        }
    }
}