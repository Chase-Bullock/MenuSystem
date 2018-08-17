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
        public IActionResult Index()
        {
            var orders = new List<OrderModel>
            {
                new OrderModel
                {
                    Name = "Tyler de Arrigunaga",
                    Order = "Tacos (3)",
                    Quantity = 2,
                    SpecialInstructions = "No Cilantro"
                }
            };
            return View(orders);
        }
    }
}