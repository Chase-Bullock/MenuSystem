using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PizzaButt.Models;

namespace PizzaButt.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IPizzaRepository pizzaRepository;

        public OrdersController(IPizzaRepository pizzaRepository)
        {
            this.pizzaRepository = pizzaRepository;
        }
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

        public async Task<IActionResult> Complete([FromQuery] string orderId)
        {
            var order = await pizzaRepository.GetOrder(orderId);
            order.CompleteTime = DateTime.UtcNow;
            order.Status = "Complete";
            await pizzaRepository.UpdateOrder(order);
            return Redirect("Status");
        }
        
        public async Task<IActionResult> Cancel([FromQuery] string orderId)
        {
            var order = await pizzaRepository.GetOrder(orderId);
            order.Status = "Canceled";
            await pizzaRepository.UpdateOrder(order);
            return RedirectToAction("OrderInfo", "Orders", new {orderId = orderId});
        }
    }
}