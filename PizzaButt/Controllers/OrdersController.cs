using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaButt.Models;
using PizzaButt.ViewModels;

namespace PizzaButt.Controllers
{
  public class OrdersController : Controller
  {
    private readonly IPizzaRepository pizzaRepository;

    public OrdersController(IPizzaRepository pizzaRepository)
    {
      this.pizzaRepository = pizzaRepository;
    }
    [Authorize]
    public async Task<IActionResult> Status()
    {
      var orders = await pizzaRepository.GetOrders();
      return View(orders);
    }

    public async Task<IActionResult> OrderInfo([FromQuery] string orderId)
    {
      var order = await pizzaRepository.GetOrder(orderId);
      return View(order);
    }

    [Authorize]
    public async Task<IActionResult> Menu()
    {
      var orders = await pizzaRepository.GetMenuItems();
      var Menu = new MenuViewModel
      {
        MenuItems = orders
      };
      return View(Menu);
    }

    [HttpPost]
    public async Task<IActionResult> Menu(MenuViewModel selectedItems)
    {
      var items = await pizzaRepository.GetMenuItems();
      List<MenuItem> allItems = items.ToList();

      allItems.Where(x => selectedItems.MenuItemNames.Contains(x.Name) ? x.Active == true : x.Active == false ).ToList();

      foreach(var x in allItems)
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

      await pizzaRepository.UpdateMenu(allItems);
      return Redirect("Status");
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> CreateItem()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateItem(CreateMenuItemViewModel newItem)
    {
      var newCreatedItem = new MenuItem
      {
        Active = newItem.Active == "true" ? true : false,
        Name = newItem.Name
      };
      var success = await pizzaRepository.CreateMenuItem(newCreatedItem);
      ViewBag.Create = success;
      return View();
    }

    public async Task<IActionResult> Complete([FromQuery] string orderId)
    {
      var order = await pizzaRepository.GetOrder(orderId);
      order.CompleteTime = DateTime.UtcNow;
      order.Status = "Complete";
      await pizzaRepository.UpdateOrder(order);
      return Redirect("Status");
    }

    public async Task<IActionResult> Start([FromQuery] string orderId)
    {
      var order = await pizzaRepository.GetOrder(orderId);
      order.Status = "Started";
      await pizzaRepository.UpdateOrder(order);
      return Redirect("Status");
    }

    public async Task<IActionResult> ReOpen([FromQuery] string orderId)
    {
      var order = await pizzaRepository.GetOrder(orderId);
      order.Status = "Started";
      await pizzaRepository.UpdateOrder(order);
      return Redirect("Status");
    }


    public async Task<IActionResult> Cancel([FromQuery] string orderId)
    {
      var order = await pizzaRepository.GetOrder(orderId);
      order.Status = "Canceled";
      await pizzaRepository.UpdateOrder(order);
      return RedirectToAction("OrderInfo", "Orders", new { orderId = orderId });
    }
  }
}