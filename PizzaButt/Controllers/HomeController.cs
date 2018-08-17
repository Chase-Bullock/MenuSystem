using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<MenuItem> GetItemById(int id)
        {
            return await pizzaRepository.GetMenuItem(id) ?? new MenuItem();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
