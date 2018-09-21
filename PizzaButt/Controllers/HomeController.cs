using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using PizzaButt.Models;

namespace PizzaButt.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPizzaRepository pizzaRepository;

        public HomeController(IPizzaRepository pizzaRepository)
        {
            this.pizzaRepository = pizzaRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<MenuItem>> GetAllItems()
        {
            return await pizzaRepository.GetMenuItems();
        }

        [HttpGet("{id}")]
        public async Task<MenuItem> GetItemById(string id)
        {
            return await pizzaRepository.GetMenuItem(id) ?? new MenuItem();
        }

        public async Task<IActionResult> Index()
        {
            var options = await pizzaRepository.GetMenuItems();
            return View(options);
        }

        [HttpPost]
        public async Task<IActionResult> Index(OrderModel request)
        {
            if (!ModelState.IsValid)
            {
                var options = await pizzaRepository.GetMenuItems();
                return View(options);
            }
            
            var orderId = await pizzaRepository.SendOrder(request);
            return RedirectToAction("OrderInfo", "Orders", new {orderId = orderId});

        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
